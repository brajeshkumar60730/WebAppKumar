using Microsoft.EntityFrameworkCore;
using WebAppKumar.Model;

namespace WebAppKumar.Data
{
    public class webAppDbContext : DbContext
    {
        public webAppDbContext(DbContextOptions options) : base(options) 
        { 
        }
        public DbSet<User>Users { get; set; }
    }
}
