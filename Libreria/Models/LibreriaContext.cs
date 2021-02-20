namespace Libreria.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LibreriaContext : DbContext
    {
        public LibreriaContext()
            : base("name=LibreriaContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<member> members { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Preview> Previews { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member>()
                .HasMany(e => e.Favorites)
                .WithRequired(e => e.member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.member)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.InvoiceInfo)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Favorites)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Previews)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);
        }
    }
}
