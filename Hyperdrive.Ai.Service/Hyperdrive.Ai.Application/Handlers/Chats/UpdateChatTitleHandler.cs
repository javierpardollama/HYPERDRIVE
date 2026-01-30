using Hyperdrive.Ai.Application.Commands.Chats;
using Hyperdrive.Ai.Application.Profiles;
using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Managers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Application.Handlers.Chats;

public class UpdateChatTitleHandler : IRequestHandler<UpdateChatTitleCommand, ViewChat>
{
    private readonly IChatManager _manager;

    public UpdateChatTitleHandler(IChatManager chatManager)
    {
        _manager = chatManager;
    }

    public async Task<ViewChat> Handle(UpdateChatTitleCommand request, CancellationToken cancellationToken)
    {
        Entities.Chat @entity = new()
        {
            Id = request.ViewModel.Id,
            Title = request.ViewModel.Title,
            ModifiedBy = request.ViewModel.ModifiedBy,
            ModifiedAt = DateTime.UtcNow,
        };

        await _manager.UpdateChat(@entity);

        var @dto = await _manager.ReloadChatById(@entity.Id);

        return @dto.ToViewModel();
    }
}
