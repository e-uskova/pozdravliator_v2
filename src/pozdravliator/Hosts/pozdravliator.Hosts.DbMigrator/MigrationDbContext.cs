using Microsoft.EntityFrameworkCore;
using pozdravliator.Infrastructure.DataAccess;

namespace pozdravliator.Hosts.DbMigrator
{
    public class MigrationDbContext : DataContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options) { }
    }
}
