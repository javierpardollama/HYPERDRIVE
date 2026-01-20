using System.Collections.Generic;

namespace Hyperdrive.Ai.Domain.Dtos;

public class RagAnswerDto
{
    public string Answer { get; set; }

    public ICollection<RagSourceDto> Sources { get; set; }
}
