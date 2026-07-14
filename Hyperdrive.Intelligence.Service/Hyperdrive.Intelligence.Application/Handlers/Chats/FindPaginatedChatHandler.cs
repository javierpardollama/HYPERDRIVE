using Hyperdrive.Intelligence.Application.Profiles;
using Hyperdrive.Intelligence.Application.Queries.Chats;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using Hyperdrive.Intelligence.Domain.Managers;
using MediatR;

namespace Hyperdrive.Intelligence.Application.Handlers.Chats;

public class FindPaginatedChatHandler : IRequestHandler<FindPaginatedChatQuery, ViewPage<ViewChat>>
{
    private readonly IChatManager _manager;

    public FindPaginatedChatHandler(IChatManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewPage<ViewChat>> Handle(FindPaginatedChatQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedChat(
           request.ViewModel.Index,
           request.ViewModel.Size,
           request.ViewModel.UserId
           );

        return @page.ToPageViewModel();

    }
}
