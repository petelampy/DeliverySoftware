﻿using DeliverySoftware.Database;

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

    }
}
