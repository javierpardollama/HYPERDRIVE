using System;
using System.Collections.Generic;
using Hyperdrive.Application.ViewModels.Interfaces.Views;

namespace Hyperdrive.Application.ViewModels.Views
{
    public class ViewApplicationRole : IViewKey, IViewBase
    {
        public int Id { get; set; }
      
        public DateTime? LastModified { get; set; }
      
        public string Name { get; set; }
     
        public string ImageUri { get; set; }
       
        public virtual ICollection<ViewCatalog> ApplicationUsers { get; set; } = [];
    }
}
