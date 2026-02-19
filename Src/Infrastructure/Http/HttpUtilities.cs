namespace DotCache.Infrastructure.Http
{
    public class HttpUtilities
    {
        public static List<string> GetAllIPAddressesFromRequest(HttpContext context)
        {
            string headers = GetHeaderValue("X-Forwarded-For", context);

            if (headers.Equals(string.Empty)) headers = context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            return headers.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        }

        public static string GetIPAddressOfRequest(HttpContext context)
        {
            string originIPAddress = GetAllIPAddressesFromRequest(context).FirstOrDefault(string.Empty);

            if (!IsValidIPAddress(originIPAddress)) return string.Empty;

            return originIPAddress;
        }

        public static string GetHeaderValue(string header, HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(header, out var values)) return values.ToString();
            
            return string.Empty;
        }

        public static bool IsValidIPAddress(string ipAddress)
        {
            return System.Net.IPAddress.TryParse(ipAddress, out _);
        }
    }
}
