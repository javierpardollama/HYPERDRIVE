using Hyperdrive.Intelligence.Application.Commands.Chats;
using Hyperdrive.Intelligence.Domain.Managers;
using MediatR;
using Chat = Hyperdrive.Intelligence.Domain.Entities.Chat;

namespace Hyperdrive.Intelligence.Application.Handlers.Chats;

public class RemoveChatHandler : IRequestHandler<RemoveChatCommand>
{
    private readonly IChatManager _manager;

    public RemoveChatHandler(IChatManager chatManager)
    {
        _manager = chatManager;
    }

    public async Task Handle(RemoveChatCommand request, CancellationToken cancellationToken)
    {
        Chat @entity = new()
        {
            Id = request.ViewModel.Id,
            DeletedBy = request.ViewModel.DeletedBy,
            DeletedAt = DateTime.UtcNow,
            Deleted = true
        };

        await _manager.RemoveChat(@entity);
    }
}
