using System;
using System.Collections.Generic;

namespace Hyperdrive.Ai.Domain.Dtos;

public class AnswerDto
{
    public string Text { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual ICollection<SourceDto> Sources { get; set; } = [];

}
