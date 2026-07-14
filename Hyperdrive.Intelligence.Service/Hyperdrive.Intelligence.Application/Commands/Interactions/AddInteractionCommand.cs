using Hyperdrive.Intelligence.Application.ViewModels.Additions;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Intelligence.Application.Commands.Interactions;

/// <summary>
/// Represents a <see cref="AddInteractionCommand" /> class. Implements <see cref="IRequest{ViewInteraction}" />
/// </summary>
public class AddInteractionCommand : IRequest<ViewInteraction>
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public ViewAddInteraction ViewModel { get; set; }
}
