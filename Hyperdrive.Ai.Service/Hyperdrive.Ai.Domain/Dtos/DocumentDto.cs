using System;

namespace Hyperdrive.Ai.Domain.Dtos;

/// <summary>
///     Represents a <see cref="DocumentDto" /> class
/// </summary>
public class DocumentDto
{
    /// <summary>
    ///     Gets or Sets <see cref="Id" />
    /// </summary>  
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="LastModified" />
    /// </summary>   
    public DateTime? LastModified { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Name" />
    /// </summary>  
    public string Name { get; set; }
}
