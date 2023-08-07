using FiFunny.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;

        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public Photo GetPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            return photo;
        }

        public List<Photo> GetPhotosByPlace(int id)
        {
            var photos = _context.Photos.Where(p => p.PlaceId == id).ToList();
            return photos;
        }

        public Place GetPlaceById(int id)
        {
            var places = _context.Places.Include(p => p.Photos).FirstOrDefault(p => p.Id == id);
            return places;
        }

        public List<Place> GetPlaces()
        {
            var places = _context.Places.Include(p=>p.Photos).Include(p => p.Filters).Where(p => p.Verified == true).ToList();
            return places;
        }

        public List<Province> GetProvinces() 
        {
            var provinces = _context.iller.ToList();
            return provinces;
        }

        public string GetProvinceById(int id)
        {
            var provinces = _context.iller.FirstOrDefault(i => i.id == id);
            return provinces.sehiradi;
        }

        public string GetDistrictById(int id)
        {
            var district = _context.ilceler.FirstOrDefault(i => i.id == id);
            return district.ilceadi;
        }

        public List<District> GetDistrictsById(int id)
        {
            var districts = _context.ilceler.Where(d => d.sehirid == id).ToList();
            return districts;
        }

        public List<Place> GetPlacesByCategoryId(int id)
        {
            var places = _context.Places.Include(p => p.Photos).Include(p => p.Filters).Where(p => p.CategoryId == id && p.Verified==true ).ToList();
            return places;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void VerifiedPlace(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.Id == id);
            place.Verified = true;
            _context.Entry(place).State = EntityState.Modified;
        }

        public List<Comment> GetCommentByPlaceId(int id)
        {
            var comments = _context.Comments.Where(p => p.PlaceId == id && p.Verified == true ).Include(c => c.user).ToList();
            return comments;
        }

        public List<Comment> GetCommentByPlaceIdAdmin()
        {
            var comments = _context.Comments.Where(c => c.Verified == false ).Include(c => c.user).ToList();
            return comments;
        }

    }
}
