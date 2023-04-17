using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Utilities;
using DeliverySoftware.Database;
using Microsoft.AspNetCore.Identity;

namespace DeliverySoftware.Business.Users
{
    public class UserController : IUserController
    {
        private readonly DeliveryDBContext __DbContext;
        private readonly IDBContextManager __DbContextManager;
        private readonly IPasswordHasher<DeliveryUser> __PasswordHasher;
        private readonly IEmailController __EmailController;

        public UserController () :
            this(new DBContextManager(), new PasswordHasher<DeliveryUser>(), new EmailController())
        { }

        internal UserController (IDBContextManager dbContextManager, IPasswordHasher<DeliveryUser> passwordHasher, IEmailController emailController)
        {
            __DbContextManager = dbContextManager;
            __DbContext = __DbContextManager.CreateNewDatabaseContext();
            __PasswordHasher = passwordHasher;
            __EmailController = emailController;
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

        private int GenerateTemporaryPassword ()
        {
            return new Random().Next(1000000, 9999999);
        }

        public void Create (DeliveryUser newUser)
        {
            if (newUser.UserType == UserType.Customer)
            {
                Random _Random = new Random();
                int _HashValue = _Random.Next(0, 100000) + DateTime.Now.Millisecond;

                newUser.UserName = "CustomerAccount" + _HashValue;
            }

            int _TempPassword = GenerateTemporaryPassword();

            if (newUser.UserType == UserType.Driver || newUser.UserType == UserType.Employee)
            {
                Email _PasswordConfirmationEmail = new Email()
                {
                    Recipient = newUser.Email,
                    Subject = "Your Delivery Account - " + newUser.Forename,
                    Body = "Welcome to your account " + newUser.Forename + " " + newUser.Surname + "\n"
                        + "Your username to login is: " + newUser.UserName + "\n"
                        + "Your temporary password is: " + _TempPassword + "\n\n"
                        + "Please change this password as soon as possible!" + "\n\n"
                        + "Thank you, Delivery+ Software Solutions"
                };

                __EmailController.SendEmail(_PasswordConfirmationEmail);
            }

            newUser.Id = Guid.NewGuid().ToString();
            newUser.PasswordHash = __PasswordHasher.HashPassword(newUser, _TempPassword.ToString());
            newUser.NormalizedUserName = newUser.UserName.ToUpper();
            newUser.NormalizedEmail = newUser.Email.ToUpper();
            newUser.SecurityStamp = Guid.NewGuid().ToString();

            __DbContext.Users.Add(newUser);
            __DbContext.SaveChanges();
        }

        public void Update (DeliveryUser updatedUser)
        {
            DeliveryUser _CurrentUser = Get(new Guid(updatedUser.Id));

            if(_CurrentUser.UserType != UserType.Customer)
            {
                _CurrentUser.UserName = updatedUser.UserName;
                _CurrentUser.NormalizedUserName = updatedUser.UserName.ToUpper();
            }

            if(_CurrentUser.UserType == UserType.Customer)
            {
                _CurrentUser.Address = updatedUser.Address;
                _CurrentUser.HouseNumber = updatedUser.HouseNumber;
                _CurrentUser.PostCode = updatedUser.PostCode;
            }
            
            _CurrentUser.Forename = updatedUser.Forename;
            _CurrentUser.Surname = updatedUser.Surname;
            _CurrentUser.Email = updatedUser.Email;
            _CurrentUser.NormalizedEmail = updatedUser.Email.ToUpper();

            __DbContext.SaveChanges();
        }

        public void Delete (Guid uid)
        {
            DeliveryUser _User = Get(uid);

            __DbContext.Remove(_User);
            __DbContext.SaveChanges();
        }
    }
}
