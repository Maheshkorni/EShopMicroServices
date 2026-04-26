using Discount.GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GrpcService.Data
{
    public class DiscountDbContext: DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options):base(options) 
        {

        }
        public DbSet<Coupon> Coupon { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName= "Iphone 17", Description = "Mobile" , Amount = 12},
                new Coupon { Id = 2, ProductName = "SamSung s26", Description = "Mobile", Amount = 14 }
                );
        }
    }
}
