using Criptic.Core.Models;

namespace Criptic.Core.Abstractions.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
}