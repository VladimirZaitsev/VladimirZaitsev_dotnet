using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Interfaces
{
    public interface ISubController<T>
    {
        void PrintOperations();

        Task HandleInput();
    }
}
