using Microsoft.EntityFrameworkCore;

namespace TeamsLeague.DAL.Context
{
    public partial class GameDBContext : DbContext
    {
        public GameDBContext()
        {
            Database.EnsureCreated();
        }

        public GameDBContext(string connectionString) : base(GetOptions(connectionString))
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
        }
    }
}
