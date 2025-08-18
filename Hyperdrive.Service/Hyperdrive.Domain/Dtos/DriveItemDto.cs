using System;
using System.Collections.Generic;
using Hyperdrive.Domain.Dtos.Interfaces;

namespace Hyperdrive.Domain.Dtos;

/// <summary>
/// Represents a <see cref="DriveItemDto"/> class. Implements <see cref="IKeyDto"/>, <see cref="IBaseDto"/>
/// </summary>
public class DriveItemDto : IKeyDto, IBaseDto
{
    public int Id { get; set; }
    
    public DateTime LastModified { get; set; }
   
    public CatalogDto By { get; set; }
    
    public bool Folder { get; set; }
    
    public bool Locked { get; set; }
   
    public string Name { get; set; }
    
    public virtual ICollection<CatalogDto> SharedWith { get; set; }
}