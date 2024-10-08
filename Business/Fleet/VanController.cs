﻿using DeliverySoftware.Database;

namespace DeliverySoftware.Business.Fleet
{
    public class VanController : IVanController
    {
        private readonly DeliveryDBContext __DbContext;
        private readonly IDBContextManager __DbContextManager;

        public VanController () :
            this(new DBContextManager())
        { }

        public VanController (IDBContextManager dbContextManager)
        {
            __DbContextManager = dbContextManager;
            __DbContext = __DbContextManager.CreateNewDatabaseContext();
        }

        public void Create (Van newVan)
        {
            newVan.UID = Guid.NewGuid();

            __DbContext.Vans.Add(newVan);
            __DbContext.SaveChanges();
        }

        public void Delete (Guid uid)
        {
            Van _Van = Get(uid);

            __DbContext.Remove(_Van);
            __DbContext.SaveChanges();
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

        public int GetCountByDriver (Guid driver_uid)
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

        public void Update (Van updatedVan)
        {
            Van _CurrentVan = Get(updatedVan.UID);

            _CurrentVan.Capacity = updatedVan.Capacity;
            _CurrentVan.DriverUID = updatedVan.DriverUID;
            _CurrentVan.Registration = updatedVan.Registration;
            _CurrentVan.DepotPostCode = updatedVan.DepotPostCode;

            __DbContext.SaveChanges();
        }
    }
}
