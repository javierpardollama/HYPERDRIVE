using Hyperdrive.Ai.Application.Profiles;
using Hyperdrive.Ai.Application.Queries.Interactions;
using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Managers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Application.Handlers.Chats;

public class FindPaginatedInteractionHandler : IRequestHandler<FindPaginatedInteractionQuery, ViewPage<ViewInteraction>>
{
    private readonly IInteractionManager _manager;

    public FindPaginatedInteractionHandler(IInteractionManager manager)
    {
        _manager = manager;
    }

    public async Task<ViewPage<ViewInteraction>> Handle(FindPaginatedInteractionQuery request, CancellationToken cancellationToken)
    {
        var @page = await _manager.FindPaginatedInteraction(
           request.ViewModel.Index,
           request.ViewModel.Size,
           request.ViewModel.ChatId
           );

        return @page.ToPageViewModel();

    }
}
