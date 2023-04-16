namespace DeliverySoftware.Business.Delivery
{
    public interface IDeliveryController
    {
        Delivery Get (Guid uid);
        List<Delivery> GetAll ();
        List<Delivery> GetAllAvailable ();
        int GetCountByVan (Guid van_uid);
    }
}