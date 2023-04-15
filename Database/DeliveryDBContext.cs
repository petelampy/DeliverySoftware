using DeliverySoftware.Business.Fleet;
using Microsoft.EntityFrameworkCore;

namespace DeliverySoftware.Database
{
    public class DeliveryDBContext : DbContext //: IdentityDbContext<FridgeUser>
    {
        public DeliveryDBContext (DbContextOptions<DeliveryDBContext> options) : base(options)
        { }

        public DbSet<Van> Vans { get; set; }
    }
}
