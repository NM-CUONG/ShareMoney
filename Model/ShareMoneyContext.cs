using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model
{
    public class ShareMoneyContext : DbContext
    {
        public ShareMoneyContext(DbContextOptions<ShareMoneyContext> options) : base(options) 
        {
            
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole  { get; set; }


    }
}
