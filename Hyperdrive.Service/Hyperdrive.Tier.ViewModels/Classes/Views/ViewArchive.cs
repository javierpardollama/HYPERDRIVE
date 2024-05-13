using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using Hyperdrive.Tier.ViewModels.Interfaces.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("archive")]
    public class ViewArchive : IViewKey, IViewBase
    {
        public ViewArchive()
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

        [XmlElement("system")]
        public bool System { get; set; }

        [XmlArray("application-user-archives")]
        public virtual ICollection<ViewApplicationUserArchive> ApplicationUserArchives { get; set; }


        [XmlArray("archive-versions")]
        public virtual ICollection<ViewArchiveVersion> ArchiveVersions { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="LastArchiveVersion"/>
        /// </summary>
        [XmlElement("last-archive-version")]
        public virtual ViewArchiveVersion LastArchiveVersion => ArchiveVersions?.AsQueryable().OrderBy(x => x.LastModified.Date).LastOrDefault();

    }
}
