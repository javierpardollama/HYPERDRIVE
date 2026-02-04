using System.Collections.Generic;

namespace Hyperdrive.Main.Application.ViewModels.Updates;

/// <summary>
/// Represents a <see cref="UpdateApplicationUser"/> class. Inherits <see cref="UpdateBase"/>
/// </summary>
public class UpdateApplicationUser : UpdateBase
{
    /// <summary>
    /// Gets or Sets <see cref="ApplicationRoleNames"/>
    /// </summary>
    public virtual ICollection<string> ApplicationRoleNames { get; set; }
}
