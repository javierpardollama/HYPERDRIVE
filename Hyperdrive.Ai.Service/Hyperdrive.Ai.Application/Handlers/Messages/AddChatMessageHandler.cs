using Hyperdrive.Ai.Application.Commands.Messages;
using Hyperdrive.Ai.Application.Profiles;
using Hyperdrive.Ai.Application.ViewModels.Views;
using Hyperdrive.Ai.Domain.Dtos;
using Hyperdrive.Ai.Domain.Managers;
using Hyperdrive.Ai.Domain.Profiles;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities = Hyperdrive.Ai.Domain.Entities;

namespace Hyperdrive.Ai.Application.Handlers.Messages;

public class AddChatMessageHandler : IRequestHandler<AddChatMessageCommand, ViewInteraction>
{
    private readonly IChunkManager _chunkManager;
    private readonly IChatCompletitionManager _chatCompletitionManager;
    private readonly IInteractionManager _interactionManager;
    private readonly IChatMessageManager _chatMessageManager;
    private readonly IChatSummaryManager _chatSummaryManager;

    public AddChatMessageHandler(IChunkManager chunkManager,
                                 IChatCompletitionManager chatCompletitionManager,
                                 IInteractionManager interactionManager,
                                 IChatMessageManager chatMessageManager,
                                 IChatSummaryManager chatSummaryManager)
    {
        _chunkManager = chunkManager;
        _chatCompletitionManager = chatCompletitionManager;
        _interactionManager = interactionManager;
        _chatMessageManager = chatMessageManager;
        _chatSummaryManager = chatSummaryManager;
    }

    public async Task<ViewInteraction> Handle(AddChatMessageCommand request, CancellationToken cancellationToken)
    {
        var @messages = new List<ChatMessageDto>();

        var @interaction = new Entities.Interaction()
        {
            ChatId = request.ViewModel.ChatId,
            CreatedBy = request.ViewModel.CreatedBy
        };

        var @chunks = await _chunkManager.FindByText(request.ViewModel.Text);

        var @previous = await _chatMessageManager.FindLatestChatMessagesByChatId(request.ViewModel.ChatId);

        if (previous is { Count: >= 10 })
        {
            var @summary = new Entities.Summary()
            {
                CreatedBy = request.ViewModel.CreatedBy,
                Content = await _chatSummaryManager.GetChatSummaryAsync(@previous)
            };

            @interaction.Summary = @summary;

            @messages = [.. @messages, @summary.ToMessageDto()];
        }

        var @setup = new Entities.Setup()
        {
            CreatedBy = request.ViewModel.CreatedBy
        };

        @interaction.Setup = @setup;
        @messages = [.. @messages, @setup.ToMessageDto()];

        var @query = new Entities.Query()
        {
            CreatedBy = request.ViewModel.CreatedBy,
            Text = request.ViewModel.Text,
            Context = chunks.ToContext()
        };

        @interaction.Query = @query;
        @messages = [.. @messages, @query.ToMessageDto()];

        var @answer = new Entities.Answer()
        {
            CreatedBy = request.ViewModel.CreatedBy,
            Sources = [.. chunks.Select(c => c?.ToSource())],
            Content = await _chatCompletitionManager.GetCompletionAsync(messages)
        };

        @interaction.Answer = @answer;

        await _interactionManager.AddInteraction(@interaction);

        var @dto = await _interactionManager.ReloadInteractionById(@interaction.Id);

        return @dto.ToViewModel();
    }
}
