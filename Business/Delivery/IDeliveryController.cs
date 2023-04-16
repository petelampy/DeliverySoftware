namespace DeliverySoftware.Business.Delivery
{
    public interface IDeliveryController
    {
        void Create (Delivery newDelivery);
        Delivery Get (Guid uid);
        List<Delivery> GetAll ();
        List<Delivery> GetAllAvailable ();
        int GetCountByVan (Guid van_uid);
        void Update (Delivery updatedDelivery);
    }
}