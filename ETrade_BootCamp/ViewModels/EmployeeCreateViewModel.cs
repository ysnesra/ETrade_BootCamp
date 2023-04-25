namespace ETrade_BootCamp.ViewModels
{
    public class EmployeeCreateViewModel
    {
        //public int EmployeeId { get; set; } yeni personel eklerken Id vermiycez otomatik Id atanacak
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Title { get; set; }
        public int Reports { get; set; }   //personelin amiri bilgiside girilsin
        public string? Done { get; set; }

        public List<EmployeeTerritoryViewModel> Territories { get; set; }
    }

    //Bölgeleri seçeceği için ayrı bir viewmodel oluştururuz
    public class EmployeeTerritoryViewModel
    {
        public string TerritoryID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }  //Checkbox ın seçilip seçilmediğni tutmak için bool bir property oluşturuduk
    }
}

