using Hyperdrive.Domain.Dtos;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Exceptions;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Domain.Profiles;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Hyperdrive.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemVersionManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDriveItemVersionManager"/>
/// </summary>    
/// <param name="context">Injected <see cref="IApplicationContext"/></param>
/// <param name="logger">Injected <see cref="ILogger{DriveItemBinaryManager}"/></param>
public class DriveItemVersionManager(IApplicationContext context,
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

    /// <summary>
    /// Finds Drive Item Version By Id
    /// </summary>
    /// <param name="id">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemVersion}"/></returns>
    public async Task<DriveItemVersion> FindDriveItemVersionById(int @id)
    {
        DriveItemVersion @entity = await Context.DriveItemVersions
                       .TagWith("FindDriveItemVersionById")
                       .AsNoTracking()
                       .AsSplitQuery()
                       .Where(x => x.Id == @id)
                       .FirstOrDefaultAsync();

        if (@entity is null)
        {
            // Log
            string @logData = nameof(DriveItemVersion)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItemVersion)
                                       + " does not exist");
        }

        return @entity;
    }

    /// <summary>
    /// Targets Drive Item Version
    /// </summary>
    /// <param name="entity">Injected <see cref="DriveItemVersion"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task TargetDriveItemVersion(DriveItemVersion @entity)
    {
        DriveItemVersion @version = new()
        {
            FileName = @entity.FileName,
            NormalizedFileName = @entity.NormalizedFileName,
            Name = @entity.Name,
            NormalizedName = @entity.NormalizedName,
            Extension = @entity.Extension,
            NormalizedExtension = @entity.NormalizedExtension,
            Type = @entity.Type,
            Size = @entity.Size,
            Data = @entity.Data,
            DriveItemId = @entity.DriveItemId,
        };

        await Context.DriveItemVersions.AddAsync(@version);

        await Context.SaveChangesAsync();

        // Log
        string @logData = nameof(DriveItemVersion)
                          + " was added at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);
    }
}
