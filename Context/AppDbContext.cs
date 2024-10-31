using ApiUsuario.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUsuario.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } 
    }
}
