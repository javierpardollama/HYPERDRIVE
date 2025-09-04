using System;
using System.Collections.Generic;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="DriveItemDto"/> class.
/// </summary>
public class DriveItemDto
{
    public int Id { get; set; }
    
    public DateTime? LastModified { get; set; }
   
    public CatalogDto By { get; set; }
    
    public bool Folder { get; set; }
   
    public string FileName { get; set; }
    
    public string Name { get; set; }
    
    public string Extension { get; set; }
    
    public CatalogDto Parent { get; set; }
    
    public virtual ICollection<CatalogDto> SharedWith { get; set; } = [];
}