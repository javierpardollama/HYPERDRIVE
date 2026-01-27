using Hyperdrive.Ai.Application.ViewModels.Removes;
using MediatR;

namespace Hyperdrive.Ai.Application.Commands.Chats;

/// <summary>
/// Represents a <see cref="RemoveChatCommand" /> class. Inherits <see cref="IRequest" />
/// </summary>
public class RemoveChatCommand : IRequest
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public ViewRemoveChat ViewModel { get; set; }
}
