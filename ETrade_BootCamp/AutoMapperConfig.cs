using AutoMapper;
using ETrade_BootCamp.Models;
using ETrade_BootCamp.ViewModels;

namespace ETrade_BootCamp
{
   
    //AutoMapper'dan gelen hazır class olan Profile'den miras alır
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()  //Constructor da belli kalıpları belirtiyoruz
        {
            //User clasını UserViewModel clasına çevirmeyi öğren  //ReverseMap() ile de tersini de öğren demiş oluyoruz
            CreateMap<Employee, EmployeeCreateViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeEditViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeListViewModel>().ReverseMap();
            CreateMap<Order, OrderListViewModel>().ReverseMap();
            CreateMap<Product, ProductListViewModel>().ReverseMap();
            CreateMap<Territory, TerritoryListViewModel>().ReverseMap();
        }
    }
    
}
