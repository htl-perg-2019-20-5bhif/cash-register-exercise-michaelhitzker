using CashRegisterAPI2.Model;
using Microsoft.EntityFrameworkCore;

namespace CashRegisterAPI2
{
    public class CashRegisterDbContext : DbContext
    {
        public CashRegisterDbContext(DbContextOptions<CashRegisterDbContext> options)
        : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ReceiptLine> ReceiptLines { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
    }
}
