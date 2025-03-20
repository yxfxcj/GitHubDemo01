using AutoMapper;
using Ivan.DianPing.Models.DTO;
using Ivan.DianPing.Models.PO;

namespace Ivan.DianPing.AutoMapper
{
    public class AppProfile : Profile
    {
        public AppProfile() 
        {
            CreateMap<User, UserDto>();
        }
    }
}
