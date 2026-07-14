using Hyperdrive.Intelligence.Application.Commands.Documents;
using Hyperdrive.Intelligence.Domain.Entities;
using Hyperdrive.Intelligence.Domain.Managers;
using MediatR;

namespace Hyperdrive.Intelligence.Application.Handlers.Documents;

public class RemoveDocumentHandler : IRequestHandler<RemoveDocumentCommand>
{
    private readonly IDocumentManager _manager;

    public RemoveDocumentHandler(IDocumentManager chatManager)
    {
        _manager = chatManager;
    }

    public async Task Handle(RemoveDocumentCommand request, CancellationToken cancellationToken)
    {
        Document @entity = new()
        {
            Id = request.ViewModel.Id,
            DeletedBy = request.ViewModel.DeletedBy,
            DeletedAt = DateTime.UtcNow,
            Deleted = true
        };

        await _manager.RemoveDocument(@entity);
    }
}
