using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestCertiCamara.Models.Entities;

#nullable disable

namespace TestCertiCamara.Models
{
  public partial class LogQueriesContext : DbContext
  {
    public LogQueriesContext()
    {
    }

    public LogQueriesContext(DbContextOptions<LogQueriesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CardTarget> CardTargets { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<HistoryQueryLog> HistoryQueryLogs { get; set; }
    public virtual DbSet<MovementTransaction> MovementTransactions { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        var connectionString = configuration.GetConnectionString("logQuery");
        optionsBuilder.UseSqlServer(connectionString);
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

      modelBuilder.Entity<CardTarget>(entity =>
      {
        entity.ToTable("CardTarget", "logQry");

        entity.HasIndex(e => e.IdProduct, "UQ_CardTarget_IdProduct")
                  .IsUnique();

        entity.HasIndex(e => new { e.Id, e.NameCard }, "UQ_CardTarget_NameCard")
                  .IsUnique();

        entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

        entity.Property(e => e.CardType)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.DateExpire).HasColumnType("datetime");

        entity.Property(e => e.NameCard)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.HasOne(d => d.IdProductNavigation)
                  .WithOne(p => p.CardTarget)
                  .HasForeignKey<CardTarget>(d => d.IdProduct)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_CardTarget_IdProduct");
      });

      modelBuilder.Entity<Client>(entity =>
      {
        entity.ToTable("Client", "logQry");

        entity.HasIndex(e => new { e.Id, e.ClientName }, "UQ_Client_Name")
                  .IsUnique();

        entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

        entity.Property(e => e.ClientEmail)
                  .HasMaxLength(80)
                  .IsUnicode(false);

        entity.Property(e => e.ClientName)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.HasOne(d => d.IdProductNavigation)
                  .WithMany(p => p.Clients)
                  .HasForeignKey(d => d.IdProduct)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Client_IdProduct");
      });

      modelBuilder.Entity<HistoryQueryLog>(entity =>
      {
        entity.ToTable("HistoryQueryLog", "logQry");

        entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

        entity.Property(e => e.RegistryDate)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("(getdate())");

        entity.Property(e => e.ResponseQuery)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false);

        entity.Property(e => e.UrlQuery)
                  .IsRequired()
                  .HasMaxLength(500)
                  .IsUnicode(false);
      });

      modelBuilder.Entity<MovementTransaction>(entity =>
      {
        entity.ToTable("MovementTransaction", "logQry");

        entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

        entity.Property(e => e.DatePayed)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("(getdate())");

        entity.Property(e => e.QuantityPayed).HasColumnType("decimal(12, 2)");

        entity.Property(e => e.StatusPayed).HasDefaultValueSql("((-1))");

        entity.HasOne(d => d.IdClientNavigation)
                  .WithMany(p => p.MovementTransactions)
                  .HasForeignKey(d => d.IdClient)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("PK_MovementTransaction_IdClient");
      });

      modelBuilder.Entity<Product>(entity =>
      {
        entity.ToTable("Product", "logQry");

        entity.HasIndex(e => e.NameProduct, "UQ_Product_NameProduct")
                  .IsUnique();

        entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

        entity.Property(e => e.Balance).HasColumnType("decimal(12, 2)");

        entity.Property(e => e.MountCredit)
                  .HasColumnType("decimal(12, 2)")
                  .HasDefaultValueSql("((1000000))");

        entity.Property(e => e.NameProduct)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.NumberQuotas).HasDefaultValueSql("((12))");

        entity.Property(e => e.PriceQuota).HasColumnType("decimal(12, 2)");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
