using Microsoft.IdentityModel.Tokens;

namespace Criptic.Core.UserControl;

public class JwtOptions
{
    public string SecretKey { get; } = "ItsMySecretKeyOnDotNetApplicationForLessonsOfOris";
    
    public int ExpiresHours { get; }
}