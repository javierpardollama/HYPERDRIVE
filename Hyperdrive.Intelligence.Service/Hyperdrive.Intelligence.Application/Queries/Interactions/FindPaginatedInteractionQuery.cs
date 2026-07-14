using Hyperdrive.Intelligence.Application.ViewModels.Filters;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Intelligence.Application.Queries.Interactions;

/// <summary>
/// Represents a <see cref="FindPaginatedInteractionQuery" /> class. Implements <see cref="IRequest{ViewPage{ViewInteraction}}" />
/// </summary>
public class FindPaginatedInteractionQuery : IRequest<ViewPage<ViewInteraction>>
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public FilterPageInteraction ViewModel { get; set; }
}
