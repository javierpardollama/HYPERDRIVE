namespace Hyperdrive.Tier.Constants.Enums
{
    /// <summary>
    /// Represents a <see cref="ApplicationEvents"/> class
    /// </summary>
    public enum ApplicationEvents
    {
        /// <summary>
        /// Insert <see cref="ApplicationEvents"/>
        /// </summary>
        InsertItem = 1,

        /// <summary>
        /// Update <see cref="ApplicationEvents"/>
        /// </summary>
        UpdateItem = 2,

        /// <summary>
        /// Delete <see cref="ApplicationEvents"/>
        /// </summary>
        DeleteItem = 3,

        /// <summary>
        /// Not Found <see cref="ApplicationEvents"/>
        /// </summary>
        GetItemNotFound = 4,

        /// <summary>
        /// Found <see cref="ApplicationEvents"/>
        /// </summary>
        GetItemFound = 5,

        /// <summary>
        /// Authenticated <see cref="ApplicationEvents"/>
        /// </summary>
        UserAuthenticated = 6,

        /// <summary>
        /// Not Authenticated <see cref="ApplicationEvents"/>
        /// </summary>
        UserNotAuthenticated = 7,

        /// <summary>
        /// Password Restored <see cref="ApplicationEvents"/>
        /// </summary>
        PasswordRestored = 8,

        /// <summary>
        /// Email Restored <see cref="ApplicationEvents"/>
        /// </summary>
        EmailRestored = 9,

        /// <summary>
        /// Phone Number Restored <see cref="ApplicationEvents"/>
        /// </summary>
        PhoneNumberRestored = 10
    }
}