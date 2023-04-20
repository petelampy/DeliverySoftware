using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using DeliverySoftware.Database;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.Directions.Request;
using static GoogleApi.GoogleMaps;

namespace DeliverySoftware.Business.Delivery
{
    public class PackageController : IPackageController
    {
        private const string MAPS_API_KEY = "AIzaSyDzLyXmbIGVxhJfVIurJYymXI-gsJsP5Zw";

        private readonly DeliveryDBContext __DbContext;
        private readonly IDBContextManager __DbContextManager;
        private readonly IDeliveryController __DeliveryController;
        private readonly IUserController __UserController;
        private readonly IVanController __VanController;

        public PackageController () :
            this(new DBContextManager(), new UserController(), new VanController(), new DeliveryController())
        { }

        public PackageController (IDBContextManager dbContextManager, IUserController userController, IVanController vanController,
            IDeliveryController deliveryController)
        {
            __DbContextManager = dbContextManager;
            __DbContext = __DbContextManager.CreateNewDatabaseContext();
            __UserController = userController;
            __VanController = vanController;
            __DeliveryController = deliveryController;
        }

        public void Create (Package newPackage)
        {
            newPackage.UID = Guid.NewGuid();

            if (newPackage.DeliveryUID != Guid.Empty)
            {
                newPackage.IsAssignedToDelivery = true;
                newPackage.DropNumber = GetPackageCountByDelivery(newPackage.DeliveryUID) + 1;
            }

            __DbContext.Packages.Add(newPackage);
            __DbContext.SaveChanges();

            if (newPackage.DeliveryUID != Guid.Empty)
            {
                UpdateDeliveryRunDropOrder(newPackage.DeliveryUID);
            }
        }

        public void Delete (Guid uid)
        {
            Package _Package = Get(uid);

            __DbContext.Remove(_Package);
            __DbContext.SaveChanges();

            if (_Package.DeliveryUID != Guid.Empty)
            {
                UpdateDeliveryRunDropOrder(_Package.DeliveryUID);
            }
        }

        public bool DoesTrackingCodeExist (string trackingCode)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.TrackingCode == trackingCode)
               .Count() > 0;
        }

        public Package Get (Guid uid)
        {
            return __DbContext.Packages
                 .ToList()
                 .Where(package => package.UID == uid)
                 .SingleOrDefault(new Package());
        }

        public int GetActivePackagesByCustomer (Guid customer_uid)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.CustomerUID == customer_uid && package.IsDelivered == false)
               .Count();
        }

        public List<Package> GetAll ()
        {
            return __DbContext.Packages.ToList();
        }

        public List<Package> GetAllUndelivered ()
        {
            return __DbContext
                .Packages
                .AsEnumerable()
                .Where(package => !package.IsDelivered)
                .ToList();
        }

        public Package GetByDeliveryAndDropNumber (Guid delivery_uid, int currentDropNumber)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.DeliveryUID == delivery_uid && package.DropNumber == currentDropNumber)
               .SingleOrDefault(new Package());
        }

        public Package GetByTrackingCode (string trackingCode)
        {
            return __DbContext
                .Packages
                .AsEnumerable()
                .Where(package => package.TrackingCode == trackingCode)
                .SingleOrDefault(new Package());
        }

        public int GetPackageCountByDelivery (Guid delivery_uid)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.DeliveryUID == delivery_uid)
               .Count();
        }

        public List<Package> GetPackagesByDelivery (Guid delivery_uid)
        {
            return __DbContext
               .Packages
               .AsEnumerable()
               .Where(package => package.DeliveryUID == delivery_uid)
               .ToList();
        }

        public void Update (Package updatedPackage)
        {
            Package _CurrentPackage = Get(updatedPackage.UID);

            _CurrentPackage.Description = updatedPackage.Description;
            _CurrentPackage.Size = updatedPackage.Size;
            _CurrentPackage.CustomerUID = updatedPackage.CustomerUID;
            _CurrentPackage.IsDelivered = updatedPackage.IsDelivered;
            _CurrentPackage.TrackingCode = updatedPackage.TrackingCode;

            if (_CurrentPackage.DeliveryUID == Guid.Empty && updatedPackage.DeliveryUID != Guid.Empty)
            {
                _CurrentPackage.IsAssignedToDelivery = true;
                _CurrentPackage.DropNumber = GetPackageCountByDelivery(_CurrentPackage.DeliveryUID) + 1;
                _CurrentPackage.DeliveryUID = updatedPackage.DeliveryUID;
            }

            __DbContext.SaveChanges();
        }

        public void UpdateDeliveryRunDropOrder (Guid delivery_uid)
        {
            List<Package> _Packages = GetPackagesByDelivery(delivery_uid);

            List<Tuple<Guid, string, int>> _PackageDropData = new List<Tuple<Guid, string, int>>();

            foreach (Package _Package in _Packages)
            {
                DeliveryUser _Customer = __UserController.Get(_Package.CustomerUID);
                string _CustomerPostcode = _Customer.PostCode;
                int _CustomerHouseNumber = _Customer.HouseNumber.Value;

                Tuple<Guid, string, int> _PackageData = Tuple.Create(_Package.UID, _CustomerPostcode, _CustomerHouseNumber);

                _PackageDropData.Add(_PackageData);
            }

            DirectionsApi _DirectionsClient = new DirectionsApi();

            Guid _VanUID = __DeliveryController.Get(delivery_uid).VanUID;

            string _VanDepot = __VanController.Get(_VanUID).DepotPostCode;

            string _CurrentPostCode = _VanDepot;
            int _CurrentDropNumber = 1;

            while (_PackageDropData.Count > 0)
            {
                Guid _ClosestPackage = Guid.Empty;
                int _ClosestDuration = 0;
                bool _IsFirstPackage = true;

                foreach (Tuple<Guid, string, int> _Package in _PackageDropData)
                {

                    Address _OriginAddress = new Address(_CurrentPostCode);
                    LocationEx _Origin = new LocationEx(_OriginAddress);

                    Address _DestinationAddress = new Address(_Package.Item3 + ", " + _Package.Item2);
                    LocationEx _Destination = new LocationEx(_DestinationAddress);

                    DirectionsRequest _Request = new DirectionsRequest
                    {
                        Origin = _Origin,
                        Destination = _Destination,
                        TravelMode = TravelMode.Driving,
                        Key = MAPS_API_KEY
                    };
                    var _Response = _DirectionsClient.Query(_Request);
                    if (_Response.Status == Status.Ok)
                    {
                        var _Duration = _Response.Routes.First().Legs.First().Duration.Value;
                        if (_IsFirstPackage || _Duration < _ClosestDuration)
                        {
                            _ClosestPackage = _Package.Item1;
                            _ClosestDuration = _Duration;
                            _IsFirstPackage = false;
                        }
                    }
                }

                Tuple<Guid, string, int> _ChosenPackage = _PackageDropData
                    .Where(package => package.Item1 == _ClosestPackage)
                    .Single();

                _Packages
                    .Where(package => package.UID == _ChosenPackage.Item1)
                    .ToList()
                    .ForEach(package => package.DropNumber = _CurrentDropNumber);

                _CurrentDropNumber += 1;
                _CurrentPostCode = _ChosenPackage.Item2;
                _PackageDropData.Remove(_ChosenPackage);
            }

            __DbContext.UpdateRange(_Packages);
            __DbContext.SaveChanges();
        }
    }
}
