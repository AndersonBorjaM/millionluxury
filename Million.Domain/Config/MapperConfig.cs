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
            CreateMap<RegisterUserDTO, User>()
                .ForMember(x => x.UserName, c => c.MapFrom(t => t.UserName))
                .ForMember(x => x.PasswordHash, c => c.MapFrom(t => BCrypt.Net.BCrypt.HashPassword(t.PasswordHash)))
                .ReverseMap();

            CreateMap<PropertyTraceDTO, PropertyTrace>().ReverseMap();
            CreateMap<PropertyDTO, Property>().ReverseMap();

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
                .ForMember(x => x.FileProperty, c => c.MapFrom(t => ConvertToArrayBytes(t.FileProperty)))
                .ReverseMap();
        }

        private byte[] ConvertToArrayBytes(IFormFile? file)
        {
            if (file == null)
                return null;

            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
