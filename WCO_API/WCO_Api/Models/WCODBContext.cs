using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WCO_Api.Models
{
    public partial class WCODBContext : DbContext
    {
        public WCODBContext()
        {
        }

        public WCODBContext(DbContextOptions<WCODBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bracket> Brackets { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<MatchTeam> MatchTeams { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=WCODB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bracket>(entity =>
            {
                entity.HasKey(e => e.BId);

                entity.ToTable("Bracket");

                entity.Property(e => e.BId)
                    .ValueGeneratedNever()
                    .HasColumnName("b_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.TournamentId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("tournamentId");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Brackets)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bracket_Tournament");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(e => e.MId);

                entity.ToTable("Match");

                entity.Property(e => e.MId)
                    .ValueGeneratedNever()
                    .HasColumnName("m_id");

                entity.Property(e => e.BracketId).HasColumnName("bracket_id");

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("date");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("startTime");

                entity.Property(e => e.Venue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("venue");

                entity.HasOne(d => d.Bracket)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.BracketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Match_Bracket");
            });

            modelBuilder.Entity<MatchTeam>(entity =>
            {
                entity.ToTable("Match_Team");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.MatchId).HasColumnName("match_id");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchTeams)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Match_Team_Match");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.MatchTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Match_Team_Team");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("Player");

                entity.Property(e => e.PId)
                    .ValueGeneratedNever()
                    .HasColumnName("p_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_Player_Team");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.TeId);

                entity.ToTable("Team");

                entity.Property(e => e.TeId)
                    .ValueGeneratedNever()
                    .HasColumnName("te_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.TournamentId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("tournamentId");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("FK_Team_Tournament");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.ToId);

                entity.ToTable("Tournament");

                entity.Property(e => e.ToId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("to_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("endDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("startDate");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
