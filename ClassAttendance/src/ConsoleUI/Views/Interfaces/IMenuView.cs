using System;
using System.Collections.Generic;

namespace ConsoleUI.Views.Interfaces
{
    public interface IMenuView<T, V>
    {
        void PrintMenu();

        void PrintAll(IEnumerable<V> items);

        void Print(V item);

        T GetFromInput();

        T Update(T item);

        int GetIdFromInput()
        {
            var input = Console.ReadLine();
            var id = int.Parse(input);

            return id;
        }
    }
}
