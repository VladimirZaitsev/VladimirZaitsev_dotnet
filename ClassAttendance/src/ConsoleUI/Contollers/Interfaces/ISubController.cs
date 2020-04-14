using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Interfaces
{
    public interface ISubController
    {
        void PrintOperations();

        Task HandleInput();
    }
}
