using Hyperdrive.Tier.ViewModels.Interfaces.Views;
using System;
using System.Xml.Serialization;

namespace Hyperdrive.Tier.ViewModels.Classes.Views
{
    /// <summary>
    /// Represents a <see cref="ViewApplicationUserRefreshToken"/> class. Implements <see cref="IViewKey"/>, <see cref="IViewBase"/>
    /// </summary>
    [XmlRoot("application-user-refresh-token")]
    public class ViewApplicationUserRefreshToken : IViewBase
    {
        /// <summary>
        /// Gets or Sets <see cref="LastModified"/>
        /// </summary>
        [XmlElement("last-modified")]
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ExpiresAt"/>
        /// </summary>
        [XmlElement("expired-at")]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="LoginProvider"/>
        /// </summary>
        [XmlElement("login-provider")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Value"/>
        /// </summary>
        [XmlElement("value")]
        public string Value { get; set; }
    }
}