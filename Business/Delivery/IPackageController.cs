﻿namespace DeliverySoftware.Business.Delivery
{
    public interface IPackageController
    {
        void Create (Package newPackage);
        void Delete (Guid uid);
        bool DoesTrackingCodeExist (string trackingCode);
        Package Get (Guid uid);
        int GetActivePackagesByCustomer (Guid customer_uid);
        List<Package> GetAll ();
        List<Package> GetAllUndelivered ();
        Package GetByDeliveryAndDropNumber (Guid delivery_uid, int currentDropNumber);
        Package GetByTrackingCode (string trackingCode);
        int GetPackageCountByDelivery (Guid delivery_uid);
        List<Package> GetPackagesByDelivery (Guid delivery_uid);
        void Update (Package updatedPackage);
        void UpdateDeliveryRunDropOrder (Guid delivery_uid);
    }
}