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

            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 1, Name = "Wynagrodzenie", Type = 1 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 2, Name = "Inne przychody", Type = 1 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 3, Name = "Stypendia", Type = 1 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 4, Name = "Płatności", Type = 0 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 5, Name = "Codzienne wydatki", Type = 0 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 6, Name = "Jedzenie", Type = 0 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 7, Name = "Auto i transport", Type = 0 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 8, Name = "Osobiste", Type = 0 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 9, Name = "Podróże i wypoczynek", Type = 0 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 10, Name = "Rozrywka", Type = 0 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 11, Name = "Nauka", Type = 0 });
            modelBuilder.Entity<Categories>().HasData(new Categories { ID = 12, Name = "Inne wydatki", Type = 0 });
        }
    }
}
