using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Identity;

namespace DeliverySoftware.Database
{
    public class SampleDataManager
    {
        private static readonly Guid __SampleCustomerUID = new Guid("751e25c8-68ba-49dc-b1de-15fcb0bf210f");
        private static readonly Guid __SampleDeliveryUID = new Guid("3f3cf1cd-131d-4d05-93b9-21dbd082fcac");
        private static readonly Guid __SampleDriverUID = new Guid("16776aef-a5ff-424d-be7e-ea3fd591ce90");
        private static readonly Guid __SampleEmployeeUID = new Guid("ca6f5584-527f-4820-802f-bd329352c3e5");
        private static readonly Guid __SamplePackageUID = new Guid("721a28e5-03b7-4c9a-863a-53d58e23d64a");
        private static readonly Guid __SampleVanUID = new Guid("f2b87d27-d4d8-425e-8df8-b3e7bf7f6460");

        public static Delivery GenerateDelivery ()
        {
            return new Delivery
            {
                Id = 1,
                UID = __SampleDeliveryUID,
                NumberOfPackages = 1,
                VanUID = __SampleVanUID,
                Status = DeliveryStatus.Pending,
                CurrentDrop = 1
            };
        }

        public static List<Package> GeneratePackage ()
        {
            return new List<Package>
            {
                new Package
                {
                    Id = 1,
                    CustomerUID = __SampleCustomerUID,
                    DeliveryUID = __SampleDeliveryUID,
                    IsAssignedToDelivery = true,
                    Description = "Big box of rocks",
                    Size = 10,
                    UID = Guid.NewGuid(),
                    DropNumber = 1,
                    IsDelivered = false,
                    TrackingCode = "TESTPKG666"
                },
                new Package
                {
                    Id = 2,
                    CustomerUID = __SampleCustomerUID,
                    DeliveryUID = __SampleDeliveryUID,
                    IsAssignedToDelivery = true,
                    Description = "Big box of bolts",
                    Size = 10,
                    UID = __SamplePackageUID,
                    DropNumber = 2,
                    IsDelivered = false,
                    TrackingCode = "TESTPKG123"
                }
            };
        }

        public static List<DeliveryUser> GenerateUsers ()
        {
            PasswordHasher<DeliveryUser> _Hasher = new PasswordHasher<DeliveryUser>();

            List<DeliveryUser> _DeliveryUsers = new List<DeliveryUser>()
            {
                new DeliveryUser
                {
                    Id = __SampleCustomerUID.ToString(),
                    UserName = "customerlogon",
                    NormalizedUserName = "CUSTOMERLOGON",
                    Forename = "Annika",
                    Surname = "Hansen",
                    Address = "306 Sinfin Avenue, Derby, UK",
                    PostCode = "DE24 9QX",
                    HouseNumber = 306,
                    Email = "petelampy@gmail.com",
                    NormalizedEmail = "petelampy@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    UserType = UserType.Customer
                },
                new DeliveryUser
                {
                    Id = __SampleDriverUID.ToString(),
                    UserName = "driverlogon",
                    NormalizedUserName = "DRIVERLOGON",
                    Forename = "Din",
                    Surname = "Djarin",
                    Email = "petelampy@gmail.com",
                    NormalizedEmail = "petelampy@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    UserType = UserType.Driver,
                },
                new DeliveryUser
                {
                    Id = __SampleEmployeeUID.ToString(),
                    UserName = "employeelogon",
                    NormalizedUserName = "EMPLOYEELOGON",
                    Forename = "William",
                    Surname = "Riker",
                    Email = "petelampy@gmail.com",
                    NormalizedEmail = "petelampy@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    UserType = UserType.Employee
                }
            };

            foreach(DeliveryUser _User in _DeliveryUsers)
            {
                _User.PasswordHash = _Hasher.HashPassword(_User, "testaccount");
            }

            return _DeliveryUsers;
        }

        public static Van GenerateVan ()
        {
            return new Van
            {
                Id = 1,
                UID = __SampleVanUID,
                Capacity = 50,
                DriverUID = __SampleDriverUID,
                Registration = "WK04 DHC"
            };
        }
    }
}
