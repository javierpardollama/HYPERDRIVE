using Hyperdrive.Tier.ViewModels.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("application-role")]
    public class ViewApplicationRole : IViewKey, IViewBase
    {
        public ViewApplicationRole()
        {
        }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("image-uri")]
        public string ImageUri { get; set; }

        [XmlArray("application-user-roles")]
        public virtual ICollection<ViewApplicationUserRole> ApplicationUserRoles { get; set; }

        [XmlArray("application-users")]
        public virtual ICollection<ViewApplicationUser> ApplicationUsers => ApplicationUserRoles?.AsQueryable().Select(x => x.ApplicationUser).ToList();
    }
}
