using Hyperdrive.Ai.Application.ViewModels.Additions;
using Hyperdrive.Ai.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Ai.Application.Commands.Messages;

/// <summary>
/// Represents a <see cref="AddChatMessageCommand" /> class. Implements <see cref="IRequest{ViewInteraction}" />
/// </summary>
public class AddChatMessageCommand : IRequest<ViewInteraction>
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public ViewAddChatMessage ViewModel { get; set; }
}
