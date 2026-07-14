using Hyperdrive.Intelligence.Application.Profiles;
using Hyperdrive.Intelligence.Application.Queries.Interactions;
using Hyperdrive.Intelligence.Application.ViewModels.Views;
using Hyperdrive.Intelligence.Domain.Managers;
using MediatR;

namespace Hyperdrive.Intelligence.Application.Handlers.Interactions;

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
