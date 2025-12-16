using Microsoft.EntityFrameworkCore;
using permit_portal.Models.DomainModel;

namespace permit_portal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TouristPermit> TouristPermits { get; set; }
    }
}
