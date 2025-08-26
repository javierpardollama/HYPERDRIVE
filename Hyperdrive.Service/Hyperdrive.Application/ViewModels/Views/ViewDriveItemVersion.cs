using System;
using Hyperdrive.Application.ViewModels.Interfaces.Views;

namespace Hyperdrive.Application.ViewModels.Views
{
    public class ViewDriveItemVersion : IViewKey, IViewBase
    {
        public int Id { get; set; }
       
       
        public string Data { get; set; }

        
        public float? Size { get; set; }

       
        public string Type { get; set; }
        
        public DateTime? LastModified { get; set; }
    }
}
