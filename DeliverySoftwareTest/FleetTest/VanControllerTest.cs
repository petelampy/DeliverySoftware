using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Database;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DeliverySoftwareTest.FleetTest
{
    [TestClass]
    public class VanControllerTest
    {
        [TestMethod]
        public void VanController_Create_GeneratesValidVan ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IVanController _VanController = new VanController(_MockContextManager.Object);

            Van _Van = new Van
            {
                Id = 1,
                Capacity = 30,
                Registration = "VN69 UGB",
                DriverUID = Guid.NewGuid()
            };

            _VanController.Create(_Van);

            Van _Result = new Van();
            _Result = _MockDBContext.Vans.AsEnumerable().FirstOrDefault(new Van());

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.AreEqual(_Van.Registration, _Result.Registration);
            Assert.AreEqual(_Van.Capacity, _Result.Capacity);
            Assert.AreEqual(_Van.DriverUID, _Result.DriverUID);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void VanController_Delete_DeletesCorrectVan ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IVanController _VanController = new VanController(_MockContextManager.Object);

            Guid _MockVanUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Vans.Add(new Van
            {
                Id = 1,
                UID = _MockVanUID,
                Capacity = 15,
                DriverUID = Guid.NewGuid(),
                Registration = "WK04 DHC"
            });

            _MockDBContext.Add(new Van
            {
                Id = 3,
                UID = Guid.NewGuid(),
                Capacity = 25,
                DriverUID = Guid.NewGuid(),
                Registration = "DL60 HBB"
            });
            _MockDBContext.SaveChanges();

            _VanController.Delete(_MockVanUID);

            bool _DoesVanStillExist = _MockDBContext.Vans
                .AsEnumerable()
                .Where(van => van.UID == _MockVanUID)
                .Count() > 0;

            Assert.IsFalse(_DoesVanStillExist);

            Assert.AreEqual(1, _MockDBContext.Vans.Count());

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void VanController_Get_ReturnsCorrectVan ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IVanController _VanController = new VanController(_MockContextManager.Object);

            Guid _MockVanUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            Van _MockVan = new Van
            {
                Id = 45,
                UID = _MockVanUID,
                Capacity = 12,
                DriverUID = Guid.NewGuid(),
                Registration = "L444 MPD"
            };

            _MockDBContext.Vans.Add(_MockVan);
            _MockDBContext.Add(new Van()
            {
                Registration = "WK04 DHC"
            });
            _MockDBContext.SaveChanges();

            Van _Result = _VanController.Get(_MockVanUID);

            Assert.AreEqual(_MockVanUID, _Result.UID);
            Assert.AreEqual(_MockVan.Capacity, _Result.Capacity);
            Assert.AreEqual(_MockVan.Registration, _Result.Registration);
            Assert.AreEqual(_MockVan.DriverUID, _Result.DriverUID);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void VanController_GetCountByDriver_ReturnsCorrectValue ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IVanController _VanController = new VanController(_MockContextManager.Object);

            Guid _MockDriverUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Vans.AddRange(new Van
            {
                UID = Guid.NewGuid(),
                DriverUID = _MockDriverUID,
                Capacity = 5,
                Registration = "WK04 DHC"
            },
            new Van
            {
                UID = Guid.NewGuid(),
                DriverUID = _MockDriverUID,
                Capacity = 12,
                Registration = "DL60 HBB"
            },
            new Van
            {
                UID = Guid.NewGuid(),
                DriverUID = Guid.NewGuid(),
                Capacity = 24,
                Registration = "L444 MPD"
            });
            _MockDBContext.SaveChanges();

            int _Result = _VanController.GetCountByDriver(_MockDriverUID);

            Assert.AreEqual(2, _Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void VanController_GetRegistration_ReturnsCorrectValue ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IVanController _VanController = new VanController(_MockContextManager.Object);

            Guid _MockVanUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            Van _MockVan = new Van
            {
                Id = 45,
                UID = _MockVanUID,
                Capacity = 12,
                DriverUID = Guid.NewGuid(),
                Registration = "L444 MPD"
            };

            _MockDBContext.Vans.Add(_MockVan);
            _MockDBContext.Add(new Van()
            {
                Registration = "WK04 DHC"
            });
            _MockDBContext.SaveChanges();

            string _Result = _VanController.GetRegistration(_MockVanUID);

            Assert.AreEqual("L444 MPD", _Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void VanController_Update_UpdatesCorrectVan ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IVanController _VanController = new VanController(_MockContextManager.Object);

            Guid _MockVanUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Vans.Add(new Van
            {
                Id = 3,
                UID = _MockVanUID,
                Capacity = 12,
                Registration = "L444 MPD"
            });
            _MockDBContext.SaveChanges();

            Van _UpdatedVan = new Van
            {
                UID = _MockVanUID,
                Capacity = 55,
                Registration = "HS53 NEJ"
            };

            _VanController.Update(_UpdatedVan);

            Van _Result = _MockDBContext.Vans
                .AsEnumerable()
                .Where(van => van.UID == _MockVanUID)
                .FirstOrDefault(new Van());

            Assert.AreEqual(_UpdatedVan.Capacity, _Result.Capacity);
            Assert.AreEqual(_UpdatedVan.Registration, _Result.Registration);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }
    }
}
