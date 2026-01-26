using System;

namespace Hyperdrive.Ai.Domain.Dtos;

public class SourceDto
{
    public Guid DocumentId { get; set; }

    public string Preview { get; set; }

    public DateTime? LastModified { get; set; }

}
