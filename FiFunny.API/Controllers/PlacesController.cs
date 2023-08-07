using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FiFunny.API.Data;
using FiFunny.API.Dtos;
using FiFunny.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiFunny.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        private DataContext _context;
        private IDefaultUserRepository _defaultUserRepository;

        public PlacesController(DataContext context,IAppRepository appRepository, IMapper mapper, IDefaultUserRepository defaultUserRepository)
        {
            _appRepository = appRepository;
            _mapper = mapper;
            _context = context;
            _defaultUserRepository = defaultUserRepository;
        }

        [EnableCors("AllowOrigin")]
        public ActionResult GetPlaces(int? categoryId = null)
        {
            var places = new List<Place>();
            if (categoryId == null)
            {
                places = _appRepository.GetPlaces();
            }
            else
            {
                places = _appRepository.GetPlacesByCategoryId(categoryId.Value);
            }
            var placesToReturn = _mapper.Map<List<PlaceForListDto>>(places);
            return Ok(placesToReturn);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]Place place)
        {
            _appRepository.Add(place);
            _appRepository.SaveAll();
            return Ok(place);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("addFilter")]
        public ActionResult AddFilter(int id, [FromBody]FilterForCreationDto filterForCreation)
        {
            var place = _appRepository.GetPlaceById(id);
            if (place == null)
            {
                return BadRequest("Could not find the place");
            }

            var filter = _mapper.Map<Filter>(filterForCreation);
            filter.Place = place;
            place.Filters = filter;

            if (_appRepository.SaveAll())
            {
                return StatusCode(201);
            }
            return BadRequest("Could not add the photo");

        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("verified")]
        public ActionResult PlaceVerified(int id)
        {
            _appRepository.VerifiedPlace(id);
            _appRepository.SaveAll();
            return StatusCode(201);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("delete")]
        public ActionResult Delete([FromBody] Place place)
        {
            _appRepository.Delete(place);
            _appRepository.SaveAll();
            return StatusCode(201);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("detail")]
        public ActionResult GetPlaceById(int id)
        {
            var place = _appRepository.GetPlaceById(id);
            var placeToReturn = _mapper.Map<PlaceForDetailDto>(place);
            placeToReturn.sehiradi = _appRepository.GetProvinceById(placeToReturn.SehirId);
            placeToReturn.ilceadi = _appRepository.GetDistrictById(placeToReturn.IlceId);
            return Ok(placeToReturn);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("photos")]
        public ActionResult GetPhotosByPlace(int id)
        {
            var photos = _appRepository.GetPhotosByPlace(id);
            return Ok(photos);
        }
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("category")]
        public ActionResult GetPlacesByCategoryId(int categoryId)
        {
            var places = _appRepository.GetPlacesByCategoryId(categoryId);
            var placesToReturn = _mapper.Map<List<PlaceForListDto>>(places);
            return Ok(placesToReturn);
        }

        [EnableCors("AllowOrigin")]
        [HttpPut("update")]
        public ActionResult UpdatePlaceSettings(Place place)
        {
            var a = _context.Places.FirstOrDefault(asd => asd.Id == place.Id);

            if (a == null)
            {
                return BadRequest("about not found");
            }

            if (place.Name != "")
            {
                a.Name = place.Name;
            }

            if (place.Uyarilar != "")
            {
                a.Uyarilar = place.Uyarilar;
            }

            if (place.Adres != "")
            {
                a.Adres = place.Adres;
            }

            if (place.VideoURL != "")
            {
                a.VideoURL = place.VideoURL;
            }

            if (place.VergiNumarasi != "")
            {
                a.VergiNumarasi = place.VergiNumarasi;
            }

            _context.SaveChanges();

            return Ok(a);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("getComments")]
        public ActionResult GetCommentsPlaceByIdAll(int id)
        {
            var comments = _appRepository.GetCommentByPlaceId(id);
            var commentToReturn = _mapper.Map<List<CommentsForListDto>>(comments);
            return Ok(commentToReturn);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("getCommentsAdmin")]
        public ActionResult GetCommentsPlaceAdmin()
        {
            var comments = _appRepository.GetCommentByPlaceIdAdmin();
            var commentToReturn = _mapper.Map<List<CommentsForListDto>>(comments);
            return Ok(commentToReturn);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("addComment")]
        public ActionResult AddComment(int id, [FromBody] Comment comment)
        {
            var place = _appRepository.GetPlaceById(comment.PlaceId);
            if (place == null)
            {
                return BadRequest("Could not find the place");
            }
            var user = _defaultUserRepository.GetDefaultUserById(comment.UserId);
            if (user == null)
            {
                return BadRequest("Could not find the place");
            }

            var com = _mapper.Map<CommentsForListDto>(comment);
            com.user = user;

            _appRepository.Add(comment);
            if (_appRepository.SaveAll())
            {
                return StatusCode(201);
            }
            return BadRequest("Could not add comment");

        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("deleteComment")]
        public ActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            _appRepository.Delete(comment);
            _appRepository.SaveAll();
            return StatusCode(201);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("updateComment")]
        public ActionResult UpdateComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if(comment == null)
            {
                return BadRequest("Could not found comment");
            }

            comment.Verified = true;

            _context.SaveChanges();

            return Ok(id);
        }

    }
}
