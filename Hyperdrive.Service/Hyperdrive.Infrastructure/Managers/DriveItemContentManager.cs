using System;
using System.Threading.Tasks;
using Hyperdrive.Domain.Entities;
using Hyperdrive.Domain.Managers;
using Hyperdrive.Infrastructure.Contexts.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hyperdrive.Infrastructure.Managers;

/// <summary>
/// Represents a <see cref="DriveItemContentManager"/> class. Inherits <see cref="BaseManager"/>. Implements <see cref="IDriveItemContentManager"/>
/// </summary>    
/// <param name="context">Injected <see cref="IApplicationContext"/></param>
/// <param name="logger">Injected <see cref="ILogger{DriveItemContentManager}"/></param>
public class DriveItemContentManager(
    IApplicationContext context,
    ILogger<DriveItemContentManager> logger) : BaseManager(context), IDriveItemContentManager
{
    /// <summary>
    /// Adds Drive Item Content
    /// </summary>
    /// <param name="driveiteminfoid">Injected <see cref="int"/></param>
    /// <param name="type">Injected <see cref="string"/></param>
    /// <param name="size">Injected <see cref="float"/></param>
    /// <param name="data">Injected <see cref="string"/></param>
    /// <param name="folder">Injected <see cref="bool"/></param>
    public async Task AddAsFileContent(int @driveiteminfoid, string @type, float? @size, string @data, bool folder)
    {
        if (folder) return;
        
        DriveItemContent @content = new()
        {
            DriveItemInfoId = @driveiteminfoid,
            Type = @type,
            Size = @size,
            Data = Convert.FromBase64String(@data)
        };

        await Context.DriveItemContents.AddAsync(@content);

        await Context.SaveChangesAsync();

        // Log
        string @logData = nameof(DriveItemContent)
                          + " was added at "
                          + DateTime.UtcNow.ToShortTimeString();

        @logger.LogInformation(@logData);
    }
}