using Abstractions;

namespace BackEndAdminPro.Configuration
{
    public class TokenParameters : ITokenParameters
    {
        public string Email { get ; set ; }
        public string PasswordHash { get ; set ; }
        public string Id { get ; set ; }
    }
}
