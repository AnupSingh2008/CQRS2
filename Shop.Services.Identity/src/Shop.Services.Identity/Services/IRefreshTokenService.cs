using System;
using System.Threading.Tasks;
using Shop.Common.Authentication;

namespace Shop.Services.Identity.Services
{
    public interface IRefreshTokenService
    {
        Task AddAsync(Guid userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, Guid userId);
    }
}