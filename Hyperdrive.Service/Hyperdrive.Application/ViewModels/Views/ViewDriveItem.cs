using System;
using System.Collections.Generic;
using Hyperdrive.Application.ViewModels.Interfaces.Views;

namespace Hyperdrive.Application.ViewModels.Views
{
    public class ViewDriveItem : IViewKey, IViewBase
    {
        public int Id { get; set; }
      
        public DateTime? LastModified { get; set; }
      
        public ViewCatalog By { get; set; }
       
        public bool Folder { get; set; }
       
        public bool Locked { get; set; }
        
        public string Name { get; set; }
        
        public ViewCatalog Parent { get; set; }
       
        public virtual ICollection<ViewCatalog> SharedWith { get; set; } = [];
    }
}
