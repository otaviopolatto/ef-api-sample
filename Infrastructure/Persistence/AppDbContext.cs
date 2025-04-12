using FinanceControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Financecontrol.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento muitos-para-muitos
            modelBuilder.Entity<Lancamento>()
                .HasMany(l => l.Tags)
                .WithMany(t => t.Lancamentos)
                .UsingEntity<Dictionary<string, object>>(
                    "LancamentoTag",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    j => j.HasOne<Lancamento>().WithMany().HasForeignKey("LancamentoId"),
                    j => j.ToTable("LancamentoTags"));

            // Configuração do Enum
            modelBuilder.Entity<Lancamento>()
                .Property(l => l.Tipo)
                .HasConversion<string>();


            // Configurar valor padrão para DataOperacao
            modelBuilder.Entity<Lancamento>().Property(l => l.Data)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Configuração da Tag
            modelBuilder.Entity<Tag>(entity =>
            {
                // Tamanho máximo da descrição
                entity.Property(t => t.Descricao)
                      .HasMaxLength(100);

                // Índice único na descrição
                entity.HasIndex(t => t.Descricao)
                      .IsUnique();
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Habilitar logging detalhado para diagnóstico (remova em produção)
            optionsBuilder
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

    }
}
