using Hyperdrive.Tier.ViewModels.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("archive")]
    public class ViewDriveItem : IViewKey, IViewBase
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }

        [XmlElement("by")]
        public virtual ViewApplicationUser By { get; set; }

        [XmlElement("folder")]
        public bool Folder { get; set; }

        [XmlElement("locked")]
        public bool Locked { get; set; }

        [XmlArray("application-user-archives")]
        public virtual ICollection<ViewApplicationUserDriveItem> ApplicationUserDriveItems { get; set; }
    }
}
