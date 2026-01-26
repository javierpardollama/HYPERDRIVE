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
    [BsonRepresentation(BsonType.Binary)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonId]
    [BsonRepresentation(BsonType.Binary)]
    [BsonElement("created_by")]
    public Guid CreatedBy { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.Binary)]
    [BsonElement("modified_by")]
    public Guid? ModifiedBy { get; set; }

    [BsonElement("modified_at")]
    public DateTime? ModifiedAt { get; set; }

    [BsonElement("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.Binary)]
    [BsonElement("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [BsonElement("deleted")]
    public bool Deleted { get; set; } = false;

    [BsonId]
    [BsonRepresentation(BsonType.Binary)]
    [BsonElement("chat_id")]
    public Guid ChatId { get; set; }

    [BsonIgnore]
    public virtual Chat Chat { get; set; }

    [BsonElement("query")]
    public Query Query { get; set; }

    [BsonElement("answer")]
    public Answer Answer { get; set; }
}
