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
                .OnDelete(DeleteBehavior.Cascade);

            #region TEAM RELATIONS
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
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Traits)
                .WithOne(tt => tt.Team)
                .HasForeignKey(tt => tt.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TeamTrait>()
                .HasOne(tt => tt.Team)
                .WithMany(t => t.Traits)
                .HasForeignKey(tt => tt.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region MEMBER RELATIONS
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Team)
                .WithMany(m => m.Members)
                .HasForeignKey(m => m.TeamId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Member>()
                .HasMany(m => m.Positions)
                .WithOne(p => p.Member)
                .HasForeignKey(p => p.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Member>()
                .HasMany(m => m.Traits)
                .WithOne(t => t.Member)
                .HasForeignKey(t => t.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Position>()
                .HasOne(p => p.Member)
                .WithMany(m => m.Positions)
                .HasForeignKey(p => p.MemberId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MemberTrait>()
                .HasOne(t => t.Member)
                .WithMany(m => m.Traits)
                .HasForeignKey(t => t.MemberId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
