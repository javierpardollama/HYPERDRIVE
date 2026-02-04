using Hyperdrive.Main.Domain.Dtos;
using Hyperdrive.Main.Domain.Entities;
using Hyperdrive.Main.Domain.Exceptions;
using Hyperdrive.Main.Domain.Managers;
using Hyperdrive.Main.Domain.Profiles;
using Hyperdrive.Main.Infrastructure.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemBinaryManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDriveItemBinaryManager"/>
/// </summary>    
/// <param name="context">Injected <see cref="IApplicationContext"/></param>
/// <param name="logger">Injected <see cref="ILogger{DriveItemBinaryManager}"/></param>
public class DriveItemBinaryManager(IApplicationContext context,
    ILogger<DriveItemBinaryManager> logger) : BaseManager(context), IDriveItemBinaryManager
{

    /// <summary>
    /// Finds Latest Drive Item Binary By Drive Item Id
    /// </summary>
    /// <param name="driveitemid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
    public async Task<DriveItemBinaryDto> FindLatestDriveItemBinaryById(int @driveitemid)
    {
        DriveItemBinaryDto @binary = await Context.DriveItemContents
            .TagWith("FindLatestDriveItemBinaryById")
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.DriveItemInfo)
            .Where(x => x.DriveItemInfo.DriveItemId == @driveitemid)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => x.ToBinary())
            .FirstOrDefaultAsync();

        if (@binary is null)
        {
            // Log
            string @logData = nameof(DriveItemContent)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItemContent)
                                       + " does not exist");
        }

        return @binary;
    }

    /// <summary>
    /// Finds Drive Item Binary By Drive Item Info Id
    /// </summary>
    /// <param name="driveiteminfoid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
    public async Task<DriveItemBinaryDto> FindDriveItemBinaryById(int @driveiteminfoid)
    {
        DriveItemBinaryDto @binary = await Context.DriveItemContents
                       .TagWith("FindDriveItemBinaryById")
                       .AsNoTracking()
                       .AsSplitQuery()
                       .Include(x => x.DriveItemInfo)
                       .Where(x => x.DriveItemInfoId == @driveiteminfoid)
                       .Select(x => x.ToBinary())
                       .FirstOrDefaultAsync();

        if (@binary is null)
        {
            // Log
            string @logData = nameof(DriveItemContent)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItemContent)
                                       + " does not exist");
        }

        return @binary;
    }
}
