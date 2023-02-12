namespace ETrade_BootCamp.ViewModels
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? QuantityPerUnit { get; set; }
    }
}
