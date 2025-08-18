using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Hyperdrive.Infrastructure.Helpers
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
        public static string GetRandomizedString() => Path.GetRandomFileName().Replace(".", "")[..8];

        /// <summary>
        /// Hashes String
        /// </summary>
        /// <param name="string"></param>
        /// <returns>Instance of <see cref="string"/></returns>
        public static string HashString(string @string) => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(@string)));
    }
}
