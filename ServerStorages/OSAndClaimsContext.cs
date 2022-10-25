using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ServerStorages
{
    public partial class OSAndClaimsContext : DbContext
    {
        public OSAndClaimsContext()
        {
        }

        public OSAndClaimsContext(DbContextOptions<OSAndClaimsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PongPingUniformResourceIdentifier> PongPingUniformResourceIdentifiers { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<TokenSecurity> TokenSecurities { get; set; } = null!;
        public virtual DbSet<TokensSecurity> TokensSecurities { get; set; } = null!;
        public virtual DbSet<UnitConnection> UnitConnections { get; set; } = null!;
        public virtual DbSet<UnitIdentification> UnitIdentifications { get; set; } = null!;
        public virtual DbSet<UnitUser> UnitUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MSBC;Initial Catalog=OSAndClaims;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<PongPingUniformResourceIdentifier>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Use).HasColumnType("datetime");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AutomaticDeletion).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");
            });

            modelBuilder.Entity<TokenSecurity>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__TokenSec__A25C5AA606F8555E");

                entity.Property(e => e.Code).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.TokenId).HasColumnName("TokenID");

                entity.HasOne(d => d.Token)
                    .WithMany(p => p.TokenSecurities)
                    .HasForeignKey(d => d.TokenId)
                    .HasConstraintName("FK_TokenSecurities_ToTokens");
            });

            modelBuilder.Entity<TokensSecurity>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AutomaticDeletion).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");
            });

            modelBuilder.Entity<UnitConnection>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.UnitIdentificationId).HasColumnName("UnitIdentificationID");

                entity.HasOne(d => d.UnitIdentification)
                    .WithMany(p => p.UnitConnections)
                    .HasForeignKey(d => d.UnitIdentificationId)
                    .HasConstraintName("FK_UnitConnections_ToUnitIdentifications");
            });

            modelBuilder.Entity<UnitIdentification>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AutomaticDeletion).HasColumnType("datetime");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Iso3166)
                    .HasMaxLength(2)
                    .HasColumnName("ISO3166");

                entity.Property(e => e.Iso6391)
                    .HasMaxLength(2)
                    .HasColumnName("ISO639_1");

                entity.Property(e => e.TokenId).HasColumnName("TokenID");

                entity.HasOne(d => d.Token)
                    .WithMany(p => p.UnitIdentifications)
                    .HasForeignKey(d => d.TokenId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_UnitIdentifications_ToToken");
            });

            modelBuilder.Entity<UnitUser>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Expires).HasColumnType("datetime");

                entity.Property(e => e.UnitIdentificationId).HasColumnName("UnitIdentificationID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.UnitIdentification)
                    .WithMany(p => p.UnitUsers)
                    .HasForeignKey(d => d.UnitIdentificationId)
                    .HasConstraintName("FK_UnitUsers_ToUnitIdentifications");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UnitUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UnitUsers_ToUser");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Expires).HasColumnType("datetime");

                entity.Property(e => e.Iso3166)
                    .HasMaxLength(2)
                    .HasColumnName("ISO3166");

                entity.Property(e => e.Iso6391)
                    .HasMaxLength(2)
                    .HasColumnName("ISO639_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
