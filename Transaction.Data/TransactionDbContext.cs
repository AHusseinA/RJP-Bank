using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.EnableServiceProviderCaching(false);
        //    }
        //}

    }
}
