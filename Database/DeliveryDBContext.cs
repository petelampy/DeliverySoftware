using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeliverySoftware.Database
{
    public class DeliveryDBContext : IdentityDbContext<DeliveryUser>
    {
        public DeliveryDBContext () { }
        public DeliveryDBContext (DbContextOptions<DeliveryDBContext> options) : base(options)
        { }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DeliveryUser>().HasData(SampleDataManager.GenerateUsers());
            builder.Entity<Van>().HasData(SampleDataManager.GenerateVan());
            builder.Entity<Delivery>().HasData(SampleDataManager.GenerateDelivery());
            builder.Entity<Package>().HasData(SampleDataManager.GeneratePackage());
        }

        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Van> Vans { get; set; }
    }
}


