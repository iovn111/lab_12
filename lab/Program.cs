using System;
using LibraryLab10;

namespace lab
{
    public class Program
    {
        // Функции для задания 3 
        static PointTree IdealTree(int size, PointTree point)
        {
            PointTree r;
            int nl, nr;
            if (size == 0)
            {
                point = null;
                return point;
            }
            nl = size/2;
            nr = size - nl - 1;
            Clock dataPoint = GetInfo();
            r = new PointTree(dataPoint);
            r.left = IdealTree(nl, r.left);
            r.right = IdealTree(nr, r.right);
            return r;
        }

        static Clock GetInfo()
        {
            Clock clock = new Clock();
            clock.IRandomInit();
            return clock;
        }

        public static int CountForTree; // Для подсчета элементов в CountBYKey

        static void CountByKey(PointTree tree, Clock key)
        {
            if (tree != null)
            {
                CountByKey(tree.left, key);
                if (key == tree.data)
                {
                    CountForTree++;
                }
                CountByKey(tree.right, key);
            }
        }

        static void FromIdealToSearch(PointTree idealTree, PointTree searchTree)
        {
            if (idealTree != null)
            {
                FromIdealToSearch(idealTree.left, searchTree);

                Add(searchTree, idealTree.data);

                FromIdealToSearch(idealTree.right, searchTree);
            }
        }

        static void ShowTree(PointTree point, int indent)
        {
            if (point != null)
            {
                ShowTree(point.left, indent + 3);
                for (int i = 0; i < indent; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(point.data);
                ShowTree(point.right, indent + 3);
            }
        }

        static PointTree MakePointTree(Clock clock)
        {
            PointTree pointTree = new PointTree(clock);
            return pointTree;
        }

        static PointTree Add(PointTree root, Clock clock)
        {
            PointTree point = root;
            PointTree pointRoot = null;
            bool ok = false;
            while (point != null && !ok)
            {
                pointRoot = point;
                if (clock == point.data)
                {
                    ok = true;
                }
                else
                {
                    if (clock < point.data) // Часы сравниваются по году выпуска
                    {
                        point = point.left;
                    }
                    else
                    {
                        point = point.right;
                    }
                }
            }
            if (ok)
            {
                return point;
            }
            PointTree newPoint = MakePointTree(clock);
            if (clock < pointRoot.data)
            {
                pointRoot.left = newPoint;
            }
            else
            {
                pointRoot.right = newPoint;
            }
            return newPoint;
        }

        static PointTree CopyFirst(PointTree point)
        {
            if (point != null)
            {
                if (point.left == null)
                {
                    return point;
                }
                if (point.left.left == null)
                {
                    return point.left;
                }
                CopyFirst(point.left);
            }
            return point.left;
        }

        static void RemoveByKey(PointTree point, Clock key)
        {
            if (point != null)
            {
                RemoveByKey(point.left, key);
                if (point.data == key)
                {
                    point.data = null;
                }
                RemoveByKey(point.right, key);
            }
        }

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

            // --- Задание 3 ---
            PointTree idealTree = null; // корень дерева
            int size = 5; // задаем размер дерева
            idealTree = IdealTree(size, idealTree); // Заполняем дерево случайными часами
            ShowTree(idealTree, 3); // Печатаем дерево

            Clock randomClock = new Clock(); // создаем случайные часы для  
            randomClock.IRandomInit();       // следующего действия
            
            CountByKey(idealTree, randomClock); // Подсчитыаем количество элементов по ключу
            Console.WriteLine(CountForTree); //   и выводим его

            PointTree searchTree = CopyFirst(idealTree); // Выделяем память под начало дерева поиска
            FromIdealToSearch(idealTree, searchTree); // Копируем элементы идеального дерева и заполняем ими дерево поиска
            ShowTree(searchTree, 10);

            RemoveByKey(searchTree, randomClock); // Удаляем часики по ключу

            idealTree = null; searchTree = null; // Чистим памят
        }
    }
}
