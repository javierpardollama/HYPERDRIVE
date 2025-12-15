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
        DriveItemBinaryDto @binary = await Context.DriveItems
            .TagWith("FindLatestDriveItemBinaryById")
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Activity)
            .Where(x => x.Id == @driveitemid)        
            .Select(x => x.ToBinary())
            .FirstOrDefaultAsync();

        if (@binary is null)
        {
            // Log
            string @logData = nameof(DriveItem)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItem)
                                       + " does not exist");
        }

        return @binary;
    }

    /// <summary>
    /// Finds Drive Item Binary By Id
    /// </summary>
    /// <param name="driveitemversionid">Injected <see cref="int"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemBinaryDto}"/></returns>
    public async Task<DriveItemBinaryDto> FindDriveItemBinaryById(int @driveitemversionid)
    {
        DriveItemBinaryDto @binary = await Context.DriveItemVersions
                       .TagWith("FindDriveItemBinaryById")
                       .AsNoTracking()
                       .AsSplitQuery()
                       .Where(x => x.Id == @driveitemversionid)
                       .Select(x => x.ToBinary())
                       .FirstOrDefaultAsync();

        if (@binary is null)
        {
            // Log
            string @logData = nameof(DriveItemVersion)
                              + " was not found at "
                              + DateTime.UtcNow.ToShortTimeString();

            @logger.LogWarning(@logData);

            throw new ServiceException(nameof(DriveItemVersion)
                                       + " does not exist");
        }

        return @binary;
    }
}
