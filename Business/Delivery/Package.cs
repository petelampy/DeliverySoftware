namespace DeliverySoftware.Business.Delivery
{
    public class Package
    {
        public Guid CustomerUID { get; set; }
        public Guid DeliveryUID { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public bool IsAssignedToDelivery { get; set; }
        public int Size { get; set; }
        public Guid UID { get; set; }
    }
}
