namespace ETrade_BootCamp.ViewModels
{
    public class OrderListViewModel
    {
        //public int EmployeeId { get; set; }    sayfada kullanmıycağımız için gerek yok
        public int OrderNo { get; set; }       
        public string OrderDate { get; set; }  //tipini DateTime yerine string yapıp.Lınq yazdığmız yerde kontrolünü yaparız
        public string? OrderCountry { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
