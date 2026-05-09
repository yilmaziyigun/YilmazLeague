using Microsoft.EntityFrameworkCore;
using YilmazLeague.EntityLayer.Entities;

namespace YilmazLeague.DataAccessLayer.Context
{
    public class YilmazLeagueDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "server=DESKTOP-9DQS8R7\\MSSQLSERVER01;initial catalog=YilmazLeagueDb;integrated security=true;TrustServerCertificate=true;"
            );
        }

        public DbSet<LeagueMatch> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<Season> Seasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Season>().ToTable("Season");

            modelBuilder.Entity<Season>().HasData(
                new Season
                {
                    SeasonId = 1,
                    Name = "2023-2024",
                    StartYear = 2023,
                    EndYear = 2024,
                    IsActive = false
                },
                new Season
                {
                    SeasonId = 2,
                    Name = "2024-2025",
                    StartYear = 2024,
                    EndYear = 2025,
                    IsActive = false
                },
                new Season
                {
                    SeasonId = 3,
                    Name = "2025-2026",
                    StartYear = 2025,
                    EndYear = 2026,
                    IsActive = false
                },
                new Season
                {
                    SeasonId = 4,
                    Name = "2026-2027",
                    StartYear = 2026,
                    EndYear = 2027,
                    IsActive = true
                }
            );

            modelBuilder.Entity<LeagueMatch>()
                .HasKey(x => x.LeagueMatchId);

            modelBuilder.Entity<Team>()
                .HasKey(x => x.TeamId);

            modelBuilder.Entity<Player>()
                .HasKey(x => x.PlayerId);

            modelBuilder.Entity<MatchEvent>()
                .HasKey(x => x.MatchEventId);

            modelBuilder.Entity<LeagueMatch>()
                .HasOne(x => x.Season)
                .WithMany(x => x.Matches)
                .HasForeignKey(x => x.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeagueMatch>()
                .HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeMatches)
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeagueMatch>()
                .HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayMatches)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MatchEvent>()
                .HasOne(x => x.LeagueMatch)
                .WithMany(x => x.MatchEvents)
                .HasForeignKey(x => x.LeagueMatchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MatchEvent>()
                .HasOne(x => x.Team)
                .WithMany()
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchEvent>()
                .HasOne(x => x.Player)
                .WithMany(x => x.MatchEvents)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchEvent>()
                .HasOne(x => x.PlayerIn)
                .WithMany(x => x.PlayerInEvents)
                .HasForeignKey(x => x.PlayerInId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchEvent>()
                .HasOne(x => x.PlayerOut)
                .WithMany(x => x.PlayerOutEvents)
                .HasForeignKey(x => x.PlayerOutId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}


