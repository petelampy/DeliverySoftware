﻿namespace DeliverySoftware.Business.Delivery
{
    public interface IDeliveryController
    {
        void Create (Delivery newDelivery);
        void Delete (Guid uid);
        Delivery Get (Guid uid);
        List<Delivery> GetAll ();
        List<Delivery> GetAllAvailable ();
        List<Delivery> GetByVan (Guid van_uid);
        int GetCountByVan (Guid van_uid);
        bool HasDeliveryRunStarted (Guid uid);
        void Update (Delivery updatedDelivery);
    }
}