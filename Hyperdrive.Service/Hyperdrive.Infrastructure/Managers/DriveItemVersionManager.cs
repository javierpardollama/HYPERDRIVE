using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Domain.Profiles;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;


namespace Hyperdrive.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemVersionManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDriveItemVersionManager"/>
/// </summary>    
/// <param name="context">Injected <see cref="IApplicationContext"/></param>
/// <param name="logger">Injected <see cref="ILogger"/></param>
public class DriveItemVersionManager(
    IApplicationContext context,
    ILogger<DriveItemVersionManager> logger) : BaseManager(context), IDriveItemVersionManager
{

    /// <summary>
    /// Finds Paginated Drive Item Version By Drive Item Id
    /// </summary>
    /// <param name="index">Injected <see cref="int"/></param>
    /// <param name="size">Injected <see cref="int"/></param>
    /// <param name="driveitemid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{PageDto{DriveItemVersionDto}}"/></returns>
    public async Task<PageDto<DriveItemVersionDto>> FindPaginatedDriveItemVersionByDriveItemId(int @index, int @size, int @driveitemid)
    {
        PageDto<DriveItemVersionDto> @page = new()
        {
            Length = await Context.DriveItemVersions.TagWith("CountAllDriveItemVersionByDriveItemId")
               .AsSplitQuery()
               .AsNoTracking()
               .Where(x => x.DriveItemId == @driveitemid)
               .CountAsync(),
            Index = @index,
            Size = @size,
            Items = await Context.DriveItemVersions
               .TagWith("FindPaginatedDriveItemVersionByDriveItemId")
               .AsSplitQuery()
               .AsNoTracking()
               .Where(x => x.DriveItemId == @driveitemid)
               .OrderByDescending(x => x.CreatedAt)
               .Skip(@index * @size)
               .Take(@size)
               .Select(x => x.ToDto())
               .ToListAsync()
        };

        return page;
    }    
}
