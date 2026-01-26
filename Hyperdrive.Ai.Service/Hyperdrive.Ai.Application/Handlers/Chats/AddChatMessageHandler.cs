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

        var @query = new Entities.Query()
        {
            CreatedBy = request.ViewModel.CreatedBy,
            Text = request.ViewModel.Text,
            User = $@"Question: {request.ViewModel.Text}
                    Context:
                    `{chunks.ToContext()}
                    Instructions:
                    - Answer concisely (<= 6 sentences).
                    - Include citations like [Chunk 2], [Chunk 4] where applicable."
        };
        var @answer = await _chatCompletitionManager.GetCompletionAsync(@query, @chunks);

        var @interaction = new Entities.Interaction()
        {
            ChatId = request.ViewModel.ChatId,
            CreatedBy = request.ViewModel.CreatedBy,
            Query = @query,
            Answer = @answer
        };

        await _interactionManager.AddInteraction(@interaction);

        var @dto = await _interactionManager.ReloadInteractionById(@interaction.Id);

        return @dto.ToViewModel();
    }
}
