using System;
using System.Collections.Generic;
using Hyperdrive.Application.ViewModels.Interfaces.Views;

namespace Hyperdrive.Application.ViewModels.Views
{
    /// <summary>
    /// Represents a <see cref="ViewApplicationUser"/> class. Implements <see cref="IViewKey"/>, <see cref="IViewBase"/>
    /// </summary>
    public class ViewApplicationUser : IViewKey, IViewBase
    {
       
        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        public int Id { get; set; }
   
        /// <summary>
        /// Gets or Sets <see cref="LastModified"/>
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Email"/>
        /// </summary>
        public string Email { get; set; }
   
        /// <summary>
        /// Gets or Sets <see cref="FirstName"/>
        /// </summary>
        public string FirstName { get; set; }
    
        /// <summary>
        /// Gets or Sets <see cref="LastName"/>
        /// </summary>
        public string LastName { get; set; }
   
        /// <summary>
        /// Gets or Sets <see cref="PhoneNumber"/>
        /// </summary>
        public string PhoneNumber { get; set; }
    
        /// <summary>
        /// Gets or Sets <see cref="Initial"/>
        /// </summary>
        public string Initial { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="ApplicationRoles"/>
        /// </summary>
        public virtual ICollection<ViewCatalog> ApplicationRoles { get; set; } = [];
   
        /// <summary>
        /// Gets or Sets <see cref="Token"/>
        /// </summary>
        public virtual ViewToken Token { get; set; }
   
        /// <summary>
        /// Gets or Sets <see cref="RefreshToken"/>
        /// </summary>
        public virtual ViewToken RefreshToken { get; set; }
        
    }
}
