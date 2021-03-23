using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Libreria.Models.EntityModel
{
    public partial class LibreriaDataModel : DbContext
    {
        public LibreriaDataModel()
            : base("name=LibreriaContext")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Exhibition> Exhibitions { get; set; }
        public virtual DbSet<ExhibitionCustomer> ExhibitionCustomers { get; set; }
        public virtual DbSet<ExhibitionOrder> ExhibitionOrders { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<member> members { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Preview> Previews { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Exhibition>()
                .Property(e => e.ExhibitionPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ExhibitionCustomer>()
                .HasMany(e => e.ExhibitionOrders)
                .WithRequired(e => e.ExhibitionCustomer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExhibitionOrder>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<member>()
                .HasMany(e => e.Favorites)
                .WithRequired(e => e.member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<member>()
                .HasMany(e => e.ShoppingCarts)
                .WithRequired(e => e.member)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ShoppingCarts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);
        }
    }
}
