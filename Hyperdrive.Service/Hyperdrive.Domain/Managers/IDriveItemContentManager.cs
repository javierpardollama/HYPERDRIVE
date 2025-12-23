using System.Threading.Tasks;

namespace Hyperdrive.Domain.Managers;

/// <summary>
/// Represents a <see cref="IDriveItemContentManager"/> interface. Inherits <see cref="IBaseManager"/>
/// </summary>
public interface IDriveItemContentManager : IBaseManager
{
    /// <summary>
    /// Adds Drive Item Content
    /// </summary>
    /// <param name="driveiteminfoid">Injected <see cref="int"/></param>
    /// <param name="type">Injected <see cref="string"/></param>
    /// <param name="size">Injected <see cref="float"/></param>
    /// <param name="data">Injected <see cref="string"/></param>
    public Task AddAsFileContent(int @driveiteminfoid, string @type, float? @size, string @data);

}