using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiFunny.API.Data;
using FiFunny.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiFunny.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private IAppRepository _appRepository;
        private DataContext _context;

        public AddressController(DataContext context, IAppRepository appRepository)
        {
            _appRepository = appRepository;
            _context = context;
        }

        [EnableCors("AllowOrigin")]
        public ActionResult GetProvince()
        {
            var provinces = _appRepository.GetProvinces().ToList();
            return Ok(provinces);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("province/{id}")]
        public ActionResult GetProvinceById(int id)
        {
            var values = _context.iller.FirstOrDefault(p => p.id == id);
            return Ok(values);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("district/{id}")]
        public ActionResult GetDistrictById(int id)
        {
            var values = _context.ilceler.FirstOrDefault(p => p.id == id);
            return Ok(values);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("{id}")]
        public ActionResult GetDitricts(int id)
        {
            var districts = _appRepository.GetDistrictsById(id).ToList();
            return Ok(districts);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("categories")]
        public async Task<ActionResult> GetCategories()
        {
            var values = await _context.Categories.ToListAsync();
            return Ok(values);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("cinsiyet")]
        public async Task<ActionResult> GetCinsiyet()
        {
            var values = await _context.Cinsiyet.ToListAsync();
            return Ok(values);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("about")]
        public async Task<ActionResult> GetAbout()
        {
            var values = await _context.about.FirstOrDefaultAsync(p => p.Id == 1);
            return Ok(values);
        }

        [EnableCors("AllowOrigin")]
        [HttpPut("about/update")]
        public ActionResult UpdateAbout(about about)
        {
            var a = _context.about.FirstOrDefault(p => p.Id == 1);
            if(a == null)
            {
                return BadRequest("about not found");
            }

            a.text = about.text;

            _context.SaveChanges();

            return Ok(a);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("MainPlace")]
        public async Task<ActionResult> GetMainPlace()
        {
            var values = await _context.MainPlace.FirstOrDefaultAsync(p => p.Id == 1);
            return Ok(values);
        }

        [EnableCors("AllowOrigin")]
        [HttpPut("mainplace/update")]
        public ActionResult UpdateMainPlace(MainPlace mainPlace)
        {
            var main = _context.MainPlace.FirstOrDefault(m => m.Id == 1);
            if (main == null)
            {
                return BadRequest("about not found");
            }
            main.PlaceId = mainPlace.PlaceId;
            _context.SaveChanges();
            return Ok(main);
        }

    }
}
