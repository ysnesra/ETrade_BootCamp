namespace ETrade_BootCamp.ViewModels
{
    public class EmployeeCreateViewModel
    {
        //public int EmployeeId { get; set; } yeni personel eklerken Id vermiycez otomatik Id atanacak
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Title { get; set; }
        public int Reports { get; set; }   //personelin amiri bilgiside girilsin
    }
}

