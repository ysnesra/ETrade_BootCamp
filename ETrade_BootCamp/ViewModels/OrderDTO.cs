namespace ETrade_BootCamp.ViewModels
{
    public class OrderDTO
    {
        public int EmployeeId { get; set; }
        public int OrderId { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipCountry { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal TotalPrice { get; set; } 
    }
}
