namespace ETrade_BootCamp.ViewModels
{
    public class EmployeeEditViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Title { get; set; }
        public int Reports { get; set; }
        public string? Done { get; set; }
    }
}

