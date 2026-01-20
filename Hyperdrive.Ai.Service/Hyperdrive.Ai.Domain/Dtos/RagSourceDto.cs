using System;

namespace Hyperdrive.Ai.Domain.Dtos;

public class RagSourceDto
{
    public Guid DocumentId { get; set; }

    public string Preview { get; set; }
}
