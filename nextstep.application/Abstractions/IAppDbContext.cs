using Microsoft.EntityFrameworkCore;
using nextstep.application.DTOs.Responses;
using nextstep.domain.Entities;

namespace nextstep.application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<Entry> Entries { get; set; }
    }
}