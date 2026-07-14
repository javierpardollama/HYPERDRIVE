using Hyperdrive.Intelligence.Application.Commands.Documents;
using Hyperdrive.Intelligence.Application.Profiles;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using Hyperdrive.Intelligence.Domain.Entities;
using Hyperdrive.Intelligence.Domain.Managers;
using MediatR;

namespace Hyperdrive.Intelligence.Application.Handlers.Documents;

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
        Document @entity = new()
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
