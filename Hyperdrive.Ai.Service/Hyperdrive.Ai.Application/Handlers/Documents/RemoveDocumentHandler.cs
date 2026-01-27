using Hyperdrive.Ai.Application.Commands.Documents;
using Hyperdrive.Ai.Domain.Managers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Application.Handlers.Documents;

public class RemoveDocumentHandler : IRequestHandler<RemoveDocumentCommand>
{
    private readonly IDocumentManager _manager;

    public RemoveDocumentHandler(IDocumentManager chatManager)
    {
        _manager = chatManager;
    }

    public async Task Handle(RemoveDocumentCommand request, CancellationToken cancellationToken)
    {
        Entities.Document @entity = new()
        {
            Id = request.ViewModel.Id,
            DeletedBy = request.ViewModel.DeletedBy,
            DeletedAt = DateTime.UtcNow,
            Deleted = true
        };

        await _manager.RemoveDocument(@entity);
    }
}
