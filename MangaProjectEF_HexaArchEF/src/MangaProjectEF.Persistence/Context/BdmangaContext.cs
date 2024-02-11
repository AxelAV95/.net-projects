using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MangaProjectEF.Persistence.Context;

public partial class BdmangaContext : DbContext
{
    public BdmangaContext()
    {
    }

    public BdmangaContext(DbContextOptions<BdmangaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tblogerror> Tblogerrors { get; set; }

    public virtual DbSet<Tbmanga> Tbmangas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-FF6SODJH\\SQLEXPRESS;Initial Catalog=bdmanga;User ID=sa;Password=12345; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tblogerror>(entity =>
        {
            entity.HasKey(e => e.Logerrorid).HasName("pk_error");

            entity.ToTable("tblogerror");

            entity.Property(e => e.Logerrorid).HasColumnName("logerrorid");
            entity.Property(e => e.Logerrordescripcion).HasColumnName("logerrordescripcion");
            entity.Property(e => e.Logerrorlinea).HasColumnName("logerrorlinea");
            entity.Property(e => e.Logerrornumero).HasColumnName("logerrornumero");
            entity.Property(e => e.Logerrorseveridad).HasColumnName("logerrorseveridad");
            entity.Property(e => e.Logerrorstoreprocedure)
                .HasMaxLength(50)
                .HasColumnName("logerrorstoreprocedure");
            entity.Property(e => e.Logfechahora)
                .HasColumnType("datetime")
                .HasColumnName("logfechahora");
        });

        modelBuilder.Entity<Tbmanga>(entity =>
        {
            entity.HasKey(e => e.Mangaid).HasName("pk_manga");

            entity.ToTable("tbmanga");

            entity.Property(e => e.Mangaid).HasColumnName("mangaid");
            entity.Property(e => e.Manganombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("manganombre");
            entity.Property(e => e.Mangaregistro)
                .HasColumnType("datetime")
                .HasColumnName("mangaregistro");
            entity.Property(e => e.Mangatomo).HasColumnName("mangatomo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
