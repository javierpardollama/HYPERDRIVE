using System;
using System.Xml.Serialization;

using Hyperdrive.Tier.ViewModels.Interfaces.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("application-user-token")]
    public class ViewApplicationUserToken : IViewBase
    {
        public ViewApplicationUserToken()
        {
        }

        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }

        [XmlElement("value")]
        public string Value { get; set; }

        [XmlElement("application-user")]
        public virtual ViewApplicationUser ApplicationUser { get; set; }
    }
}
