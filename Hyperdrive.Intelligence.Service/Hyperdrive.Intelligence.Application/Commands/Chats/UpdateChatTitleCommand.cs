using Hyperdrive.Intelligence.Application.ViewModels.Updates;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Intelligence.Application.Commands.Chats;

/// <summary>
/// Represents a <see cref="UpdateChatTitleCommand" /> class. Implements <see cref="IRequest{ViewChat}" />
/// </summary>
public class UpdateChatTitleCommand : IRequest<ViewChat>
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public ViewUpdateChatTitle ViewModel { get; set; }
}