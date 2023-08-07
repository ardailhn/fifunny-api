using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FiFunny.API.Data;
using FiFunny.API.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiFunny.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("user")]
        public ActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            var userToReturn = _mapper.Map<UserForProfileDto>(user);

            return Ok(userToReturn);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("places")]
        public ActionResult GetPlacesByUserId(int id)
        {
            var places = _userRepository.GetPlacesByUserId(id);
            var placesToReturn = _mapper.Map<List<PlaceForListDto>>(places);
            return Ok(placesToReturn);
        }

    }
}
