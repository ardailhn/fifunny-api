using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FiFunny.API.Data;
using FiFunny.API.Dtos;
using FiFunny.API.Helpers;
using FiFunny.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FiFunny.API.Controllers
{
    [Route("api/places/{id}/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        private IOptions<CloudinarySettings> _cloudinaryConfig;

        private Cloudinary _cloudinary;

        public PhotosController(IOptions<CloudinarySettings> cloudinaryConfig, IAppRepository appRepository, IMapper mapper)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _appRepository = appRepository;
            _mapper = mapper;
            Account account = new Account(_cloudinaryConfig.Value.CloudName,_cloudinaryConfig.Value.ApiKey,_cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }


        
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult AddPhotoForPlace(int id,[FromForm]PhotoForCreationDto photoForCreationDto)
        {
            var place = _appRepository.GetPlaceById(id);
            if (place==null)
            {
                return BadRequest("Could not find the place");
            }
            /*
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId != place.UserId)
            {
                return Unauthorized();
            }
            */
            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.SecureUrl.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.Place = place;

            if (!place.Photos.Any(p => p.IsMain))
            {
                photo.IsMain = true;
            }

            place.Photos.Add(photo);

            if (_appRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }
            return BadRequest("Could not add the photo");

        }

        [EnableCors("AllowOrigin")]
        [HttpGet(Name = "GetPhoto")]
        public ActionResult GetPhoto(int id)
        {
            var photoFromDb = _appRepository.GetPhoto(id);
            var photo = _mapper.Map<PhotoForReturnDto>(photoFromDb);

            return Ok(photo);
        }
    }
}
