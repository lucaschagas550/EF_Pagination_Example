﻿using EF_Pagination_Example.Model;
using EF_Pagination_Example.ViewModels;

namespace EF_Pagination_Example.Business.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseViewModel> LoginAsync(LoginRequestViewModel loginRequestViewModel, CancellationToken cancellationToken);
        Task<LoginResponseViewModel> GenerateJwtAsync(string userId, CancellationToken cancellationToken);
        Task<RefreshToken?> GetRefreshTokenAsync(Guid refreshToken, CancellationToken cancellationToken);
        Task<AppUser> CreateUserAsync(RegisterUserViewModel registerUser, CancellationToken cancellationToken);
    }
}