using AutoMapper;
using BLL.Models;
using ConsoleUI.ViewModels.Lecturer;
using ConsoleUI.ViewModels.Student;

namespace ConsoleUI.Automapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Person, LecturerViewModel>().ReverseMap();
            CreateMap<Person, StudentViewModel>().ReverseMap();
        }
    }
}
