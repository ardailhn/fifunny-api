using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();

        List<Place> GetPlaces();
        List<Photo> GetPhotosByPlace(int id);
        Place GetPlaceById(int id);
        Photo GetPhoto(int id);

        List<Place> GetPlacesByCategoryId(int id);
        List<Province> GetProvinces();
        List<District> GetDistrictsById(int id);
        string GetProvinceById(int id);
        string GetDistrictById(int id);

        void VerifiedPlace(int id);
        List<Comment> GetCommentByPlaceId(int id);
        List<Comment> GetCommentByPlaceIdAdmin();
    }
}
