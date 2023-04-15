namespace DeliverySoftware.Business.Fleet
{
    public class Van
    {
        public int Capacity { get; set; }
        public Guid? DriverUID { get; set; }
        public int Id { get; set; }
        public string Registration { get; set; }
        public Guid UID { get; set; }
    }
}
