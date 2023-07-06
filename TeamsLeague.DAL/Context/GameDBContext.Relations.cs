using Microsoft.EntityFrameworkCore;
using TeamsLeague.DAL.Entities;
using TeamsLeague.DAL.Entities.MemberParts;
using TeamsLeague.DAL.Entities.TeamParts;

namespace TeamsLeague.DAL.Context
{
    public partial class GameDBContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Team)
                .WithOne(t => t.User)
                .HasForeignKey<User>(u => u.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Team>()
                .HasOne(t => t.User)
                .WithOne(u => u.Team)
                .HasForeignKey<User>(u => u.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(m => m.Team)
                .HasForeignKey(m => m.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Team)
                .WithMany(m => m.Members)
                .HasForeignKey(m => m.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
