using Hyperdrive.Ai.Application.ViewModels.Additions;
using Hyperdrive.Ai.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Ai.Application.Commands.Chats;

/// <summary>
/// Represents a <see cref="AddChatCommand" /> class. Implements <see cref="IRequest{ViewChat}" />
/// </summary>
public class AddChatCommand : IRequest<ViewChat>
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public ViewAddChat ViewModel { get; set; }
}
