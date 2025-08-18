using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Hyperdrive.Infrastructure.Helpers
{
    /// <summary>
    /// Represents a <see cref="ClaimHelper"/> class.
    /// </summary>
    public static class ClaimHelper
    {
        /// <summary>
        /// Serializes <see cref="IEnumerable{Claim}"/> to <see cref="Dictionary{string, object}"/>
        /// </summary>
        /// <param name="claims">Injected <see cref="IEnumerable{Claim}"/></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(IEnumerable<Claim> claims) => claims.GroupBy(i => i.Type)
                                                                                                  .ToDictionary(i => i.Key, i => (object)(i.Count() == 1 ? i.First().Value : i.Select(i => i.Value)
                                                                                                  .ToArray()));
    }
}
