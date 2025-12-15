namespace Hyperdrive.Application.ViewModels.Interfaces.Filters;

/// <summary>
/// Represents a <see cref="IFilterPage"/> interface
/// </summary>
public interface IFilterPage
{
    /// <summary>
    /// Gets or Sets <see cref="Index"/>
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Gets or Sets <see cref="Size"/>
    /// </summary>
    public int Size { get; set; }
}
