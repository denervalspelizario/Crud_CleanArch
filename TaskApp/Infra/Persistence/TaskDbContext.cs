using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence
{
    public class TaskDbContext(DbContextOptions options): DbContext(options)
    {
        // criação das tabelas
        public DbSet<User> Users { get; set; }    
        public DbSet<Workspace> Workspaces { get; set; }    
        public DbSet<ListCard> ListCards { get; set; }    
        public DbSet<Card> Cards { get; set; }
    }
}
