using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using DeliverySoftware.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverySoftwareTest.DeliveryTest
{
    [TestClass]
    public class PackageControllerTest
    {
        [TestMethod]
        public void PackageController_Create_GeneratesValidPackage ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Package _Package = new Package
            {
                Id = 1,
                CustomerUID = Guid.NewGuid(),
                Description = "test package",
                Size = 20,
                TrackingCode = "TESTTRACK123"
            };

            _PackageController.Create(_Package);

            Package _Result = new Package();
            _Result = _MockDBContext.Packages.AsEnumerable().FirstOrDefault(new Package());

            Assert.AreNotEqual(Guid.Empty, _Result.UID);

            Assert.AreEqual(_Package.Id, _Result.Id);
            Assert.AreEqual(_Package.Size, _Result.Size);
            Assert.AreEqual(_Package.Description, _Result.Description);
            Assert.AreEqual(_Package.TrackingCode, _Result.TrackingCode);

            Assert.AreEqual(0, _Result.DropNumber);
            Assert.IsFalse(_Result.IsAssignedToDelivery);
            Assert.IsFalse(_Result.IsDelivered);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_Delete_DeletesCorrectPackage ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            Delivery _Result = new Delivery();

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockPackageUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Packages.Add(new Package
            {
                Id = 5,
                UID = _MockPackageUID,
                Description = "test",
                TrackingCode = "test"
            });

            _MockDBContext.Add(new Package()
            {
                Description = "test",
                TrackingCode = "test"
            });
            _MockDBContext.SaveChanges();

            _PackageController.Delete(_MockPackageUID);

            bool _DoesPackageStillExist = _MockDBContext.Packages
                .AsEnumerable()
                .Where(package => package.UID == _MockPackageUID)
                .Count() > 0;

            Assert.IsFalse(_DoesPackageStillExist);

            Assert.AreEqual(1, _MockDBContext.Packages.Count());

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_Update_UpdatesCorrectPackage ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockPackageUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Packages.Add(new Package
            {
                Id = 8,
                UID = _MockPackageUID,
                TrackingCode = "testtracker",
                Description = "a test package",
                DropNumber = 1,
                IsAssignedToDelivery = false,
                DeliveryUID = Guid.Empty
            });
            _MockDBContext.SaveChanges();

            Package _UpdatedPackage = new Package
            {
                UID = _MockPackageUID,
                TrackingCode = "test update",
                Description = "updated description",
                IsAssignedToDelivery = true,
                DeliveryUID = Guid.NewGuid()
            };

            _PackageController.Update(_UpdatedPackage);

            Package _Result = _MockDBContext.Packages
                .AsEnumerable()
                .Where(package => package.UID == _MockPackageUID)
                .FirstOrDefault(new Package());

            Assert.AreEqual(_UpdatedPackage.Description, _Result.Description);
            Assert.AreEqual(_UpdatedPackage.TrackingCode, _Result.TrackingCode);
            Assert.AreEqual(_UpdatedPackage.IsAssignedToDelivery, _Result.IsAssignedToDelivery);
            
            Assert.AreNotEqual(Guid.Empty, _Result.DeliveryUID);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_GetActivePackagesByCustomer_ReturnsCorrectValue ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockCustomerUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Packages.AddRange(new Package
            {
                UID = Guid.NewGuid(),
                CustomerUID = _MockCustomerUID,
                TrackingCode = "test 1",
                Description = "Description 1",
                IsDelivered = false
            },
            new Package
            {
                UID = Guid.NewGuid(),
                CustomerUID = _MockCustomerUID,
                TrackingCode = "test 2",
                Description = "Description 2",
                IsDelivered = false
            },
            new Package
            {
                UID = Guid.NewGuid(),
                CustomerUID = _MockCustomerUID,
                TrackingCode = "test 3",
                Description = "Description 3",
                IsDelivered = true
            });
            _MockDBContext.SaveChanges();

            int _Result = _PackageController.GetActivePackagesByCustomer(_MockCustomerUID);

            Assert.AreEqual(2, _Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_DoesTrackingCodeExist_ReturnsTrueIfExists ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockCustomerUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Packages.AddRange(new Package
            {
                UID = Guid.NewGuid(),
                CustomerUID = _MockCustomerUID,
                TrackingCode = "MYTRACKINGCODE",
                Description = "Description 1",
                IsDelivered = false
            });
            _MockDBContext.SaveChanges();

            bool _Result = _PackageController.DoesTrackingCodeExist("MYTRACKINGCODE");

            Assert.IsTrue(_Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_DoesTrackingCodeExist_ReturnsFalseIfDoesNotExist ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockCustomerUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Packages.AddRange(new Package
            {
                UID = Guid.NewGuid(),
                CustomerUID = _MockCustomerUID,
                TrackingCode = "MYTRACKINGCODE",
                Description = "Description 1",
                IsDelivered = false
            });
            _MockDBContext.SaveChanges();

            bool _Result = _PackageController.DoesTrackingCodeExist("AFAKETRACKINGCODE");

            Assert.IsFalse(_Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_GetPackagesByDelivery_ReturnsCorrectPackages ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            Package _MockPackage = new Package
            {
                Id = 7,
                UID = Guid.NewGuid(),
                IsAssignedToDelivery= true,
                DeliveryUID = _MockDeliveryUID,
                Description = "test",
                TrackingCode = "test code",
                DropNumber = 5,
                Size = 50
            };

            _MockDBContext.Packages.Add(_MockPackage);
            _MockDBContext.Add(new Package()
            {
                Description = "test 2",
                TrackingCode = "test code 2",
            });
            _MockDBContext.SaveChanges();

            List<Package> _Results = _PackageController.GetPackagesByDelivery(_MockDeliveryUID);

            Assert.AreEqual(1, _Results.Count);

            Package _ResultPackage = _Results.Single();

            Assert.AreEqual(_MockDeliveryUID, _ResultPackage.DeliveryUID);

            Assert.AreEqual(_MockPackage.UID, _ResultPackage.UID);
            Assert.AreEqual(_MockPackage.IsAssignedToDelivery, _ResultPackage.IsAssignedToDelivery);
            Assert.AreEqual(_MockPackage.TrackingCode, _ResultPackage.TrackingCode);
            Assert.AreEqual(_MockPackage.Description, _ResultPackage.Description);
            Assert.AreEqual(_MockPackage.DropNumber, _ResultPackage.DropNumber);
            Assert.AreEqual(_MockPackage.Size, _ResultPackage.Size);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_GetAllUndelivered_ReturnsCorrectPackages ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            _MockDBContext.Database.EnsureDeleted();

            Package _MockPackage = new Package
            {
                Id = 53,
                UID = Guid.NewGuid(),
                IsAssignedToDelivery = true,
                DeliveryUID = Guid.NewGuid(),
                Description = "test",
                TrackingCode = "testing code",
                DropNumber = 1,
                Size = 10,
                IsDelivered = false
            };

            _MockDBContext.Packages.Add(_MockPackage);
            _MockDBContext.Add(new Package()
            {
                Description = "test 2",
                TrackingCode = "test code 2",
                IsDelivered = true,
            });
            _MockDBContext.SaveChanges();

            List<Package> _Results = _PackageController.GetAllUndelivered();

            Assert.AreEqual(1, _Results.Count);

            Package _ResultPackage = _Results.Single();

            Assert.AreEqual(_MockPackage.DeliveryUID, _ResultPackage.DeliveryUID);
            Assert.AreEqual(_MockPackage.UID, _ResultPackage.UID);
            Assert.AreEqual(_MockPackage.IsAssignedToDelivery, _ResultPackage.IsAssignedToDelivery);
            Assert.AreEqual(_MockPackage.TrackingCode, _ResultPackage.TrackingCode);
            Assert.AreEqual(_MockPackage.Description, _ResultPackage.Description);
            Assert.AreEqual(_MockPackage.DropNumber, _ResultPackage.DropNumber);
            Assert.AreEqual(_MockPackage.Size, _ResultPackage.Size);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_GetByTrackingCode_ReturnsCorrectPackage ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            _MockDBContext.Database.EnsureDeleted();

            Package _MockPackage = new Package
            {
                Id = 15,
                UID = Guid.NewGuid(),
                IsAssignedToDelivery = true,
                DeliveryUID = Guid.NewGuid(),
                Description = "test description",
                TrackingCode = "AFANCYTRACKINGCODE",
                DropNumber = 3,
                Size = 25
            };
            _MockDBContext.Packages.Add(_MockPackage);
            _MockDBContext.SaveChanges();

            Package _Result = _PackageController.GetByTrackingCode("AFANCYTRACKINGCODE");

            Assert.AreEqual("AFANCYTRACKINGCODE", _Result.TrackingCode);

            Assert.AreEqual(_MockPackage.UID, _Result.UID);
            Assert.AreEqual(_MockPackage.IsAssignedToDelivery, _Result.IsAssignedToDelivery);
            Assert.AreEqual(_MockPackage.Description, _Result.Description);
            Assert.AreEqual(_MockPackage.DropNumber, _Result.DropNumber);
            Assert.AreEqual(_MockPackage.Size, _Result.Size);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_GetPackageCountByDelivery_ReturnsCorrectValue ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Packages.AddRange(new Package
            {
                UID = Guid.NewGuid(),
                DeliveryUID = _MockDeliveryUID,
                TrackingCode = "test 33",
                Description = "Description 44",
                IsDelivered = false
            },
            new Package
            {
                UID = Guid.NewGuid(),
                DeliveryUID = _MockDeliveryUID,
                TrackingCode = "test 33",
                Description = "Description 11",
                IsDelivered = false
            },
            new Package
            {
                UID = Guid.NewGuid(),
                DeliveryUID = _MockDeliveryUID,
                TrackingCode = "test 1",
                Description = "Description 45",
                IsDelivered = false
            },
            new Package
            {
                TrackingCode = "test 334",
                Description = "Description 111",
            });
            _MockDBContext.SaveChanges();

            int _Result = _PackageController.GetPackageCountByDelivery(_MockDeliveryUID);

            Assert.AreEqual(3, _Result);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_GetByDeliveryAndDropNumber_ReturnsCorrectPackage ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            Package _MockPackage = new Package
            {
                Id = 7,
                UID = Guid.NewGuid(),
                IsAssignedToDelivery = true,
                DeliveryUID = _MockDeliveryUID,
                Description = "test",
                TrackingCode = "test tracking code",
                DropNumber = 2,
                Size = 50
            };

            _MockDBContext.Packages.Add(_MockPackage);
            _MockDBContext.Add(new Package()
            {
                Description = "test 2",
                TrackingCode = "test code 2",
                DeliveryUID = _MockDeliveryUID,
                DropNumber = 1
            });
            _MockDBContext.SaveChanges();

            Package _Result = _PackageController.GetByDeliveryAndDropNumber(_MockDeliveryUID, 2);

            Assert.AreEqual(_MockDeliveryUID, _Result.DeliveryUID);
            Assert.AreEqual(2, _Result.DropNumber);

            Assert.AreEqual(_MockPackage.UID, _Result.UID);
            Assert.AreEqual(_MockPackage.IsAssignedToDelivery, _Result.IsAssignedToDelivery);
            Assert.AreEqual(_MockPackage.TrackingCode, _Result.TrackingCode);
            Assert.AreEqual(_MockPackage.Description, _Result.Description);
            Assert.AreEqual(_MockPackage.Size, _Result.Size);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_Get_ReturnsCorrectPackage ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();
            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockPackageUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            Package _MockPackage = new Package
            {
                Id = 45,
                UID = _MockPackageUID,
                IsAssignedToDelivery= true,
                DeliveryUID = Guid.NewGuid(),
                CustomerUID = Guid.NewGuid(),
                DropNumber = 5,
                Size = 300,
                Description = "test desc",
                TrackingCode = "test tracker"
            };

            _MockDBContext.Packages.Add(_MockPackage);
            _MockDBContext.Add(new Package()
            {
                Description = "test 2",
                TrackingCode = "another code"
            });
            _MockDBContext.SaveChanges();

            Package _Result = _PackageController.Get(_MockPackageUID);

            Assert.AreEqual(_MockPackageUID, _Result.UID);
            Assert.AreEqual(_MockPackage.IsAssignedToDelivery, _Result.IsAssignedToDelivery);
            Assert.AreEqual(_MockPackage.DeliveryUID, _Result.DeliveryUID);
            Assert.AreEqual(_MockPackage.CustomerUID, _Result.CustomerUID);
            Assert.AreEqual(_MockPackage.DropNumber, _Result.DropNumber);
            Assert.AreEqual(_MockPackage.Size, _Result.Size);
            Assert.AreEqual(_MockPackage.TrackingCode, _Result.TrackingCode);
            Assert.AreEqual(_MockPackage.Description, _Result.Description);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }

        [TestMethod]
        public void PackageController_UpdateDeliveryRunDropOrder_SetsCorrectDropNumbers ()
        {
            Mock<IDBContextManager> _MockContextManager = new Mock<IDBContextManager>();

            DbContextOptions<DeliveryDBContext> _DBContextOptions = new DbContextOptionsBuilder<DeliveryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            DeliveryDBContext _MockDBContext = new DeliveryDBContext(_DBContextOptions);

            _MockContextManager.Setup(mock => mock.CreateNewDatabaseContext()).Returns(_MockDBContext);

            Mock<IUserController> _MockUserController = new Mock<IUserController>();

            Guid _MockCustomer1UID = Guid.NewGuid();
            Guid _MockCustomer2UID = Guid.NewGuid();
            Guid _MockCustomer3UID = Guid.NewGuid();

            DeliveryUser _MockUser1 = new DeliveryUser
            {
                Id = _MockCustomer1UID.ToString(),
                HouseNumber = 3,
                PostCode = "CV23 0RD"
            };
            DeliveryUser _MockUser2 = new DeliveryUser
            {
                Id = _MockCustomer2UID.ToString(),
                HouseNumber = 7,
                PostCode = "LE16 9RS"
            };
            DeliveryUser _MockUser3 = new DeliveryUser
            {
                Id = _MockCustomer3UID.ToString(),
                HouseNumber = 2,
                PostCode = "LE17 4HY"
            };

            _MockUserController.Setup(mock => mock.Get(_MockCustomer1UID)).Returns(_MockUser1);
            _MockUserController.Setup(mock => mock.Get(_MockCustomer2UID)).Returns(_MockUser2);
            _MockUserController.Setup(mock => mock.Get(_MockCustomer3UID)).Returns(_MockUser3);

            Mock<IVanController> _MockVanController = new Mock<IVanController>();
            Mock<IDeliveryController> _MockDeliveryController = new Mock<IDeliveryController>();

            _MockDeliveryController.Setup(mock => mock.Get(It.IsAny<Guid>())).Returns(new Delivery()
            {
                VanUID = Guid.NewGuid()
            });

            _MockVanController.Setup(mock => mock.Get(It.IsAny<Guid>())).Returns(new Van()
            {
                DepotPostCode = "B92 0DP"
            });

            IPackageController _PackageController = new PackageController(_MockContextManager.Object, _MockUserController.Object,
                _MockVanController.Object, _MockDeliveryController.Object);

            Guid _MockDeliveryUID = Guid.NewGuid();

            _MockDBContext.Database.EnsureDeleted();

            _MockDBContext.Packages.AddRange(new Package
            {
                UID = Guid.NewGuid(),
                DeliveryUID = _MockDeliveryUID,
                CustomerUID = _MockCustomer1UID,
                Description = "test desc",
                TrackingCode = "test tracker",
                DropNumber = 2
            }, new Package
            {
                UID = Guid.NewGuid(),
                DeliveryUID = _MockDeliveryUID,
                CustomerUID = _MockCustomer2UID,
                Description = "test desc",
                TrackingCode = "test tracker",
                DropNumber = 3
            }, new Package
            {
                UID = Guid.NewGuid(),
                DeliveryUID = _MockDeliveryUID,
                CustomerUID = _MockCustomer3UID,
                Description = "test desc",
                TrackingCode = "test tracker",
                DropNumber = 1
            });            

            _MockDBContext.SaveChanges();

            _PackageController.UpdateDeliveryRunDropOrder(_MockDeliveryUID);

            Package _Result1 = _MockDBContext.Packages.Where(package => package.CustomerUID == _MockCustomer1UID).Single();
            Package _Result2 = _MockDBContext.Packages.Where(package => package.CustomerUID == _MockCustomer2UID).Single();
            Package _Result3 = _MockDBContext.Packages.Where(package => package.CustomerUID == _MockCustomer3UID).Single();

            Assert.AreEqual(1, _Result1.DropNumber);
            Assert.AreEqual(3, _Result2.DropNumber);
            Assert.AreEqual(2, _Result3.DropNumber);

            _MockContextManager.Verify(mock => mock.CreateNewDatabaseContext(), Times.Once());
        }
    }
    
}
