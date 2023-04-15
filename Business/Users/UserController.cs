using DeliverySoftware.Database;

namespace DeliverySoftware.Business.Users
{
    public class UserController : IUserController
    {
        private readonly DeliveryDBContext __DbContext;
        private readonly IDBContextManager __DbContextManager;

        public UserController () :
            this(new DBContextManager())
        { }

        internal UserController (IDBContextManager dbContextManager)
        {
            __DbContextManager = dbContextManager;
            __DbContext = __DbContextManager.CreateNewDatabaseContext();
        }

        public DeliveryUser Get (Guid uid)
        {
            return __DbContext.Users
                 .ToList()
                 .Where(user => user.Id == uid.ToString())
                 .SingleOrDefault(new DeliveryUser());
        }

        public List<DeliveryUser> GetAll ()
        {
            return __DbContext.Users.ToList();
        }
    }
}
