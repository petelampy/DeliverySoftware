namespace DeliverySoftware.Business.Delivery
{
    public interface IPackageController
    {
        bool DoesTrackingCodeExist (string trackingCode);
        List<Package> GetAll ();
        List<Package> GetAllUndelivered ();
        Package GetByDeliveryAndDropNumber (Guid delivery_uid, int currentDropNumber);
        Package GetByTrackingCode (string trackingCode);
        int GetPackageCountByDelivery (Guid delivery_uid);
    }
}