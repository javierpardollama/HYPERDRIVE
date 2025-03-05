using Hyperdrive.Tier.ViewModels.Interfaces.Views;
using System;
using System.Xml.Serialization;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("application-user-role")]
    public class ViewApplicationUserRole : IViewBase, IViewKey
    {
        public ViewApplicationUserRole()
        {
        }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }

        [XmlElement("application-role")]
        public virtual ViewApplicationRole ApplicationRole { get; set; }

        [XmlElement("application-user")]
        public virtual ViewApplicationUser ApplicationUser { get; set; }
    }
}
