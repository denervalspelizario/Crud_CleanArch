using Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.AuthService
{
    public class AuthService(IConfiguration configuration) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateJWT(string email, string username)
        {
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Issuer"];
            var key = _configuration["JWT:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new("Email", email),
                new("Username", username),
                new("EmailIdentifier", email.Split("@").ToString()!),
                new("CurrentTime", DateTime.Now.ToString())
            };

            _ = int.TryParse(_configuration["JWT:TokenExpirationTimeInDays"], out int tokenExpirationTimeInDays);
           
            var token = new JwtSecurityToken(
                issuer: issuer, 
                audience: audience, 
                claims: claims, 
                expires: DateTime.Now.AddDays(tokenExpirationTimeInDays), signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    
        public string GenerateRefreshToken()
        {
            // variavel de array vazio de 128 bytes
            var secureRandomBytes = new byte[128];

            // código cria uma instância de um gerador de números aleatórios seguro
            using var randomNumberGenerator = RandomNumberGenerator.Create();

            // gerando bytes aleatorios e jogando em secureRandomBytes 
            randomNumberGenerator.GetBytes(secureRandomBytes);

            // retornando a secureRandomBytes convertida para ToBase64String
            return Convert.ToBase64String(secureRandomBytes);
        }

        public string HashingPassword(string password)
        {
            // gerando um array de bytes com base SHA256 e o password passado
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            // crio um objeto tipo StringBuilder
            StringBuilder builder = new StringBuilder();

            // percorrendo a array de bytes
            for(int i = 0; i < bytes.Length; i++)
            {
                // adicionando os dados de bytes no formato string em builder(x2 gera uma string representada por hexadecimal)
                builder.Append(bytes[i].ToString("x2"));
            }


            // retorna o builder no formato de string
            return builder.ToString();
        }
    }
}
