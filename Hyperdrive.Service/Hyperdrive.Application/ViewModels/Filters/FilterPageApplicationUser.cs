using System.ComponentModel.DataAnnotations;
using Hyperdrive.Application.ViewModels.Interfaces.Filters;

namespace Hyperdrive.Application.ViewModels.Filters;

/// <summary>
/// Represents a <see cref="FilterPageApplicationUser"/> class. Implements <see cref="IFilterPage"/>
/// </summary>
public class FilterPageApplicationUser : IFilterPage
{
    /// <summary>
    /// Gets or Sets <see cref="Index"/>
    /// </summary>
    [Required]
    public int Index { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Size"/>
    /// </summary>
    [Required]
    public int Size { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="ApplicationUserId"/>
    /// </summary>
    [Required]
    public int ApplicationUserId { get; set; }
}
