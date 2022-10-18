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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
