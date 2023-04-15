using Microsoft.EntityFrameworkCore;

namespace DeliverySoftware.Database
{
    public class DBContextManager : IDBContextManager
    {
        public DeliveryDBContext CreateNewDatabaseContext ()
        {
            WebApplicationBuilder _Builder = WebApplication.CreateBuilder();

            string? _ConnectionString = _Builder.Configuration.GetConnectionString("Default");

            DbContextOptionsBuilder<DeliveryDBContext> _OptionsBuilder = new DbContextOptionsBuilder<DeliveryDBContext>();

            _OptionsBuilder.UseSqlServer(_ConnectionString);

            return new DeliveryDBContext(_OptionsBuilder.Options);
        }
    }
}
