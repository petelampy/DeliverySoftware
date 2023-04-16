namespace DeliverySoftware.Business.Fleet
{
    public interface IVanController
    {
        void Create (Van newVan);
        Van Get (Guid uid);
        List<Van> GetAll ();
        int GetCountByDriver (Guid driver_uid);
        string GetRegistration (Guid uid);
        void Update (Van updatedVan);
    }
}