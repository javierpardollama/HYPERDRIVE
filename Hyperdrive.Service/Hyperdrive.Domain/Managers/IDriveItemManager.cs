using System.Collections.Generic;
using System.Threading.Tasks;
using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;

namespace Hyperdrive.Domain.Managers
{
    public interface IDriveItemManager : IBaseManager
    {
        Task<DriveItem> FindDriveItemById(int @id);

        Task RemoveDriveItemById(int @id);

        Task<IList<DriveItemDto>> FindAllDriveItem();

        Task<PageDto<DriveItemDto>> FindPaginatedDriveItemByApplicationUserId(int @index, int @size, int @id);

        Task<PageDto<DriveItemDto>> FindPaginatedSharedDriveItemByApplicationUserId(int @index, int @size, int @id);

        Task<IList<DriveItemVersionDto>> FindAllDriveItemVersionByDriveItemId(int @id);

        Task<ApplicationUser> FindApplicationUserById(int @id);

        Task<DriveItemDto> AddDriveItem(DriveItem @entity);

        void AddApplicationUserDriveItem(List<int> @users, DriveItem @entity);

        void AddDriveItemVersion(DriveItemVersion @version, DriveItem @entity);

        Task<DriveItemDto> UpdateDriveItem(DriveItem @entity);

        void UpdateApplicationUserDriveItem(List<int> @users, DriveItem @entity);

        void UpdateDriveItemVersion(DriveItemVersion @version, DriveItem @entity);

        Task<DriveItem> CheckName(string @name);

        Task<DriveItem> CheckName(string @name, int @id);
    }
}
