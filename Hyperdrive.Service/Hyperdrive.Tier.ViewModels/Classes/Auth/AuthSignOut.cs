﻿using System.ComponentModel.DataAnnotations;

namespace Hyperdrive.Tier.ViewModels.Classes.Auth
{
    /// <summary>
    /// Represents a <see cref="AuthSignOut"/> class.
    /// </summary>
    public class AuthSignOut
    {
        /// <summary>
        /// Gets or Sets <see cref="Email"/>
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
