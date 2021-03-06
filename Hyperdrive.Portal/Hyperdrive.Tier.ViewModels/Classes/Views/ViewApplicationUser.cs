﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using Hyperdrive.Tier.ViewModels.Interfaces.Views;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    [XmlRoot("application-user")]
    public class ViewApplicationUser : IViewKey, IViewBase
    {
        public ViewApplicationUser()
        {
        }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("initial")]
        public string Initial => Email?.Substring(0, 1).ToUpper();

        [XmlArray("application-user-roles")]
        public virtual ICollection<ViewApplicationUserRole> ApplicationUserRoles { get; set; }

        [XmlArray("appplication-roles")]
        public virtual ICollection<ViewApplicationRole> ApplicationRoles => ApplicationUserRoles?.AsQueryable().Select(x => x.ApplicationRole).ToList();

        [XmlArray("application-user-tokens")]
        public virtual ICollection<ViewApplicationUserToken> ApplicationUserTokens { get; set; }

        [XmlElement("application-user-token")]
        public virtual ViewApplicationUserToken ApplicationUserToken => ApplicationUserTokens?.AsQueryable().LastOrDefault();

        [XmlArray("application-user-archives")]
        public virtual ICollection<ViewApplicationUserArchive> ApplicationUserArchives { get; set; }

    }
}
