using DeliverySoftware.Business.Users;
using DeliverySoftware.Business.Utilities;
using DeliverySoftware.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DeliverySoftwareTest.UserTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void UserController_Create_CreatesValidUser ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IPasswordHasher<DeliveryUser> _MockHasher = new PasswordHasher<DeliveryUser>();
            Mock<IEmailController> _MockEmailController = new Mock<IEmailController>();

            Email _SentEmail = new Email();

            _MockEmailController.Setup(mock => mock.SendEmail(It.IsAny<Email>())).Callback((Email email) =>
            {
                _SentEmail = email;
            });

            IUserController _UserController = new UserController(_MockContextManager.Object, _MockHasher, _MockEmailController.Object);

            Guid _MockUserUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            DeliveryUser _MockUser = new DeliveryUser
            {
                Email = "test@test.com",
                UserName = "test",
                Forename = "Peter",
                Surname = "Lampard",
                UserType = UserType.Driver
            };

            _UserController.Create(_MockUser);


            DeliveryUser _Result = new DeliveryUser();
            _Result = _MockDBContext.Users.AsEnumerable().FirstOrDefault(new DeliveryUser());


            Assert.AreNotEqual(Guid.Empty.ToString(), _Result.Id);

            Assert.AreEqual(_MockUser.Email, _Result.Email);
            Assert.AreEqual(_MockUser.UserName, _Result.UserName);
            Assert.AreEqual(_MockUser.Forename, _Result.Forename);
            Assert.AreEqual(_MockUser.Surname, _Result.Surname);
            Assert.AreEqual(_MockUser.UserType, _Result.UserType);

            Assert.AreEqual(_MockUser.Email, _SentEmail.Recipient);
            Assert.AreEqual("Your Delivery Account - Peter", _SentEmail.Subject);

            Assert.AreEqual(1, _MockDBContext.Users.Count());

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void UserController_Delete_RemovesCorrectUser ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IPasswordHasher<DeliveryUser>> _MockHasher = new Mock<IPasswordHasher<DeliveryUser>>();
            Mock<IEmailController> _MockEmailController = new Mock<IEmailController>();

            IUserController _UserController = new UserController(_MockContextManager.Object, _MockHasher.Object, _MockEmailController.Object);

            Guid _MockUserUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            DeliveryUser _MockUser = new DeliveryUser
            {
                Id = _MockUserUID.ToString(),
                Email = "test@test.com",
                UserName = "test",
                Forename = "Peter",
                Surname = "Lampard",
                HouseNumber = 25,
                PostCode = "W1T 3BF",
                Address = "25 Cleveland St, London W1T 3BF",
                UserType = UserType.Customer
            };

            _MockDBContext.Users.Add(_MockUser);
            _MockDBContext.Add(new DeliveryUser()
            {
                Forename = "test",
                Surname = "test"
            });
            _MockDBContext.SaveChanges();

            _UserController.Delete(_MockUserUID);

            bool _DoesUserStillExist = _MockDBContext.Users
                .AsEnumerable()
                .Where(user => user.Id == _MockUserUID.ToString())
                .Count() > 0;

            Assert.IsFalse(_DoesUserStillExist);

            Assert.AreEqual(1, _MockDBContext.Users.Count());

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void UserController_Get_ReturnsCorrectUser ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IPasswordHasher<DeliveryUser>> _MockHasher = new Mock<IPasswordHasher<DeliveryUser>>();
            Mock<IEmailController> _MockEmailController = new Mock<IEmailController>();

            IUserController _UserController = new UserController(_MockContextManager.Object, _MockHasher.Object, _MockEmailController.Object);


            Guid _MockUserUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            DeliveryUser _MockUser = new DeliveryUser
            {
                Id = _MockUserUID.ToString(),
                Email = "test@test.com",
                UserName = "test",
                Forename = "Peter",
                Surname = "Lampard",
                HouseNumber = 25,
                PostCode = "W1T 3BF",
                Address = "25 Cleveland St, London W1T 3BF",
                UserType = UserType.Customer
            };

            _MockDBContext.Users.Add(_MockUser);
            _MockDBContext.Add(new DeliveryUser()
            {
                Forename = "test",
                Surname = "test"
            });
            _MockDBContext.SaveChanges();

            DeliveryUser _Result = _UserController.Get(_MockUserUID);

            Assert.AreEqual(_MockUserUID.ToString(), _Result.Id);
            Assert.AreEqual(_MockUser.Id, _Result.Id);
            Assert.AreEqual(_MockUser.Email, _Result.Email);
            Assert.AreEqual(_MockUser.UserName, _Result.UserName);
            Assert.AreEqual(_MockUser.Forename, _Result.Forename);
            Assert.AreEqual(_MockUser.Surname, _Result.Surname);
            Assert.AreEqual(_MockUser.HouseNumber, _Result.HouseNumber);
            Assert.AreEqual(_MockUser.PostCode, _Result.PostCode);
            Assert.AreEqual(_MockUser.Address, _Result.Address);
            Assert.AreEqual(_MockUser.UserType, _Result.UserType);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void UserController_GetAllCustomers_ReturnsCorrectUsers ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IPasswordHasher<DeliveryUser>> _MockHasher = new Mock<IPasswordHasher<DeliveryUser>>();
            Mock<IEmailController> _MockEmailController = new Mock<IEmailController>();

            IUserController _UserController = new UserController(_MockContextManager.Object, _MockHasher.Object, _MockEmailController.Object);


            Guid _MockUserUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.AddRange(new DeliveryUser()
            {
                Forename = "test2",
                Surname = "test2",
                UserType = UserType.Driver
            }, new DeliveryUser()
            {
                Forename = "test3",
                Surname = "test3",
                UserType = UserType.Driver
            }, new DeliveryUser()
            {
                Forename = "test",
                Surname = "test",
                UserType = UserType.Customer
            });
            _MockDBContext.SaveChanges();

            List<DeliveryUser> _Result = _UserController.GetAllCustomers();

            bool _IsCustomerPresent = _Result.Any(user => user.Forename == "test");

            Assert.IsTrue(_IsCustomerPresent);
            Assert.AreEqual(1, _Result.Count());

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void UserController_GetAllDrivers_ReturnsCorrectUsers ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IPasswordHasher<DeliveryUser>> _MockHasher = new Mock<IPasswordHasher<DeliveryUser>>();
            Mock<IEmailController> _MockEmailController = new Mock<IEmailController>();

            IUserController _UserController = new UserController(_MockContextManager.Object, _MockHasher.Object, _MockEmailController.Object);


            Guid _MockUserUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.AddRange(new DeliveryUser()
            {
                Forename = "test2",
                Surname = "test2",
                UserType = UserType.Driver
            }, new DeliveryUser()
            {
                Forename = "test3",
                Surname = "test3",
                UserType = UserType.Driver
            }, new DeliveryUser()
            {
                Forename = "test",
                Surname = "test",
                UserType = UserType.Customer
            });
            _MockDBContext.SaveChanges();

            List<DeliveryUser> _Result = _UserController.GetAllDrivers();

            bool _IsDriverPresent = _Result.Any(user => user.Forename == "test3");

            Assert.IsTrue(_IsDriverPresent);
            Assert.AreEqual(2, _Result.Count());

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void UserController_GetAllEmployees_ReturnsCorrectUsers ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IPasswordHasher<DeliveryUser>> _MockHasher = new Mock<IPasswordHasher<DeliveryUser>>();
            Mock<IEmailController> _MockEmailController = new Mock<IEmailController>();

            IUserController _UserController = new UserController(_MockContextManager.Object, _MockHasher.Object, _MockEmailController.Object);


            Guid _MockUserUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.AddRange(new DeliveryUser()
            {
                Forename = "test2",
                Surname = "test2",
                UserType = UserType.Employee
            }, new DeliveryUser()
            {
                Forename = "test3",
                Surname = "test3",
                UserType = UserType.Employee
            }, new DeliveryUser()
            {
                Forename = "test",
                Surname = "test",
                UserType = UserType.Employee
            });
            _MockDBContext.SaveChanges();

            List<DeliveryUser> _Result = _UserController.GetAllEmployees();

            bool _IsEmployeePresent = _Result.Any(user => user.Forename == "test2");

            Assert.IsTrue(_IsEmployeePresent);
            Assert.AreEqual(3, _Result.Count());

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void UserController_GetName_ReturnsCorrectName ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IPasswordHasher<DeliveryUser>> _MockHasher = new Mock<IPasswordHasher<DeliveryUser>>();
            Mock<IEmailController> _MockEmailController = new Mock<IEmailController>();

            IUserController _UserController = new UserController(_MockContextManager.Object, _MockHasher.Object, _MockEmailController.Object);


            Guid _MockUserUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            DeliveryUser _MockUser = new DeliveryUser
            {
                Id = _MockUserUID.ToString(),
                Email = "test@test.com",
                UserName = "test",
                Forename = "Peter",
                Surname = "Lampard",
                HouseNumber = 25,
                PostCode = "W1T 3BF",
                Address = "25 Cleveland St, London W1T 3BF",
                UserType = UserType.Customer
            };

            _MockDBContext.Users.Add(_MockUser);
            _MockDBContext.Add(new DeliveryUser()
            {
                Forename = "test",
                Surname = "test"
            });
            _MockDBContext.SaveChanges();

            string _Result = _UserController.GetName(_MockUserUID);

            Assert.AreEqual("Peter Lampard", _Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void UserController_Update_UpdatesCorrectUser ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IPasswordHasher<DeliveryUser>> _MockHasher = new Mock<IPasswordHasher<DeliveryUser>>();
            Mock<IEmailController> _MockEmailController = new Mock<IEmailController>();

            IUserController _UserController = new UserController(_MockContextManager.Object, _MockHasher.Object, _MockEmailController.Object);

            Guid _MockUserUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            DeliveryUser _MockUser = new DeliveryUser
            {
                Id = _MockUserUID.ToString(),
                Email = "test@test.com",
                UserName = "test",
                Forename = "Peter",
                Surname = "Lampard",
                HouseNumber = 25,
                PostCode = "W1T 3BF",
                Address = "25 Cleveland St, London W1T 3BF",
                UserType = UserType.Customer
            };

            _MockDBContext.Users.Add(_MockUser);
            _MockDBContext.Add(new DeliveryUser()
            {
                Forename = "test",
                Surname = "test"
            });
            _MockDBContext.SaveChanges();

            DeliveryUser _UpdatedMockUser = new DeliveryUser
            {
                Id = _MockUserUID.ToString(),
                Email = "test33@test.com",
                UserName = "test",
                Forename = "Petrol",
                Surname = "Car",
                HouseNumber = 12,
                PostCode = "W1T 4AA",
                Address = "33 Cleveland St, London W1T 3BF"
            };

            _UserController.Update(_UpdatedMockUser);

            DeliveryUser _Result = _MockDBContext.Users
                .AsEnumerable()
                .Where(user => user.Id == _MockUserUID.ToString())
                .FirstOrDefault(new DeliveryUser());

            Assert.AreEqual(_UpdatedMockUser.Email, _Result.Email);
            Assert.AreEqual(_UpdatedMockUser.UserName, _Result.UserName);
            Assert.AreEqual(_UpdatedMockUser.Forename, _Result.Forename);
            Assert.AreEqual(_UpdatedMockUser.Surname, _Result.Surname);
            Assert.AreEqual(_UpdatedMockUser.HouseNumber, _Result.HouseNumber);
            Assert.AreEqual(_UpdatedMockUser.PostCode, _Result.PostCode);
            Assert.AreEqual(_UpdatedMockUser.Address, _Result.Address);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }
    }
}
