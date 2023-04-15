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

        internal PackageController (IDBContextManager dbContextManager)
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
    }
}
