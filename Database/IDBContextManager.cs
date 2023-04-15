namespace DeliverySoftware.Database
{
    public interface IDBContextManager
    {
        DeliveryDBContext CreateNewDatabaseContext ();
    }
}