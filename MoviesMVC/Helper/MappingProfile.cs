

namespace MoviesMVC.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterVM, AppUser>();
            CreateMap<Member, MemberVM>().ReverseMap();
        }
    }
}