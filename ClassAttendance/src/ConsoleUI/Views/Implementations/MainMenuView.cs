using ConsoleUI.Views.Interfaces;
using System;

namespace ConsoleUI.Views.Implementations
{
    public class MainMenuView : IPrintMenuView
    {
        public void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("////////////////////////");
            Console.WriteLine("1. Student service");
            Console.WriteLine("2. Lecturer service");
            Console.WriteLine("3. Lectures service");
            Console.WriteLine("4. Group service");
            Console.WriteLine("5. Subject service");
            Console.WriteLine("0. Exit");
            Console.WriteLine("////////////////////////");
        }
    }
}
