using Hyperdrive.Ai.Application.ViewModels.Filters;
using Hyperdrive.Ai.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Ai.Application.Queries.Chats;

/// <summary>
/// Represents a <see cref="FindPaginatedChatQuery" /> class. Implements <see cref="IRequest{ViewPage{ViewChat}}" />
/// </summary>
public class FindPaginatedChatQuery : IRequest<ViewPage<ViewChat>>
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public FilterPageChat ViewModel { get; set; }
}

