using Hyperdrive.Intelligence.Application.Commands.Chats;
using Hyperdrive.Intelligence.Application.Profiles;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using Hyperdrive.Intelligence.Domain.Managers;
using MediatR;
using Chat = Hyperdrive.Intelligence.Domain.Entities.Chat;

namespace Hyperdrive.Intelligence.Application.Handlers.Chats;

public class AddChatHandler : IRequestHandler<AddChatCommand, ViewChat>
{
    private readonly IChatManager _manager;

    public AddChatHandler(IChatManager chatManager)
    {
        _manager = chatManager;
    }

    public async Task<ViewChat> Handle(AddChatCommand request, CancellationToken cancellationToken)
    {
        Chat @entity = new()
        {
            CreatedBy = request.ViewModel.CreatedBy
        };

        await _manager.AddChat(@entity);

        var @dto = await _manager.ReloadChatById(@entity.Id);

        return @dto.ToViewModel();
    }
}
