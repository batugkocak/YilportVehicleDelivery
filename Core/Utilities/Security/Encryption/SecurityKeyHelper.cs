using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption;

// This helper returns our SecurityKey on appsettings.json as byte array with SymmetricSecurityKey type casting
public class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}