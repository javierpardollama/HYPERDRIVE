using AutoMapper;

using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Views;

namespace Hyperdrive.Tier.Mappings.Classes
{
    /// <summary>
    /// Represents a <see cref="ModelingProfile"/> class. Inherits <see cref="Profile"/>
    /// </summary>
    public class ModelingProfile : Profile
    {
        /// <summary>
        /// Initializes a new Instance of <see cref="ModelingProfile"/>
        /// </summary>
        public ModelingProfile()
        {
            CreateMap<ApplicationRole, ViewApplicationRole>();

            CreateMap<ApplicationUser, ViewApplicationUser>();

            CreateMap<ApplicationUserRole, ViewApplicationUserRole>();

            CreateMap<ApplicationUserToken, ViewApplicationUserToken>();

            CreateMap<DriveItem, ViewDriveItem>();

            CreateMap<ApplicationUserDriveItem, ViewApplicationUserDriveItem>();

            CreateMap<DriveItemVersion, ViewDriveItemVersion>();
        }
    }
}
