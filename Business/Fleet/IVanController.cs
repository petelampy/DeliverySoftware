namespace DeliverySoftware.Business.Fleet
{
    public interface IVanController
    {
        int GetCountByDriver (Guid driver_uid);
        string GetRegistration (Guid uid);
    }
}