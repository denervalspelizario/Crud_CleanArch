using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(45)]
        public string? Title { get; set; }

        [StringLength(120)]
        public string? Description { get; set; }
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? Deadline { get; set; }
        
        public ListCard? List { get; set; }
        
        public StatusCardEnum? Status { get; set; }
    }
}
