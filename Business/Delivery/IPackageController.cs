namespace DeliverySoftware.Business.Delivery
{
    public interface IPackageController
    {
        bool DoesTrackingCodeExist (string trackingCode);
        List<Package> GetAll ();
        List<Package> GetAllUndelivered ();
        Package GetByTrackingCode (string trackingCode);
    }
}