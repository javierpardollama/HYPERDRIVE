using Hyperdrive.Ai.Application.Commands.Chats;
using Hyperdrive.Ai.Application.Profiles;
using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Domain.Profiles;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Application.Handlers.Chats;

public class AddChatMessageHandler : IRequestHandler<AddChatMessageCommand, ViewInteraction>
{
    private readonly IChunkManager _chunkManager;
    private readonly IChatCompletitionManager _chatCompletitionManager;
    private readonly IInteractionManager _interactionManager;

    public AddChatMessageHandler(IChunkManager chunkManager,
                                 IChatCompletitionManager chatCompletitionManager,
                                 IInteractionManager interactionManager)
    {
        _chunkManager = chunkManager;
        _chatCompletitionManager = chatCompletitionManager;
        _interactionManager = interactionManager;
    }

    public async Task<ViewInteraction> Handle(AddChatMessageCommand request, CancellationToken cancellationToken)
    {
        var @chunks = await _chunkManager.FindByText(request.ViewModel.Text);

        var @arrange = new Entities.Arrange()
        {
            CreatedBy = request.ViewModel.CreatedBy
        };

        var @query = new Entities.Query()
        {
            CreatedBy = request.ViewModel.CreatedBy,
            Text = request.ViewModel.Text,
            Context = chunks.ToContext()
        };

        var @interaction = new Entities.Interaction()
        {
            ChatId = request.ViewModel.ChatId,
            CreatedBy = request.ViewModel.CreatedBy,
            Arrange = @arrange,
            Query = @query
        };

        @interaction = await _chatCompletitionManager.GetCompletionAsync(@interaction, @chunks);

        await _interactionManager.AddInteraction(@interaction);

        var @dto = await _interactionManager.ReloadInteractionById(@interaction.Id);

        return @dto.ToViewModel();
    }
}
