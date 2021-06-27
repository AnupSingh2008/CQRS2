using Shop.Common.Authentication;

namespace Shop.Api.Framework
{
    public class AdminAuth : JwtAuthAttribute
    {
        public AdminAuth() : base("admin")
        {
        }
    }
}