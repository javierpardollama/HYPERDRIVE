using Hyperdrive.Intelligence.Application.Commands.Chats;
using Hyperdrive.Intelligence.Application.Profiles;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using Hyperdrive.Intelligence.Domain.Managers;
using MediatR;
using Chat = Hyperdrive.Intelligence.Domain.Entities.Chat;

namespace Hyperdrive.Intelligence.Application.Handlers.Chats;

public class UpdateChatTitleHandler : IRequestHandler<UpdateChatTitleCommand, ViewChat>
{
    private readonly IChatManager _manager;

    public UpdateChatTitleHandler(IChatManager chatManager)
    {
        _manager = chatManager;
    }

    public async Task<ViewChat> Handle(UpdateChatTitleCommand request, CancellationToken cancellationToken)
    {
        Chat @entity = new()
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
