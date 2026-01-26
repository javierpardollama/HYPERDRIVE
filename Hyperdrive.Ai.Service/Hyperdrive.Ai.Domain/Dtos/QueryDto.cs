using System;

namespace Hyperdrive.Ai.Domain.Dtos;

public class QueryDto
{
    public string Text { get; set; }

    public DateTime? LastModified { get; set; }
}
