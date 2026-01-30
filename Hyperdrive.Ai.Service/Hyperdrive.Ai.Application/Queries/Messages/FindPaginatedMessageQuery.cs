using Hyperdrive.Ai.Application.ViewModels.Filters;
using Hyperdrive.Ai.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Ai.Application.Queries.Messages;

/// <summary>
/// Represents a <see cref="FindPaginatedMessageQuery" /> class. Implements <see cref="IRequest{ViewPage{ViewInteraction}}" />
/// </summary>
public class FindPaginatedMessageQuery : IRequest<ViewPage<ViewInteraction>>
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public FilterPage ViewModel { get; set; }
}