using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Database;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DeliverySoftwareTest.DeliveryTest
{
    [TestClass]
    public class DeliveryControllerTest
    {
        [TestMethod]
        public void DeliveryController_Create_GeneratesValidDelivery ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            Delivery _Result = new Delivery();

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            Delivery _Delivery = new Delivery
            {
                Id = 1,
                VanUID = Guid.NewGuid()
            };

            _DeliveryController.Create(_Delivery);

            _Result = _MockDBContext.Deliveries.AsEnumerable().FirstOrDefault(new Delivery());

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.AreEqual(_Delivery.Id, _Result.Id);
            Assert.AreEqual(DeliveryStatus.Pending, _Result.Status);
            Assert.AreEqual(1, _Result.CurrentDrop);
            Assert.AreEqual(0, _Result.NumberOfPackages);
            Assert.AreEqual(_Delivery.VanUID, _Result.VanUID);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_Delete_DeletesCorrectDelivery ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            Delivery _Result = new Delivery();

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Deliveries.Add(new Delivery
            {
                Id = 3,
                UID = _MockDeliveryUID,
                VanUID = Guid.NewGuid(),
                CurrentDrop = 1,
                NumberOfPackages = 15,
                Status = DeliveryStatus.Pending
            });
            _MockDBContext.Add(new Delivery());
            _MockDBContext.Add(new Delivery());
            _MockDBContext.SaveChanges();

            _DeliveryController.Delete(_MockDeliveryUID);

            bool _DoesDeliveryStillExist = _MockDBContext.Deliveries
                .AsEnumerable()
                .Where(delivery => delivery.UID == _MockDeliveryUID)
                .Count() > 0;

            Assert.IsFalse(_DoesDeliveryStillExist);

            Assert.AreEqual(2, _MockDBContext.Deliveries.Count());

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_Get_ReturnsCorrectDelivery ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            Delivery _MockDelivery = new Delivery
            {
                Id = 3,
                UID = _MockDeliveryUID,
                VanUID = Guid.NewGuid(),
                CurrentDrop = 4,
                NumberOfPackages = 26,
                Status = DeliveryStatus.Started
            };

            _MockDBContext.Deliveries.Add(_MockDelivery);
            _MockDBContext.Add(new Delivery());
            _MockDBContext.SaveChanges();

            Delivery _Result = _DeliveryController.Get(_MockDeliveryUID);

            Assert.AreEqual(_Result.UID, _MockDeliveryUID);
            Assert.AreEqual(_Result.Status, _MockDelivery.Status);
            Assert.AreEqual(_Result.NumberOfPackages, _MockDelivery.NumberOfPackages);
            Assert.AreEqual(_Result.CurrentDrop, _MockDelivery.CurrentDrop);
            Assert.AreEqual(_Result.VanUID, _MockDelivery.VanUID);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_GetAll_ReturnsCorrectNumberOfResults ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Deliveries.AddRange(new Delivery(), new Delivery(), new Delivery(), new Delivery(), new Delivery());
            _MockDBContext.SaveChanges();

            List<Delivery> _Results = _DeliveryController.GetAll();

            Assert.AreEqual(5, _Results.Count);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_GetAllAvailable_ReturnsCorrectDeliveries ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            _MockDBContext.Database.EnsureDeleted();

            Delivery _MockDelivery = new Delivery
            {
                Id = 12,
                UID = Guid.NewGuid(),
                VanUID = Guid.NewGuid(),
                CurrentDrop = 5,
                NumberOfPackages = 11,
                Status = DeliveryStatus.Started
            };

            _MockDBContext.Deliveries.Add(_MockDelivery);
            _MockDBContext.Add(new Delivery()
            {
                Status = DeliveryStatus.Completed
            });
            _MockDBContext.Add(new Delivery()
            {
                Status = DeliveryStatus.Completed
            });
            _MockDBContext.SaveChanges();

            List<Delivery> _Results = _DeliveryController.GetAllAvailable();

            Assert.AreEqual(1, _Results.Count);

            Delivery _ResultDelivery = _Results.Single();

            Assert.AreEqual(_ResultDelivery.UID, _MockDelivery.UID);
            Assert.AreEqual(_ResultDelivery.Status, _MockDelivery.Status);
            Assert.AreEqual(_ResultDelivery.NumberOfPackages, _MockDelivery.NumberOfPackages);
            Assert.AreEqual(_ResultDelivery.CurrentDrop, _MockDelivery.CurrentDrop);
            Assert.AreEqual(_ResultDelivery.VanUID, _MockDelivery.VanUID);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_GetByVan_ReturnsCorrectDeliveries ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            Guid _MockDeliveryVanUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            Delivery _MockDelivery = new Delivery
            {
                Id = 7,
                UID = Guid.NewGuid(),
                VanUID = _MockDeliveryVanUID,
                CurrentDrop = 7,
                NumberOfPackages = 31,
                Status = DeliveryStatus.Started
            };

            _MockDBContext.Deliveries.Add(_MockDelivery);
            _MockDBContext.Add(new Delivery());
            _MockDBContext.SaveChanges();

            List<Delivery> _Results = _DeliveryController.GetByVan(_MockDeliveryVanUID);

            Assert.AreEqual(1, _Results.Count);

            Delivery _ResultDelivery = _Results.Single();

            Assert.AreEqual(_ResultDelivery.UID, _MockDelivery.UID);
            Assert.AreEqual(_ResultDelivery.Status, _MockDelivery.Status);
            Assert.AreEqual(_ResultDelivery.NumberOfPackages, _MockDelivery.NumberOfPackages);
            Assert.AreEqual(_ResultDelivery.CurrentDrop, _MockDelivery.CurrentDrop);
            Assert.AreEqual(_MockDeliveryVanUID, _MockDelivery.VanUID);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_GetCountByVan_ReturnsCorrectValue ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            Guid _MockVanUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Deliveries.AddRange(new Delivery
            {
                UID = Guid.NewGuid(),
                VanUID = _MockVanUID,
                Status = DeliveryStatus.Started
            },
            new Delivery
            {
                UID = Guid.NewGuid(),
                VanUID = _MockVanUID,
                Status = DeliveryStatus.Started
            },
            new Delivery
            {
                UID = Guid.NewGuid(),
                VanUID = _MockVanUID,
                Status = DeliveryStatus.Completed
            });
            _MockDBContext.SaveChanges();

            int _Result = _DeliveryController.GetCountByVan(_MockVanUID);

            Assert.AreEqual(2, _Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_HasDeliveryRunStarted_ReturnsFalseIfNotStarted ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Deliveries.Add(new Delivery
            {
                Id = 5,
                UID = _MockDeliveryUID,
                VanUID = Guid.NewGuid(),
                CurrentDrop = 4,
                NumberOfPackages = 27,
                Status = DeliveryStatus.Pending
            });
            _MockDBContext.SaveChanges();

            bool _Result = _DeliveryController.HasDeliveryRunStarted(_MockDeliveryUID);

            Assert.IsFalse(_Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_HasDeliveryRunStarted_ReturnsTrueIfStarted ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Deliveries.Add(new Delivery
            {
                Id = 5,
                UID = _MockDeliveryUID,
                VanUID = Guid.NewGuid(),
                CurrentDrop = 4,
                NumberOfPackages = 27,
                Status = DeliveryStatus.Started
            });
            _MockDBContext.SaveChanges();

            bool _Result = _DeliveryController.HasDeliveryRunStarted(_MockDeliveryUID);

            Assert.IsTrue(_Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void DeliveryController_Update_UpdatesCorrectDelivery ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            Delivery _Result = new Delivery();

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            IDeliveryController _DeliveryController = new DeliveryController(_MockContextManager.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Deliveries.Add(new Delivery
            {
                Id = 1,
                UID = _MockDeliveryUID,
                VanUID = Guid.NewGuid(),
                CurrentDrop = 1,
                NumberOfPackages = 12,
                Status = DeliveryStatus.Pending
            });
            _MockDBContext.SaveChanges();

            Delivery _UpdatedDelivery = new Delivery
            {
                UID = _MockDeliveryUID,
                VanUID = Guid.NewGuid(),
                CurrentDrop = 3,
                NumberOfPackages = 17,
                Status = DeliveryStatus.Started
            };

            _DeliveryController.Update(_UpdatedDelivery);

            _Result = _MockDBContext.Deliveries
                .AsEnumerable()
                .Where(delivery => delivery.UID == _MockDeliveryUID)
                .FirstOrDefault(new Delivery());

            Assert.AreEqual(_UpdatedDelivery.VanUID, _Result.VanUID);
            Assert.AreEqual(_UpdatedDelivery.Status, _Result.Status);
            Assert.AreEqual(_UpdatedDelivery.CurrentDrop, _Result.CurrentDrop);
            Assert.AreEqual(_UpdatedDelivery.NumberOfPackages, _Result.NumberOfPackages);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }
    }
}
