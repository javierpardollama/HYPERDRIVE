using AutoMapper;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.Mappings.Classes
{
    public class ModelingProfile : Profile
    {
        public ModelingProfile()
        {
            CreateMap<ApplicationRole, ViewApplicationRole>();

            CreateMap<ApplicationUser, ViewApplicationUser>();

            CreateMap<ApplicationUserRole, ViewApplicationUserRole>();

            CreateMap<ApplicationUserToken, ViewApplicationUserToken>();

            CreateMap<Archive, ViewArchive>();

            CreateMap<ApplicationUserArchive, ViewApplicationUserArchive>();

            CreateMap<ArchiveVersion, ViewArchiveVersion>();
        }
    }
}
