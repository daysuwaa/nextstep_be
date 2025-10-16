using Microsoft.EntityFrameworkCore;
using nextstep.application.DTOs.Responses;
using nextstep.application.Interfaces;
using nextstep.domain.Entities;

namespace nextstep.infrastructure
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }

        // Correct implementation
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}