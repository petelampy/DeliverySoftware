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

        public string GetName (Guid uid)
        {
            DeliveryUser _User = __DbContext.Users
                 .ToList()
                 .Where(user => user.Id == uid.ToString())
                 .SingleOrDefault(new DeliveryUser());

            return _User.Forename + " " + _User.Surname;
        }

        public List<DeliveryUser> GetAll ()
        {
            return __DbContext.Users.ToList();
        }

        public List<DeliveryUser> GetAllDrivers ()
        {
            return GetAll()
                .Where(user => user.UserType == UserType.Driver)
                .ToList();
        }

        public List<DeliveryUser> GetAllCustomers ()
        {
            return GetAll()
                .Where(user => user.UserType == UserType.Customer)
                .ToList();
        }

        public List<DeliveryUser> GetAllEmployees ()
        {
            return GetAll()
                .Where(user => user.UserType == UserType.Employee)
                .ToList();
        }
    }
}
