using Application.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))   
                .ForMember(dest => dest.Space, opt => opt.MapFrom(src => src.Space));

            CreateMap<Space, SpaceDto>();
        }
    }
}
