namespace DeliverySoftware.Business.Fleet
{
    public interface IVanController
    {
        List<Van> GetAll ();
        int GetCountByDriver (Guid driver_uid);
        string GetRegistration (Guid uid);
    }
}