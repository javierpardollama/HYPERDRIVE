namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="ApplicationRoleDto"/> class.
/// </summary>
public class DriveItemBinaryDto
{
    /// <summary>
    /// Gets or Sets <see cref="FileName"/>
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Data"/>
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Type"/>
    /// </summary>
    public string Type { get; set; }
}