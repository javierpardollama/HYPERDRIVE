using Hyperdrive.Ai.Application.Commands.Documents;
using Hyperdrive.Ai.Application.Profiles;
using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Application.Handlers.Documents;

/// <summary>
/// Represents a <see cref="AddDocumentHandler"/>. Implements <see cref="IRequestHandler{AddDocumentCommand, ViewDocument}"/>
/// </summary>
public class AddDocumentHandler : IRequestHandler<AddDocumentCommand, ViewDocument>
{
    private readonly IDocumentManager _documentManager;
    private readonly IChunkManager _chunkManager;

    /// <summary>
    ///  Initializes a new Instance of <see cref="AddDocumentHandler" />
    /// </summary>
    /// <param name="documentManager">Injected <see cref="IDocumentManager"/></param>
    /// <param name="chunkManager">Injected <see cref="IChunkManager"/></param>
    public AddDocumentHandler(IDocumentManager documentManager, IChunkManager chunkManager)
    {
        _documentManager = documentManager;
        _chunkManager = chunkManager;
    }

    public async Task<ViewDocument> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
    {
        Entities.Document @entity = new()
        {
            FileName = request.ViewModel.Name,
            FileId = request.ViewModel.Id,
            CreatedBy = request.ViewModel.CreatedBy,
        };

        await _documentManager.CheckFileId(@entity.FileId);

        var @document = await _documentManager.AddDocument(@entity);

        await _chunkManager.AddChunks(document.Id, request.ViewModel.Content);

        var @dto = await _documentManager.ReloadDocumentById(@document.Id);

        return @dto.ToViewModel();
    }
}
