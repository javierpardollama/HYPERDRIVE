using Hyperdrive.Ai.Application.Commands.Chats;
using Hyperdrive.Ai.Application.Profiles;
using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Application.Handlers.Chats;

public class AddChatHandler : IRequestHandler<AddChatCommand, ViewChat>
{
    private readonly IChatManager _manager;

    public AddChatHandler(IChatManager chatManager)
    {
        _manager = chatManager;
    }

    public async Task<ViewChat> Handle(AddChatCommand request, CancellationToken cancellationToken)
    {
        Entities.Chat @entity = new()
        {
            CreatedBy = request.ViewModel.CreatedBy
        };

        await _manager.AddChat(@entity);

        var @dto = await _manager.ReloadChatById(@entity.Id);

        return @dto.ToViewModel();
    }
}
