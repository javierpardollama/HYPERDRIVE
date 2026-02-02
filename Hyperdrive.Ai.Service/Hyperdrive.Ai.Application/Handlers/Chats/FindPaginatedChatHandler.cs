using Hyperdrive.Ai.Application.Profiles;
using Hyperdrive.Ai.Application.Queries.Chats;
using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Application.Handlers.Chats;

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
