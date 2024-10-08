﻿namespace DeliverySoftware.Business.Delivery
{
    public class Delivery
    {
        public int CurrentDrop { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public int NumberOfPackages { get; set; }
        public DeliveryStatus Status { get; set; }
        public Guid UID { get; set; }
        public Guid VanUID { get; set; }
    }
}
