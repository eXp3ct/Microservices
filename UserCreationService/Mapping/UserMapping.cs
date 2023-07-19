using AutoMapper;
using Core.Model;
using UserCreationService.Commands;

namespace UserCreationService.Mapping
{
	public class UserMapping : Profile
	{
        public UserMapping()
        {
            CreateMap<SendUserQuery, User>()
                .ForMember(x => x.Id, opt => opt.MapFrom(user => Guid.NewGuid()))
                .ForMember(x => x.Name, opt => opt.MapFrom(user => user.Name))
                .ForMember(x => x.Surname, opt => opt.MapFrom(user => user.Surname))
                .ForMember(x => x.MiddleName, opt => opt.MapFrom(user => user.MiddleName))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(user => user.PhoneNumber))
                .ForMember(x => x.Email, opt => opt.MapFrom(user => user.Email))
                .ForMember(x => x.OrganizationId, opt => opt.MapFrom(user => Guid.Empty));
        }
    }
}
