﻿namespace DeliverySoftware.Business.Delivery
{
    public class Delivery
    {
        public int Id { get; set; }
        public int NumberOfPackages { get; set; }
        public Guid UID { get; set; }
        public Guid VanUID { get; set; }
    }
}
