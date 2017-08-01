namespace SistemaIgreja.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Contexto : DbContext
    {
        public Contexto()
            : base("name=Contexto")
        {
        }

        public virtual DbSet<CELULA> CELULA { get; set; }
        public virtual DbSet<DIA_SEMANA> DIA_SEMANA { get; set; }
        public virtual DbSet<DISCIPULO> DISCIPULO { get; set; }
        public virtual DbSet<ENTRADA> ENTRADA { get; set; }
        public virtual DbSet<ESCOLA_LIDERES> ESCOLA_LIDERES { get; set; }
        public virtual DbSet<FEZ_BATISMO> FEZ_BATISMO { get; set; }
        public virtual DbSet<FEZ_ENCONTRO> FEZ_ENCONTRO { get; set; }
        public virtual DbSet<HORARIO_CELULA> HORARIO_CELULA { get; set; }
        public virtual DbSet<SAIDA> SAIDA { get; set; }
        public virtual DbSet<SEXO> SEXO { get; set; }
        public virtual DbSet<TIPO_ENTRADA> TIPO_ENTRADA { get; set; }
        public virtual DbSet<TIPO_SAIDA> TIPO_SAIDA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CELULA>()
                .Property(e => e.LUGAR)
                .IsUnicode(false);

            modelBuilder.Entity<DIA_SEMANA>()
                .Property(e => e.NOME_DIA)
                .IsUnicode(false);

            modelBuilder.Entity<DIA_SEMANA>()
                .HasMany(e => e.CELULA)
                .WithRequired(e => e.DIA_SEMANA)
                .HasForeignKey(e => e.DIA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPULO>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<DISCIPULO>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<DISCIPULO>()
                .Property(e => e.ENDERECO)
                .IsUnicode(false);

            modelBuilder.Entity<DISCIPULO>()
                .Property(e => e.TELEFONE)
                .IsUnicode(false);

            modelBuilder.Entity<DISCIPULO>()
                .HasMany(e => e.CELULA)
                .WithRequired(e => e.DISCIPULO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DISCIPULO>()
                .HasMany(e => e.DISCIPULO1)
                .WithOptional(e => e.DISCIPULO2)
                .HasForeignKey(e => e.DISCIPULADOR);

            modelBuilder.Entity<ENTRADA>()
                .Property(e => e.valor)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ENTRADA>()
                .Property(e => e.observao)
                .IsUnicode(false);

            modelBuilder.Entity<ESCOLA_LIDERES>()
                .Property(e => e.OPCAO)
                .IsUnicode(false);

            modelBuilder.Entity<ESCOLA_LIDERES>()
                .HasMany(e => e.DISCIPULO)
                .WithRequired(e => e.ESCOLA_LIDERES1)
                .HasForeignKey(e => e.ESCOLA_LIDERES)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FEZ_BATISMO>()
                .Property(e => e.OPCAO)
                .IsUnicode(false);

            modelBuilder.Entity<FEZ_BATISMO>()
                .HasMany(e => e.DISCIPULO)
                .WithRequired(e => e.FEZ_BATISMO)
                .HasForeignKey(e => e.BATISMO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FEZ_ENCONTRO>()
                .Property(e => e.OPCAO)
                .IsUnicode(false);

            modelBuilder.Entity<FEZ_ENCONTRO>()
                .HasMany(e => e.DISCIPULO)
                .WithRequired(e => e.FEZ_ENCONTRO)
                .HasForeignKey(e => e.ENCONTRO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HORARIO_CELULA>()
                .Property(e => e.HORARIO)
                .IsUnicode(false);

            modelBuilder.Entity<HORARIO_CELULA>()
                .HasMany(e => e.CELULA)
                .WithRequired(e => e.HORARIO_CELULA)
                .HasForeignKey(e => e.HORARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SAIDA>()
                .Property(e => e.valor)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SAIDA>()
                .Property(e => e.observacao)
                .IsUnicode(false);

            modelBuilder.Entity<SEXO>()
                .Property(e => e.NOME)
                .IsUnicode(false);

            modelBuilder.Entity<SEXO>()
                .HasMany(e => e.DISCIPULO)
                .WithRequired(e => e.SEXO1)
                .HasForeignKey(e => e.SEXO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TIPO_ENTRADA>()
                .Property(e => e.TIPO)
                .IsUnicode(false);

            modelBuilder.Entity<TIPO_ENTRADA>()
                .HasMany(e => e.ENTRADA)
                .WithRequired(e => e.TIPO_ENTRADA)
                .HasForeignKey(e => e.cod_tipo_entrada)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TIPO_SAIDA>()
                .Property(e => e.TIPO)
                .IsUnicode(false);

            modelBuilder.Entity<TIPO_SAIDA>()
                .HasMany(e => e.SAIDA)
                .WithRequired(e => e.TIPO_SAIDA)
                .WillCascadeOnDelete(false);
        }
    }
}
