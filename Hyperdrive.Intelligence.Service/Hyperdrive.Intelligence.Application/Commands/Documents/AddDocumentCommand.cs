using Hyperdrive.Intelligence.Application.ViewModels.Additions;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Intelligence.Application.Commands.Documents;

/// <summary>
/// Represents a <see cref="AddDocumentCommand" /> class. Implements <see cref="IRequest{ViewDocument}" />
/// </summary>
public class AddDocumentCommand : IRequest<ViewDocument>
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public ViewAddDocument ViewModel { get; set; }
}
