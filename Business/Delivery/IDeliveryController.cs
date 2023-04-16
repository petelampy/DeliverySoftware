namespace DeliverySoftware.Business.Delivery
{
    public interface IDeliveryController
    {
        List<Delivery> GetAll ();
        List<Delivery> GetAllAvailable ();
    }
}