using Hyperdrive.Ai.Domain.Entities.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Hyperdrive.Ai.Domain.Entities;

/// <summary>
/// Represents a <see cref="Interaction"/> class. Implements <see cref="IBase"/>, <see cref="IKey"/>
/// </summary>
public class Interaction : IBase, IKey
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonId]
    [BsonElement("created_by")]
    public Guid CreatedBy { get; set; }

    [BsonId]
    [BsonElement("modified_by")]
    public Guid? ModifiedBy { get; set; }

    [BsonElement("modified_at")]
    public DateTime? ModifiedAt { get; set; }

    [BsonElement("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [BsonId]
    [BsonElement("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [BsonElement("deleted")]
    public bool Deleted { get; set; } = false;

    [BsonId]
    [BsonElement("chat_id")]
    public Guid ChatId { get; set; }

    [BsonIgnore]
    public virtual Chat Chat { get; set; }

    [BsonElement("setup")]
    public Setup Setup { get; set; }

    [BsonElement("summary")]
    public Summary Summary { get; set; }

    [BsonElement("query")]
    public Query Query { get; set; }

    [BsonElement("answer")]
    public Answer Answer { get; set; }
}
