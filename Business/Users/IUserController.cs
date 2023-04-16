namespace DeliverySoftware.Business.Users
{
    public interface IUserController
    {
        void Create (DeliveryUser newUser);
        DeliveryUser Get (Guid uid);
        List<DeliveryUser> GetAll ();
        List<DeliveryUser> GetAllCustomers ();
        List<DeliveryUser> GetAllDrivers ();
        List<DeliveryUser> GetAllEmployees ();
        string GetName (Guid uid);
        void Update (DeliveryUser updatedUser);
    }
}