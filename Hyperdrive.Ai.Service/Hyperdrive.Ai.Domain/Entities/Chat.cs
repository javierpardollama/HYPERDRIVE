using Hyperdrive.Ai.Domain.Entities.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Hyperdrive.Ai.Domain.Entities;

/// <summary>
/// Represents a <see cref="Chat"/> class. Implements <see cref="IBase"/>, <see cref="IKey"/>
/// </summary>
public class Chat : IBase, IKey
{
    [BsonId]
    [BsonRepresentation(BsonType.Binary)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("created_by")]
    public Guid CreatedBy { get; set; }

    [BsonElement("modified_at")]
    public DateTime? ModifiedAt { get; set; }

    [BsonElement("modified_by")]
    public Guid? ModifiedBy { get; set; }

    [BsonElement("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [BsonElement("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [BsonElement("deleted")]
    public bool Deleted { get; set; } = false;

    [BsonElement("title")]
    public string Title { get; set; }

    public virtual ICollection<Interaction> Interactions { get; set; } = [];

}
