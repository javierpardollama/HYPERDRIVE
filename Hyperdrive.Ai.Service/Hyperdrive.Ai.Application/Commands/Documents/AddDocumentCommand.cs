using Hyperdrive.Ai.Application.ViewModels.Additions;
using Hyperdrive.Ai.Application.ViewModels.Views;
using MediatR;

namespace Hyperdrive.Ai.Application.Commands.Documents
{
    public class AddDocumentCommand : IRequest<ViewDocument>
    {
        /// <summary>
        /// Gets or Sets <see cref="ViewModel"/>
        /// </summary>
        public ViewAddDocument ViewModel { get; set; }
    }
}
