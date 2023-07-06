using Microsoft.EntityFrameworkCore;
using TeamsLeague.DAL.Entities;
using TeamsLeague.DAL.Entities.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.DAL.Context
{
    public partial class GameDBContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Member> Members => Set<Member>();
    }
}
