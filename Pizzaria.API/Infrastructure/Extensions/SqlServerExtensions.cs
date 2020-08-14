using System.Collections.Generic;

namespace Pizzaria.API.Infrastructure.Extensions
{
    public static class SqlServerExtensions
    {
        internal static string FixConnectionTimeout(this string connectionString)
        {
            var connectionParts = connectionString.Split(";");
            var newConnectionString = new List<string>();
            foreach (var cp in connectionParts)
            {
                if (!cp.ToLower().Contains("timeout") && !string.IsNullOrEmpty(cp))
                    newConnectionString.Add(cp);
            }

            newConnectionString.Add("Connection Timeout=900");
            return string.Join(";", newConnectionString);
        }
    }
}
