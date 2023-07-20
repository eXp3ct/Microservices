using AutoMapper;
using Core.Model;
using Core.Model.Dtos;

namespace ApplicationReceiverService.Mapping
{
	public class UserDtoMapping : Profile
	{
        public UserDtoMapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(u => u.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(u => u.Name))
                .ForMember(x => x.Surname, opt => opt.MapFrom(u => u.Surname))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(u => u.PhoneNumber))
                .ForMember(x => x.Email, opt => opt.MapFrom(u => u.Email));
        }
    }
}
