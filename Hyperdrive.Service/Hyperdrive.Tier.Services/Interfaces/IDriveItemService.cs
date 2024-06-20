using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.ViewModels.Classes.Additions;
using Hyperdrive.Tier.ViewModels.Classes.Filters;
using Hyperdrive.Tier.ViewModels.Classes.Updates;
using Hyperdrive.Tier.ViewModels.Classes.Views;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperdrive.Tier.Services.Interfaces
{
    public interface IDriveItemService : IBaseService
    {
        Task<DriveItem> FindDriveItemById(int @id);

        Task RemoveDriveItemById(int @id);

        Task<IList<ViewDriveItem>> FindAllDriveItem();

        Task<ViewPage<ViewDriveItem>> FindPaginatedDriveItemByApplicationUserId(FilterPageDriveItem @viewModel);

        Task<ViewPage<ViewDriveItem>> FindPaginatedSharedDriveItemByApplicationUserId(FilterPageDriveItem @viewModel);

        Task<IList<ViewDriveItemVersion>> FindAllDriveItemVersionByDriveItemId(int @id);

        Task<ApplicationUser> FindApplicationUserById(int @id);

        Task<ViewDriveItem> AddDriveItem(AddDriveItem @viewModel);

        void AddApplicationUserDriveItem(AddDriveItem @viewModel, DriveItem @entity);

        void AddDriveItemVersion(AddDriveItem @viewModel, DriveItem @entity);

        Task<ViewDriveItem> UpdateDriveItem(UpdateDriveItem @viewModel);

        void UpdateApplicationUserDriveItem(UpdateDriveItem @viewModel, DriveItem @entity);

        void UpdateDriveItemVersion(UpdateDriveItem @viewModel, DriveItem @entity);

        Task<DriveItem> CheckName(AddDriveItem @viewModel);

        Task<DriveItem> CheckName(UpdateDriveItem @viewModel);
    }
}
