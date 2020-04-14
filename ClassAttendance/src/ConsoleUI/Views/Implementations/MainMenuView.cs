using ConsoleUI.Views.Interfaces;
using System;

namespace ConsoleUI.Views.Implementations
{
    public class MainMenuView : IMainMenuView
    {
        public void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("////////////////////////");
            Console.WriteLine("1. Student service");
            Console.WriteLine("2. Lecturer service");
            Console.WriteLine("3. Lectures service");
            Console.WriteLine("0. Exit");
        }
    }
}
