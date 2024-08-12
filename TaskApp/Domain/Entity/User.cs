using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Usuario")]
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Column("Nome",TypeName = "nvarchar(50)")]
        public string? Name { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Column("Sobrenome",TypeName = "nvarchar(50)")]
        public string? Surname { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Username { get; set; }

        public ICollection<Workspace>? Workspaces { get; set; }
        public string? RefresToken { get; set; }
        public DateTime? RefreshTokenExpirationTime { get; set; }
    }
}
