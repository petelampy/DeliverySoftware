namespace DeliverySoftware.Business.Delivery
{
    public interface IDeliveryController
    {
        List<Delivery> GetAll ();
        List<Delivery> GetAllAvailable ();
        int GetCountByVan (Guid van_uid);
    }
}