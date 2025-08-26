using System;
using Hyperdrive.Application.ViewModels.Interfaces.Views;

namespace Hyperdrive.Application.ViewModels.Views;

public class ViewCatalog : IViewKey, IViewBase
{
    /// <summary>
    /// Gets or Sets <see cref="Id"/>
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="LastModified"/>
    /// </summary>
    public DateTime? LastModified { get; set; }
    
    /// <summary>
    /// Gets or Sets <see cref="Name"/>
    /// </summary>
    public string Name { get; set; }
}