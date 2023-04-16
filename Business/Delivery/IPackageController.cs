namespace DeliverySoftware.Business.Delivery
{
    public interface IPackageController
    {
        List<Package> GetAll ();
        List<Package> GetAllUndelivered ();
    }
}