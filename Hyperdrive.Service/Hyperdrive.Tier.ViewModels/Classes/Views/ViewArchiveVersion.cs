using System;
using System.Xml.Serialization;

using Hyperdrive.Tier.ViewModels.Interfaces.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("archive-version")]
    public class ViewArchiveVersion : IViewKey, IViewBase
    {
        public ViewArchiveVersion()
        {
        }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("data")]
        public byte[] Data { get; set; }

        [XmlElement("size")]
        public float Size { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }

        [XmlElement("archive")]
        public virtual ViewArchive Archive { get; set; }
    }
}
