using Hyperdrive.Ai.Domain.Entities.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Hyperdrive.Ai.Domain.Entities;

public class Query : IBase, IKey
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

    [BsonElement("text")]
    public string Text { get; set; }

    [BsonElement("api_context")]
    public string Context { get; set; }

    [BsonElement("api_text")]
    public string Content { get; set => field = $@"Question: {Text}
                    Context:
                    `{Context}
                    Instructions:
                    - Answer concisely (<= 6 sentences).
                    - Include citations like [Chunk 2], [Chunk 4] where applicable."; }
}
