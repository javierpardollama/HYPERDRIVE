using Hyperdrive.Ai.Application.ViewModels.Removes;
using MediatR;

namespace Hyperdrive.Ai.Application.Commands.Documents;

/// <summary>
/// Represents a <see cref="RemoveDocumentCommand" /> class. Implements <see cref="IRequest" />
/// </summary>
public class RemoveDocumentCommand : IRequest
{
    /// <summary>
    /// Gets or Sets <see cref="ViewModel"/>
    /// </summary>
    public ViewRemoveDocument ViewModel { get; set; }
}
