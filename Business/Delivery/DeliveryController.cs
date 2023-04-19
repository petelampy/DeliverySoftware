using DeliverySoftware.Business.Fleet;
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

        public DeliveryController (IDBContextManager dbContextManager)
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

        public List<Delivery> GetByVan (Guid van_uid)
        {
            return __DbContext.Deliveries
                 .AsEnumerable()
                 .Where(delivery => delivery.VanUID == van_uid)
                 .ToList();
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

        public int GetCountByVan(Guid van_uid)
        {
            return __DbContext
                .Deliveries
                .AsEnumerable()
                .Where(delivery => delivery.VanUID == van_uid && delivery.Status != DeliveryStatus.Completed)
                .Count();
        }

        public bool HasDeliveryRunStarted (Guid uid)
        {
            return __DbContext
                .Deliveries
                .AsEnumerable()
                .Where(delivery => delivery.UID == uid && delivery.Status == DeliveryStatus.Started)
                .Count() > 0;
        }

        public void Create (Delivery newDelivery)
        {
            newDelivery.UID = Guid.NewGuid();
            newDelivery.CurrentDrop = 0;
            newDelivery.Status = DeliveryStatus.Pending;
            newDelivery.NumberOfPackages = 0;

            __DbContext.Deliveries.Add(newDelivery);
            __DbContext.SaveChanges();
        }

        public void Update (Delivery updatedDelivery)
        {
            Delivery _CurrentDelivery = Get(updatedDelivery.UID);

            _CurrentDelivery.NumberOfPackages = updatedDelivery.NumberOfPackages;
            _CurrentDelivery.Status = updatedDelivery.Status;
            _CurrentDelivery.CurrentDrop = updatedDelivery.CurrentDrop;
            _CurrentDelivery.VanUID = updatedDelivery.VanUID;

            __DbContext.SaveChanges();
        }

        public void Delete (Guid uid)
        {
            Delivery _Delivery = Get(uid);

            __DbContext.Remove(_Delivery);
            __DbContext.SaveChanges();
        }
    }
}
