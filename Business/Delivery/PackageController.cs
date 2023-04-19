using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Database;

namespace DeliverySoftware.Business.Delivery
{
    public class PackageController : IPackageController
    {
        private readonly DeliveryDBContext __DbContext;
        private readonly IDBContextManager __DbContextManager;

        public PackageController () :
            this(new DBContextManager())
        { }

        public PackageController (IDBContextManager dbContextManager)
        {
            __DbContextManager = dbContextManager;
            __DbContext = __DbContextManager.CreateNewDatabaseContext();
        }

        public Package Get (Guid uid)
        {
            return __DbContext.Packages
                 .ToList()
                 .Where(package => package.UID == uid)
                 .SingleOrDefault(new Package());
        }

        public List<Package> GetAll ()
        {
            return __DbContext.Packages.ToList();
        }

        public List<Package> GetAllUndelivered ()
        {
            return __DbContext
                .Packages
                .AsEnumerable()
                .Where(package => !package.IsDelivered)
                .ToList();
        }

        public Package GetByTrackingCode(string trackingCode)
        {
            return __DbContext
                .Packages
                .AsEnumerable()
                .Where(package => package.TrackingCode == trackingCode)
                .SingleOrDefault(new Package());
        }

        public bool DoesTrackingCodeExist(string trackingCode)
        {
             return __DbContext
                .Packages
                .AsEnumerable()
                .Where(package => package.TrackingCode == trackingCode)
                .Count() > 0;
        }

        public Package GetByDeliveryAndDropNumber (Guid delivery_uid, int currentDropNumber)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.DeliveryUID == delivery_uid && package.DropNumber == currentDropNumber)
               .SingleOrDefault(new Package());
        }

        public int GetPackageCountByDelivery(Guid delivery_uid)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.DeliveryUID == delivery_uid)
               .Count();
        }

        public List<Package> GetPackagesByDelivery (Guid delivery_uid)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.DeliveryUID == delivery_uid)
               .ToList();
        }

        public int GetActivePackagesByCustomer (Guid customer_uid)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.CustomerUID == customer_uid && package.IsDelivered == false)
               .Count();
        }

        public void Create (Package newPackage)
        {
            newPackage.UID = Guid.NewGuid();
            
            if(newPackage.DeliveryUID != Guid.Empty)
            {
                newPackage.IsAssignedToDelivery = true;
                newPackage.DropNumber = GetPackageCountByDelivery(newPackage.DeliveryUID) + 1;
            }

            __DbContext.Packages.Add(newPackage);
            __DbContext.SaveChanges();
        }

        public void Update (Package updatedPackage)
        {
            Package _CurrentPackage = Get(updatedPackage.UID);

            _CurrentPackage.Description = updatedPackage.Description;
            _CurrentPackage.Size = updatedPackage.Size;
            _CurrentPackage.CustomerUID = updatedPackage.CustomerUID;
            _CurrentPackage.IsDelivered = updatedPackage.IsDelivered;
            _CurrentPackage.TrackingCode = updatedPackage.TrackingCode;

            if(_CurrentPackage.DeliveryUID == Guid.Empty && updatedPackage.DeliveryUID != Guid.Empty)
            {
                _CurrentPackage.IsAssignedToDelivery = true;
                _CurrentPackage.DropNumber = GetPackageCountByDelivery(_CurrentPackage.DeliveryUID) + 1;
                _CurrentPackage.DeliveryUID = updatedPackage.DeliveryUID;
            }

            __DbContext.SaveChanges();
        }

        public void Delete (Guid uid)
        {
            Package _Package = Get(uid);

            __DbContext.Remove(_Package);
            __DbContext.SaveChanges();
        }

    }
}
