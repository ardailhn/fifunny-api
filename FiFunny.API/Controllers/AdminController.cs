using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FiFunny.API.Data;
using FiFunny.API.Dtos;
using FiFunny.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FiFunny.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminRepository _adminRepository;
        private IConfiguration _configuration;
        private IMapper _mapper;

        public AdminController(IAdminRepository adminRepository, IConfiguration configuration, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("notificaitons")]
        public ActionResult GetMessage()
        {
            var notis = _adminRepository.GetNotification();

            return Ok(notis);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("message")]
        public async Task<ActionResult> SendMessage([FromBody] NotificationDto notification)
        {
            var notiToCreate = new Notification
            {
                FirstName = notification.firstName,
                LastName = notification.lastName,
                Email = notification.email,
                Phone = notification.phone,
                Text = notification.Text
                
            };

            var createdNoti = await _adminRepository.SendMessage (notiToCreate);

            return StatusCode(201);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("notificaitons/delete")]
        public ActionResult Delete([FromBody] Notification noti)
        {
            _adminRepository.Delete(noti);
            _adminRepository.SaveAll();
            return StatusCode(201);
        }
        
        [EnableCors("AllowOrigin")]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] AdminDto adminDto)
        {
            if (await _adminRepository.UserExists(adminDto.UserName))
            {
                ModelState.AddModelError("UserName", "UserName already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new Admin
            {
                UserName = adminDto.UserName
            };

            var createdUser = await _adminRepository.Register(userToCreate, adminDto.Password);

            return StatusCode(201);
        }
        
        [EnableCors("AllowOrigin")]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AdminDto adminDto)
        {
            var admin = await _adminRepository.Login(adminDto.UserName, adminDto.Password);

            if (admin == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("places")]
        public ActionResult GetPlaces()
        {
            var places = new List<Place>();
            places = _adminRepository.GetPlaces();
            var placesToReturn = _mapper.Map<List<PlaceForAdminDto>>(places);
            return Ok(placesToReturn);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("places/list")]
        public ActionResult GetPlacesList()
        {
            var places = new List<Place>();
            places = _adminRepository.GetPlacesList();
            var placesToReturn = _mapper.Map<List<PlaceForAdminDto>>(places);
            return Ok(placesToReturn);
        }

    }
}
