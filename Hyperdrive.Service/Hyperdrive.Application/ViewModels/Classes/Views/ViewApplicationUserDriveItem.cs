using Hyperdrive.Tier.ViewModels.Interfaces.Views;
using System;
using System.Xml.Serialization;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("application-user-archive")]
    public class ViewApplicationUserDriveItem : IViewBase, IViewKey
    {
 
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }

        [XmlElement("archive")]
        public virtual ViewDriveItem DriveItem { get; set; }

        [XmlElement("application-user")]
        public virtual ViewApplicationUser ApplicationUser { get; set; }
    }
}
