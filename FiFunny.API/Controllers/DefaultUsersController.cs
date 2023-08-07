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
    public class DefaultUsersController : ControllerBase
    {
        private IDefaultUserRepository _auhtRepository;
        private IConfiguration _configuration;
        private IMapper _mapper;


        public DefaultUsersController(IDefaultUserRepository auhtRepository, IConfiguration configuration,IMapper mapper)
        {
            _auhtRepository = auhtRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] DefaultUserForRegisterDto userForRegisterDto)
        {
            if (await _auhtRepository.UserExists(userForRegisterDto.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new DefaultUser
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Phone = userForRegisterDto.Phone
            };

            var createdUser = await _auhtRepository.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] DefaultUserForLoginDto userForLoginDto)
        {
            var user = await _auhtRepository.Login(userForLoginDto.Email, userForLoginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.FirstName)
                }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("user")]
        public ActionResult GetDefaultUserById(int id)
        {
            var user = _auhtRepository.GetDefaultUserById(id);
            var userToReturn = _mapper.Map<DefaultUserForProfileDto>(user);

            return Ok(userToReturn);
        }

    }
}
