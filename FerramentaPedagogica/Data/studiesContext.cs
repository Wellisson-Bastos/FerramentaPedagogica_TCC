using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FerramentaPedagogica.Data
{
    public partial class studiesContext : DbContext
    {
        public studiesContext()
        {
        }

        public studiesContext(DbContextOptions<studiesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Favorito> Favorito { get; set; }
        public virtual DbSet<JogadorModel> Jogador { get; set; }
        public virtual DbSet<Jogo> Jogo { get; set; }
        public virtual DbSet<Pergunta> Pergunta { get; set; }
        public virtual DbSet<Resposta> Resposta { get; set; }
        public virtual DbSet<Sessao> Sessao { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:wellissonstudies.database.windows.net,1433;Initial Catalog=studies;Persist Security Info=False;User ID=wellisson;Password=240717Wa;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorito>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.DataInsercao).HasColumnType("datetime");

                entity.HasOne(d => d.CodigoJogoNavigation)
                    .WithMany(p => p.Favorito)
                    .HasForeignKey(d => d.CodigoJogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Jogo_Favorito");

                entity.HasOne(d => d.CodigoUsuarioNavigation)
                    .WithMany(p => p.Favorito)
                    .HasForeignKey(d => d.CodigoUsuario)
                    .HasConstraintName("FK_Usuario_Favorito");
            });

            modelBuilder.Entity<JogadorModel>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.CodigoSessao).HasColumnName("Codigo_Sessao");

                entity.Property(e => e.Jogador)
                    .IsRequired()
                    .HasColumnName("Jogador")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoSessaoNavigation)
                    .WithMany(p => p.Jogador)
                    .HasForeignKey(d => d.CodigoSessao)
                    .HasConstraintName("FK_Sessao_Jogador");
            });

            modelBuilder.Entity<Jogo>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Capa).HasColumnType("image");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.HoraCriacao).HasColumnType("datetime");

                entity.Property(e => e.PkUsuario).HasColumnName("PK_Usuario");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PkUsuarioNavigation)
                    .WithMany(p => p.Jogo)
                    .HasForeignKey(d => d.PkUsuario)
                    .HasConstraintName("FK_Usuario_Jogo");
            });

            modelBuilder.Entity<Pergunta>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.CodigoJogo).HasColumnName("Codigo_Jogo");

                entity.Property(e => e.Imagem).HasColumnType("image");

                entity.Property(e => e.LinkVideo)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Ordem).HasDefaultValueSql("((0))");

                entity.Property(e => e.Pontuacao).HasDefaultValueSql("((10))");

                entity.Property(e => e.RespostaCorreta)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Texto).IsUnicode(false);

                entity.HasOne(d => d.CodigoJogoNavigation)
                    .WithMany(p => p.Pergunta)
                    .HasForeignKey(d => d.CodigoJogo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Jogo_Pergunta");
            });

            modelBuilder.Entity<Resposta>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.CodigoPergunta).HasColumnName("Codigo_Pergunta");

                entity.Property(e => e.Opcao)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoPerguntaNavigation)
                    .WithMany(p => p.Resposta)
                    .HasForeignKey(d => d.CodigoPergunta)
                    .HasConstraintName("FK_Pergunta_Resposta");
            });

            modelBuilder.Entity<Sessao>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.CodigoJogo).HasColumnName("Codigo_Jogo");

                entity.HasOne(d => d.CodigoJogoNavigation)
                    .WithMany(p => p.Sessao)
                    .HasForeignKey(d => d.CodigoJogo)
                    .HasConstraintName("FK_Jogo_Sessao");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Foto).HasColumnType("image");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Usuario1)
                    .IsRequired()
                    .HasColumnName("Usuario")
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
