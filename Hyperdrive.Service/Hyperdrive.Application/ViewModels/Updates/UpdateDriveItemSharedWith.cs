using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Application.ViewModels.Updates;

/// <summary>
/// Represents a <see cref="UpdateDriveItemSharedWith"/> class. Inherits <see cref="UpdateBase"/>
/// </summary>
public class UpdateDriveItemSharedWith : UpdateBase
{
    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserIds"/>
    /// </summary>
    [Required]
    public virtual ICollection<int> ApplicationUserIds { get; set; }
}