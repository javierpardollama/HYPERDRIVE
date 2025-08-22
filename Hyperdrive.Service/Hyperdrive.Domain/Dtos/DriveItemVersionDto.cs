using System;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="DriveItemVersionDto"/> class.
/// </summary>
public class DriveItemVersionDto
{
    /// <summary>
    /// Gets or Sets <see cref="Id"/>
    /// </summary>
    public int Id { get; set; }
  
    /// <summary>
    /// Gets or Sets <see cref="Data"/>
    /// </summary>
    public string Data { get; set; }
   
    /// <summary>
    /// Gets or Sets <see cref="Size"/>
    /// </summary>
    public float? Size { get; set; }
   
    /// <summary>
    /// Gets or Sets <see cref="Type"/>
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="LastModified"/>
    /// </summary>
    public DateTime LastModified { get; set; }
}