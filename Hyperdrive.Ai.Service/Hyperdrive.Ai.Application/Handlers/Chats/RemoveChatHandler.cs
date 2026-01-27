using Hyperdrive.Ai.Application.Commands.Chats;
using Hyperdrive.Ai.Domain.Managers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Application.Handlers.Chats;

public class RemoveChatHandler : IRequestHandler<RemoveChatCommand>
{
    private readonly IChatManager _manager;

    public RemoveChatHandler(IChatManager chatManager)
    {
        _manager = chatManager;
    }

    public async Task Handle(RemoveChatCommand request, CancellationToken cancellationToken)
    {
        Entities.Chat @entity = new()
        {
            Id = request.ViewModel.Id,
            DeletedBy = request.ViewModel.DeletedBy,
            DeletedAt = DateTime.UtcNow,
            Deleted = true
        };

        await _manager.RemoveChat(@entity);
    }
}
