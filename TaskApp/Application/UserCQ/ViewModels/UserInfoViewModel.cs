namespace Application.UserCQ.ViewModels
{
    // Dados de Saida
    public record UserInfoViewModel
    {
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? RefresToken { get; set; }
        public DateTime? RefreshTokenExpirationTime { get; set; }
        public string? TokenJWT { get; set; }
    }
}
