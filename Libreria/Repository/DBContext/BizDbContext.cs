using Libreria.Models.EntityModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Libreria.Repository
{
    public partial class BizDbContext : DbContext
    {
        public BizDbContext()
            : base("name=BizDbContext")
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SalesMan> SalesMan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
