using System;
using System.Security.Cryptography;
using System.Text;

namespace Hyperdrive.Tier.Helpers.Classes
{
    /// <summary>
    /// Represents a <see cref="StringHelper"/> class.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Gets Randomized Strings
        /// </summary>
        /// <returns>Instance of <see cref="string"/></returns>
        public static string GetRandomizedString()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Hashes String
        /// </summary>
        /// <param name="string"></param>
        /// <returns>Instance of <see cref="string"/></returns>
        public static string HashString(string @string) => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(@string)));
    }
}
