using System;
using System.Threading.Tasks;
using Shop.Common.Authentication;
using Shop.Services.Identity.Domain;

namespace Shop.Services.Identity.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);         
    }
}