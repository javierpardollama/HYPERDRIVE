using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using Hyperdrive.Tier.ViewModels.Interfaces.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("archive")]
    public class ViewDriveItem : IViewKey, IViewBase
    {
        public ViewDriveItem()
        {
        }

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


        [XmlArray("archive-versions")]
        public virtual ICollection<ViewDriveItemVersion> DriveItemVersions { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="LastDriveItemVersion"/>
        /// </summary>
        [XmlElement("last-archive-version")]
        public virtual ViewDriveItemVersion LastDriveItemVersion => DriveItemVersions?.AsQueryable().OrderBy(x => x.LastModified.Date).LastOrDefault();

    }
}
