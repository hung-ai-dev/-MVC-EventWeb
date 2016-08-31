using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EventWeb.Core.Dtos;
using EventWeb.Core.Models;

namespace EventWeb.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Genre, GenreDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });
        }

        protected override void Configure()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Genre, GenreDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });
        }
    }
}