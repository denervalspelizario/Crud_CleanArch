﻿namespace Domain.Abstractions
{
    public interface IAuthService
    {
        public string GenerateJWT(string email, string username);

        public string GenerateRefreshToken();

        public string HashingPassword(string password);
    }
}
