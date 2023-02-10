using Microsoft.AspNetCore.Authorization;

namespace HCAuthTest
{
    /// <summary>
    /// Checks if the user has an active subscription
    /// </summary>
    public class AssinaturaValidaRequiment : IAuthorizationRequirement
    {
    }
}
