using System;
using Hyperdrive.Application.ViewModels.Interfaces.Views;

namespace Hyperdrive.Application.ViewModels.Views
{
    /// <summary>
    /// Represents a <see cref="ViewApplicationUserToken"/> class. Implements <see cref="IViewKey"/>, <see cref="IViewBase"/>
    /// </summary>
    public class ViewApplicationUserToken : IViewKey, IViewBase 
    {

        /// <summary>
        /// Gets or Sets <see cref="Id"/>
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or Sets <see cref="LastModified"/>
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Name"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="LoginProvider"/>
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or Sets <see cref="Value"/>
        /// </summary>
        public string Value { get; set; }
    }
}