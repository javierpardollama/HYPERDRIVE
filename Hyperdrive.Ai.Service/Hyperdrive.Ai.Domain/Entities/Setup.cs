using Hyperdrive.Ai.Domain.Entities.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Hyperdrive.Ai.Domain.Entities;

public class Setup : IBase, IKey
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("created_by")]
    public Guid CreatedBy { get; set; }

    [BsonElement("modified_by")]
    public Guid? ModifiedBy { get; set; }

    [BsonElement("modified_at")]
    public DateTime? ModifiedAt { get; set; }

    [BsonElement("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    [BsonElement("deleted_by")]
    public Guid? DeletedBy { get; set; }

    [BsonElement("deleted")]
    public bool Deleted { get; set; } = false;

    [BsonElement("content")]
    public string Content { get; set; } = @"You are a helpful assistant. 
        Answer strictly using the provided context.
        If the answer is not in the context, say “I don’t know.” Cite chunk numbers.";
}
