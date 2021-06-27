using System;
using System.Threading.Tasks;
using Shop.Common.Authentication;
using Shop.Common.RabbitMq;
using Shop.Common.Types;
using Shop.Services.Identity.Domain;
using Shop.Services.Identity.Messages.Events;
using Shop.Services.Identity.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Shop.Services.Identity.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClaimsProvider _claimsProvider;
        private readonly IBusPublisher _busPublisher;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtHandler jwtHandler,
            IPasswordHasher<User> passwordHasher,
            IClaimsProvider claimsProvider,
            IBusPublisher busPublisher)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
            _claimsProvider = claimsProvider;
            _busPublisher = busPublisher;
        }

        public async Task AddAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ShopException(Codes.UserNotFound, 
                    $"User: '{userId}' was not found.");
            }
            await _refreshTokenRepository.AddAsync(new RefreshToken(user, _passwordHasher));
        }

        public async Task<JsonWebToken> CreateAccessTokenAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null)
            {
                throw new ShopException(Codes.RefreshTokenNotFound, 
                    "Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new ShopException(Codes.RefreshTokenAlreadyRevoked, 
                    $"Refresh token: '{refreshToken.Id}' was revoked.");
            }
            var user = await _userRepository.GetAsync(refreshToken.UserId);
            if (user == null)
            {
                throw new ShopException(Codes.UserNotFound, 
                    $"User: '{refreshToken.UserId}' was not found.");
            }
            var claims = await _claimsProvider.GetAsync(user.Id);
            var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), user.Role, claims);
            jwt.RefreshToken = refreshToken.Token;
            await _busPublisher.PublishAsync(new AccessTokenRefreshed(user.Id), CorrelationContext.Empty);
            
            return jwt;
        }

        public async Task RevokeAsync(string token, Guid userId)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null || refreshToken.UserId != userId)
            {
                throw new ShopException(Codes.RefreshTokenNotFound, 
                    "Refresh token was not found.");
            }
            refreshToken.Revoke();
            await _refreshTokenRepository.UpdateAsync(refreshToken);
            await _busPublisher.PublishAsync(new RefreshTokenRevoked(refreshToken.UserId), CorrelationContext.Empty);
        }
    }
}