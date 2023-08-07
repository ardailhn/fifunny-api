using AutoMapper;
using FiFunny.API.Dtos;
using FiFunny.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Place, PlaceForListDto>()
                .ForMember(dest => dest.PhotoURL, opt =>
                  {
                      opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).URL);
                  });
            CreateMap<Place, PlaceForDetailDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<PhotoForReturnDto, Photo>();
            CreateMap<Place, PlaceForAdminDto>();
            CreateMap<FilterForCreationDto, Filter>();
            CreateMap<Comment, CommentsForListDto>()
                .ForMember(dest => dest.firstName, opt =>
            {
                opt.MapFrom(src => src.user.FirstName);
            }).ForMember(dest => dest.lastName, opt =>
            {
                opt.MapFrom(src => src.user.LastName);
            });

        }
    }
}
