using Microsoft.EntityFrameworkCore;

namespace  PruebaLogin.Models
{
    public class MvcUserContext : DbContext
    {
        public MvcUserContext (DbContextOptions<MvcUserContext> options)
            : base(options)
        {
        }

        public DbSet<PruebaLogin.Models.User> Movie { get; set; }
    }
}