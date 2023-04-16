namespace DeliverySoftware.Business.Fleet
{
    public interface IVanController
    {
        int GetCountByDriver (Guid driver_uid);
    }
}