using Hyperdrive.Main.Domain.Entities;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Domain.Managers;

/// <summary>
/// Represents a <see cref="IDriveItemInfoManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IDriveItemInfoManager : IBaseManager
{
    /// <summary>
    /// Adds Drive Item Info As Name
    /// </summary>
    /// <param name="driveitemid">Injected <see cref="int"/></param>
    /// <param name="name">Injected <see cref="string"/></param>
    /// <param name="extension">Injected <see cref="string"/></param>
    /// <returns>Instance of <see cref="Task{DriveItemInfo}"/></returns>
    public Task<DriveItemInfo> AddAsNameInfo(int @driveitemid, string @name, string @extension);

    /// <summary>
    /// Adds Drive Item Info As FileName
    /// </summary>
    /// <param name="driveitemid">Injected <see cref="int"/></param>
    /// <param name="filename">Injected <see cref="string"/></param>       
    /// <returns>Instance of <see cref="Task{DriveItemInfo}"/></returns>
    public Task<DriveItemInfo> AddAsFileNameInfo(int @driveitemid, string @filename);
}