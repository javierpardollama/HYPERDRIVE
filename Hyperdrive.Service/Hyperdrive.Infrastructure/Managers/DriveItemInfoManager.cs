using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hyperdrive.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemInfoManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDriveItemInfoManager"/>
/// </summary>    
/// <param name="context">Injected <see cref="IApplicationContext"/></param>
/// <param name="logger">Injected <see cref="ILogger{DriveItemInfoManager}"/></param>
public class DriveItemInfoManager(
    IApplicationContext context,
ILogger<DriveItemInfoManager> logger) : BaseManager(context), IDriveItemInfoManager
{
    /// <summary>
    /// Adds Activity
    /// </summary>
    /// <param name="driveitemid">Injected <see cref="int"/></param>
    /// <param name="name">Injected <see cref="string"/></param>
    /// <param name="extension">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemInfo}"/></returns>
    public async Task<DriveItemInfo> AddAsNameInfo(int @driveitemid, string @name, string @extension)
    {
        DriveItemInfo @version = new()
        {
            Extension = @extension.Trim(),
            NormalizedExtension = @extension.Trim().ToUpper(),
            FileName = $"{@name?.Trim()}.{extension.Trim()}",
            NormalizedFileName = $"{@name?.Trim().ToUpper()}.{extension.Trim().ToUpper()}",
            Name = @name?.Trim(),
            NormalizedName = @name?.Trim().ToUpper(),
            DriveItemId = @driveitemid
        };

        await Context.DriveItemInfos.AddAsync(@version);

        await Context.SaveChangesAsync();

        // Log
        string @logData = nameof(DriveItemInfo)                             
                          + " was added at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @version;
    }
    
    /// <summary>
    /// Adds Drive Item Info
    /// </summary>
    /// <param name="driveitemid">Injected <see cref="int"/></param>
    /// <param name="filename">Injected <see cref="string"/></param>       
    /// <returns>Instance of <see cref="Task{DriveItemInfo}"/></returns>
    public async Task<DriveItemInfo> AddAsFileNameInfo(int @driveitemid, string @filename)
    {
        DriveItemInfo @version = new()
        {
            FileName = @filename.Trim(),
            NormalizedFileName = @filename.Trim().ToUpper(),
            Name = Path.GetFileNameWithoutExtension(@filename.Trim()),
            NormalizedName = Path.GetFileNameWithoutExtension(@filename.Trim()).ToUpper(),
            Extension = Path.GetExtension(@filename.Trim()),
            NormalizedExtension = Path.GetExtension(@filename.Trim()).ToUpper(),           
            DriveItemId = @driveitemid
        };

        await Context.DriveItemInfos.AddAsync(@version);

        await Context.SaveChangesAsync();

        // Log
        string @logData = nameof(DriveItemInfo)                             
                          + " was added at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);

        return @version;
    }

}