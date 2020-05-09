using AutoMapper;
using BLL.Models;
using ConsoleUI.ViewModels.Group;
using ConsoleUI.ViewModels.Lecturer;
using ConsoleUI.ViewModels.Student;

namespace ConsoleUI.Automapper
{
    public class AutomapperConsoleConfig : Profile
    {
        public AutomapperConsoleConfig()
        {
            CreateMap<Lecturer, LecturerViewModel>().ReverseMap();
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<Group, GroupViewModel>().ReverseMap();
        }
    }
}
