using System;
using System.Collections.Generic;
using API_CooperaProfissional.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CooperaProfissional.Data;

public partial class dbCooperaEntities : DbContext
{
    public dbCooperaEntities()
    {
    }

    public dbCooperaEntities(DbContextOptions<dbCooperaEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<Funcao> Funcaos { get; set; }

    public virtual DbSet<ImagensPortifolio> ImagensPortifolios { get; set; }

    public virtual DbSet<Servico> Servicos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Avaliaco> Avaliacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcao>(entity =>
        {
            entity.HasKey(e => e.IdFuncao).HasName("PK__Funcao__CE75DBD24FD7869F");

            entity.ToTable("Funcao");

            entity.Property(e => e.NomeFuncao)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ImagensPortifolio>(entity =>
        {
            entity.HasKey(e => e.IdImagem).HasName("PK__ImagensP__B42D8F150611F977");

            entity.ToTable("ImagensPortifolio");
        });

        modelBuilder.Entity<Servico>(entity =>
        {
            entity.HasKey(e => e.IdServico).HasName("PK__Servico__474DDE3AA308EC3E");

            entity.ToTable("Servico");

            entity.Property(e => e.NomeServico)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF977816AA99");

            entity.ToTable("Usuario");

            entity.Property(e => e.Descricao)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NomeCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pin).HasColumnName("PIN");
            entity.Property(e => e.Senha)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFuncaoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdFuncao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Funcao");

            entity.HasOne(d => d.IdServicoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdServico)
                .HasConstraintName("FK_Usuario_Servico");
        });

        modelBuilder.Entity<Avaliaco>(entity =>
        {
            entity.HasKey(e => e.IdAvaliacao);

            entity.ToTable("Avaliacoes");

            entity.Property(e => e.NomeAvaliador)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.HasOne(d => d.IdUsuarioNavigation)
                .WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Avaliacoes_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
