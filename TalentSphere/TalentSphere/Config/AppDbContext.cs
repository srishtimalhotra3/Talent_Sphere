using Microsoft.EntityFrameworkCore;

namespace TalentSphere.Config
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
