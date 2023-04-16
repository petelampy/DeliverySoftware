using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Database;

namespace DeliverySoftware.Business.Fleet
{
    public class VanController : IVanController
    {
        private readonly DeliveryDBContext __DbContext;
        private readonly IDBContextManager __DbContextManager;

        public VanController () :
            this(new DBContextManager())
        { }

        internal VanController (IDBContextManager dbContextManager)
        {
            __DbContextManager = dbContextManager;
            __DbContext = __DbContextManager.CreateNewDatabaseContext();
        }

        public Van Get (Guid uid)
        {
            return __DbContext.Vans
                 .ToList()
                 .Where(van => van.UID == uid)
                 .SingleOrDefault(new Van());
        }

        public List<Van> GetAll ()
        {
            return __DbContext.Vans.ToList();
        }

        public int GetCountByDriver(Guid driver_uid)
        {
            return __DbContext.Vans
                .Where(van => van.DriverUID == driver_uid)
                .Count();
        }

        public string GetRegistration (Guid uid)
        {
            return __DbContext.Vans
                .AsEnumerable()
                .Where(van => van.UID == uid)
                .SingleOrDefault(new Van())
                .Registration;
        }
    }
}
