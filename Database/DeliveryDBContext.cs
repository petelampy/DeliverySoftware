using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeliverySoftware.Database
{
    public class DeliveryDBContext : IdentityDbContext<DeliveryUser>
    {
        public DeliveryDBContext (DbContextOptions<DeliveryDBContext> options) : base(options)
        { }

        public DbSet<Van> Vans { get; set; }
    }
}


