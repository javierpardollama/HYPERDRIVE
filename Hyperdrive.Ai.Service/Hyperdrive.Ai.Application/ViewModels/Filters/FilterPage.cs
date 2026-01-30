using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Application.ViewModels.Filters;

/// <summary>
///     Represents a <see cref="FilterPage" /> class.
/// </summary>
public class FilterPage
{
    /// <summary>
    ///     Initializes a new Instance of <see cref="FilterPage" />
    /// </summary>
    public FilterPage()
    {
    }

    /// <summary>
    ///     Gets or Sets <see cref="Index" />
    /// </summary>
    [Required]
    public int Index { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Size" />
    /// </summary>
    [Required]
    public int Size { get; set; }
}
