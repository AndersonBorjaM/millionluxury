using AutoMapper;
using Microsoft.AspNetCore.Http;
using Million.Domain.DTO;
using Million.Domain.Models;

namespace Million.Domain.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<RegisterUserDTO, User>().ReverseMap();

            CreateMap<PropertyTrace, PropertyTrace>().ReverseMap();

            CreateMap<LoginUserDTO, User>()
                .ForMember(x => x.UserName, c => c.MapFrom(t => t.UserName))
                .ForMember(x => x.PasswordHash, c => c.MapFrom(t => t.Password))
                .ReverseMap();

            CreateMap<OwnerDTO, Owner>()
                .ForMember(x => x.Address, c => c.MapFrom(t => t.Address))
                .ForMember(x => x.Name, c => c.MapFrom(t => t.Name))
                .ForMember(x => x.Birthday, c => c.MapFrom(t => t.Birthday))
                .ForMember(x => x.Photo, c => c.MapFrom(t => ConvertToArrayBytes(t.Photo)))
                .ReverseMap();

            CreateMap<PropertyImageDTO, PropertyImage>()
                .ForMember(x => x.IdProperty, c => c.MapFrom(t => t.IdProperty))
                .ForMember(x => x.Enabled, c => c.MapFrom(t => t.Enabled))
                .ForMember(x => x.File, c => c.MapFrom(t => ConvertToArrayBytes(t.File)))
                .ReverseMap();
        }

        private byte[] ConvertToArrayBytes(IFormFile? photo)
        {
            using var memoryStream = new MemoryStream();
            photo.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
