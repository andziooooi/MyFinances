using Microsoft.EntityFrameworkCore;
using MyFinances.Models;

namespace MyFinances.Database
{
    public class TransactionContext : DbContext
    {
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Users> Users { get; set; }

        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }
        public TransactionContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Finances.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        //user -> transactions
        modelBuilder.Entity<Transactions>()
            .HasOne<Users>()
            .WithMany()
            .HasForeignKey(t => t.UsersID)
            .OnDelete(DeleteBehavior.Cascade);

        // Category -> transactions
        modelBuilder.Entity<Transactions>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoriesID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Users>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<Users>()
            .HasIndex(u => u.Login)
            .IsUnique();
        }
    }
}
