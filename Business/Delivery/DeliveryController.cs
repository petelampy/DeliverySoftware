using DeliverySoftware.Database;

namespace DeliverySoftware.Business.Delivery
{
    public class DeliveryController : IDeliveryController
    {
        private readonly DeliveryDBContext __DbContext;
        private readonly IDBContextManager __DbContextManager;

        public DeliveryController () :
            this(new DBContextManager())
        { }

        internal DeliveryController (IDBContextManager dbContextManager)
        {
            __DbContextManager = dbContextManager;
            __DbContext = __DbContextManager.CreateNewDatabaseContext();
        }

        public Delivery Get (Guid uid)
        {
            return __DbContext.Deliveries
                 .ToList()
                 .Where(delivery => delivery.UID == uid)
                 .SingleOrDefault(new Delivery());
        }

        public List<Delivery> GetAll ()
        {
            return __DbContext.Deliveries.ToList();
        }

        public List<Delivery> GetAllAvailable ()
        {
            return __DbContext
                .Deliveries
                .AsEnumerable()
                .Where(delivery => delivery.Status != DeliveryStatus.Completed)
                .ToList();
        }
    }
}
