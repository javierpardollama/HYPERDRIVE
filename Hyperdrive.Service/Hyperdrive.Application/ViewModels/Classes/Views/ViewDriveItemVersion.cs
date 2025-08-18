using Hyperdrive.Tier.ViewModels.Interfaces.Views;
using System;
using System.Xml.Serialization;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("archive-version")]
    public class ViewDriveItemVersion : IViewKey, IViewBase
    {
  
        [XmlElement("id")]
        public int Id { get; set; }
       
        [XmlElement("data")]
        public string Data { get; set; }

        [XmlElement("size")]
        public float Size { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }
    }
}
