using System;

namespace Hyperdrive.Ai.Application.ViewModels.Views;

/// <summary>
///     Represents a <see cref="ViewDocument" /> class.
/// </summary>
public class ViewDocument
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>  
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>   
    public DateTime? LastModified { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Name" />
    /// </summary>  
    public string Name { get; set; }
}
