using DiscordElegiaBot.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordElegiaBot.DAL.Database.Context
{
    public class MirageContext : DbContext
    {
        public MirageContext(DbContextOptions<MirageContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<UserHit> UsersHits { get; private set; }
        public DbSet<UserUnusualKill> UsersUnusualKills { get; private set; }
        public DbSet<UserWeapon> UsersWeapons { get; private set; }
        public DbSet<TestMvtUser> MvtUsers { get; private set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Steam)
                    .HasName("PRIMARY");

                entity.ToTable("lvl_base");

                entity.Property(e => e.Steam)
                    .HasMaxLength(22)
                    .HasColumnName("steam");

                entity.Property(e => e.Assists)
                    .HasColumnType("int(11)")
                    .HasColumnName("assists");

                entity.Property(e => e.Deaths)
                    .HasColumnType("int(11)")
                    .HasColumnName("deaths");

                entity.Property(e => e.Headshots)
                    .HasColumnType("int(11)")
                    .HasColumnName("headshots");

                entity.Property(e => e.Hits)
                    .HasColumnType("int(11)")
                    .HasColumnName("hits");

                entity.Property(e => e.Kills)
                    .HasColumnType("int(11)")
                    .HasColumnName("kills");

                entity.Property(e => e.LastConnect)
                    .HasColumnType("int(11)")
                    .HasColumnName("lastconnect");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Playtime)
                    .HasColumnType("int(11)")
                    .HasColumnName("playtime");

                entity.Property(e => e.Rank)
                    .HasColumnType("int(11)")
                    .HasColumnName("rank");

                entity.Property(e => e.RoundLose)
                    .HasColumnType("int(11)")
                    .HasColumnName("round_lose");

                entity.Property(e => e.RoundWin)
                    .HasColumnType("int(11)")
                    .HasColumnName("round_win");

                entity.Property(e => e.Shoots)
                    .HasColumnType("int(11)")
                    .HasColumnName("shoots");

                entity.Property(e => e.Value)
                    .HasColumnType("int(11)")
                    .HasColumnName("value");
            });

            modelBuilder.Entity<UserHit>(entity =>
            {
                entity.HasKey(e => e.SteamId)
                    .HasName("PRIMARY");

                entity.ToTable("lvl_base_hits");

                entity.Property(e => e.SteamId)
                    .HasMaxLength(32)
                    .HasColumnName("SteamID")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Belly).HasColumnType("int(11)");

                entity.Property(e => e.Chest).HasColumnType("int(11)");

                entity.Property(e => e.DmgArmor).HasColumnType("int(11)");

                entity.Property(e => e.DmgHealth).HasColumnType("int(11)");

                entity.Property(e => e.Head).HasColumnType("int(11)");

                entity.Property(e => e.LeftArm).HasColumnType("int(11)");

                entity.Property(e => e.LeftLeg).HasColumnType("int(11)");

                entity.Property(e => e.Neak).HasColumnType("int(11)");

                entity.Property(e => e.RightArm).HasColumnType("int(11)");

                entity.Property(e => e.RightLeg).HasColumnType("int(11)");
            });

            modelBuilder.Entity<UserUnusualKill>(entity =>
            {
                entity.HasKey(e => e.SteamId)
                    .HasName("PRIMARY");

                entity.ToTable("lvl_base_unusualkills");

                entity.Property(e => e.SteamId)
                    .HasMaxLength(22)
                    .HasColumnName("SteamID");

                entity.Property(e => e.Flash).HasColumnType("int(11)");

                entity.Property(e => e.Jump).HasColumnType("int(11)");

                entity.Property(e => e.LastClip).HasColumnType("int(11)");

                entity.Property(e => e.NoScope).HasColumnType("int(11)");

                entity.Property(e => e.Op)
                    .HasColumnType("int(11)")
                    .HasColumnName("OP");

                entity.Property(e => e.Penetrated).HasColumnType("int(11)");

                entity.Property(e => e.Run).HasColumnType("int(11)");

                entity.Property(e => e.Smoke).HasColumnType("int(11)");

                entity.Property(e => e.Whirl).HasColumnType("int(11)");
            });

            modelBuilder.Entity<UserWeapon>(entity =>
            {
                entity.HasKey(e => new { e.Steam, e.Classname })
                    .HasName("PRIMARY");

                entity.ToTable("lvl_base_weapons");

                entity.Property(e => e.Steam)
                    .HasMaxLength(32)
                    .HasColumnName("steam")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Classname)
                    .HasMaxLength(64)
                    .HasColumnName("classname")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Kills)
                    .HasColumnType("int(11)")
                    .HasColumnName("kills");
            });

            modelBuilder.Entity<TestMvtUser>(entity =>
            {
                entity.ToTable("mvt_users");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.NextTest)
                    .HasColumnType("int(11)")
                    .HasColumnName("next_test");

                entity.Property(e => e.Steam)
                    .HasColumnType("int(11)")
                    .HasColumnName("steam");

                entity.Property(e => e.TestGroup)
                    .HasMaxLength(64)
                    .HasColumnName("test_group");
            });
        }
    }
}