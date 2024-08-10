using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ListCard
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(45, MinimumLength = 2)]
        public string? Title { get; set; }

        public StatusItemEnum Status  { get; set; } = StatusItemEnum.Active;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public Workspace? workspace { get; set; }
        
        public ICollection<Card>? Cards  { get; set; }
    }
}
