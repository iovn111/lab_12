using System;
using LibraryLab10;

namespace lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ---- Задание 1 ---
            
            MyList<Clock> list = new MyList<Clock>(); // Создаем список
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Clock()); // Заполняем список
            }

            list.PrintList(); // Печатаем список
            Console.WriteLine("");

            AnalogClock analogClock = new AnalogClock();
            analogClock.IRandomInit();
            list.Add(2, analogClock); // Добавляем элемент с заданным номером
            list.PrintList();
            Console.WriteLine("");

            list.Remove(analogClock); // Удаляем эемент с заданным значением
            list.PrintList();
            Console.WriteLine("");

            Clock[] arr = new Clock[list.Count]; // Выделяем память под копию списка
            list.CopyTo(arr, 0); // Создаем копию 
            list.Add(1, analogClock); // Создаем разницу между оригиналом и копией, чтобы увидеть разницу
            list.PrintList();
            Console.WriteLine("");
            for (int i = 0; i < arr.Length;i++)
            {
                Console.WriteLine(arr[i]);
            }

            list.Clear(); // Удаляем список из памяти
            list.PrintList();

            // --- Задание 2 ---
            MyOpenHS<Clock> myOpenHS = new MyOpenHS<Clock>(); // создали хеш-таблицу
            for (int i = 0; i < 9; i++) // заполняем таблицу
            {
                Clock localClock = new Clock();
                localClock.IRandomInit();
                myOpenHS.Add(localClock);
            }

            Clock clock = new Clock(); // создаем элемент для поиска в таблице (он не обязательно должен быть в таблице)
            clock.IRandomInit();
            int hech = Math.Abs(clock.GetHashCode() % myOpenHS.Count); // вычисляем хеш-код 
            Console.WriteLine(myOpenHS.FindByKey(hech)); // ищем
            myOpenHS.RemoveByKey(hech); // удаляем
            Console.WriteLine(myOpenHS.FindByKey(hech)); // снова ищем (тут в любом случе должны вывестись часы NoBrend 2000)

            Console.WriteLine("");
            Console.WriteLine(myOpenHS.Count); // длина равна 9
            myOpenHS.Add(clock); // просто добавляется элемент
            Console.WriteLine(myOpenHS.Count); // длина 10
            clock.RandomInit(); // меняем элемент, иначе если добавим тот же то длина не измениться
            myOpenHS.Add(clock); // а тут не просто добавляется элемент, перед этим срабатывает метод Resize, который увеличивает
                                 // емкость таблицы в 2 раза, чо позволяет нам записать 11-ый элемент
            Console.WriteLine(myOpenHS.Count); // длина 11
        }
    }
}
