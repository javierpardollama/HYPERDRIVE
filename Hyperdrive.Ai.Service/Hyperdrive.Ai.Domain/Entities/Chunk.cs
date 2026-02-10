using Hyperdrive.Ai.Domain.Entities.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Ai.Domain.Entities;

/// <summary>
/// Represents a <see cref="Chunk"/> class. Implements <see cref="IBase"/>, <see cref="IKey"/>
/// </summary>
public class Chunk : IBase, IKey
{
    [BsonId]
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

    [BsonId]
    [BsonElement("document_id")]
    public Guid DocumentId { get; set; }

    [BsonIgnore]
    public virtual Document Document { get; set; }

    [Required]
    [BsonElement("text")]
    public string Text { get; set; }

    [Required]
    [BsonElement("ordinal")]
    public int Ordinal { get; set; }

    [Required]
    [BsonElement("embedding")]
    [BinaryVector(BinaryVectorDataType.Float32)]
    public float[] Embedding { get; set; }
}
