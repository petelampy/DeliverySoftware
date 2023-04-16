namespace DeliverySoftware.Business.Users
{
    public interface IUserController
    {
        DeliveryUser Get (Guid uid);
        List<DeliveryUser> GetAll ();
        List<DeliveryUser> GetAllCustomers ();
        List<DeliveryUser> GetAllDrivers ();
        List<DeliveryUser> GetAllEmployees ();
    }
}