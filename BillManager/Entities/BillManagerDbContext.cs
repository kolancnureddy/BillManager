using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Entities
{
    public class BillManagerDbContext: DbContext
    {
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<SplitBill> SplitBills { get; set; }

        public BillManagerDbContext(DbContextOptions<BillManagerDbContext> options)
            :base(options)
        {
            Database.Migrate();
        }
    }
}
