using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryLab10
{
    public class MyOpenHS<T>
    {
        public PointHS<T>[] set; // начало списка
        int count = 0; // количество элементов
        double LoadFactor; // коэффицент заполненности

        public int Count => count;

        public bool isReadOnly => false;

        public MyOpenHS(int capacity = 10, double loadFactor = 0.72)
        {
            if (capacity < 0)
            {
                throw new Exception("Ёмкость таблицы не может быть меньше 0");
            }
            if (loadFactor < 0 || loadFactor > 1)
            {
                throw new Exception("Коэффицент заполненности должен быть от 0 до 1");
            }
            set = new PointHS<T>[capacity]; // создали массив элементов
            LoadFactor = loadFactor;
        }

        public void Clear()
        {
            for (int i = 0; i < set.Length; i++)
            {
                set[i] = null;
            }
        }

        public void Resize()
        {
            PointHS<T>[] newSet = new PointHS<T>[set.Length * 2]; // увеличиваем размер в 2 раза
            for (int i = 0; i < set.Length; i++)
            {
                PointHS<T> item = set[i];
                if (item == null || item.isDeleted) // пустой или отмечен как удаленный => не удаляем
                {
                    continue;
                }
                int index = Math.Abs(item.GetHashCode()) % newSet.Length;
                if (newSet[index] == null)
                {
                    newSet[index] = item;
                }
                else
                {
                    for (int j = 0; j < newSet.Length; j++)
                    {
                        int newIndex = (index + j) % newSet.Length; // новый индекс
                        if (newSet[newIndex] == null)
                        {
                            newSet[newIndex] = item;
                            break;
                        }
                    }
                }
            }
            set = newSet;
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new Exception("Элемент пустой");
            }
            if (count >= LoadFactor * set.Length)
            {
                Resize();
            }
            int index = Math.Abs(item.GetHashCode()) % set.Length;
            if (set[index] == null || set[index].isDeleted)
            {
                set[index] = new PointHS<T>(item);
                count++;
            }
            else
            {
                for (int i = 0; i < set.Length; i++)
                {
                    int newIndex = (index + i) % set.Length; // новый индекс
                    if (set[newIndex] != null && set[newIndex].data.Equals(item))
                    {
                        return;
                    }
                    if (set[newIndex] == null || set[newIndex].isDeleted)
                    {
                        set[newIndex] = new PointHS<T>(item);
                        count++;
                        return;
                    }
                }
            }
        }

        public bool Contains(T item)
        {
            if (item == null) return false;
            int index = Math.Abs(item.GetHashCode()) % set.Length;
            if (set[index] != null)
            {
                if (!set[index].isDeleted && set[index].data.Equals(item))
                {
                    return true;
                }
                else
                {
                    for (int i = 0; i < set.Length;i++)
                    {
                        index = (index + i) % set.Length;
                        if (set[index] == null)
                        {
                            return false;
                        }
                        else
                        {
                            if (!set[index].isDeleted && set[index].data.Equals(item))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool Remove(T item)
        {
            if (item == null) return false;
            int index = Math.Abs(item.GetHashCode()) % set.Length;
            if (set[index] != null)
            {
                if (!set[index].isDeleted && set[index].data.Equals(item))
                {
                    count--;
                    set[index].isDeleted = true;
                    return true;
                }
                else
                {
                    for (int i = 0;i < set.Length;i++)
                    {
                        index = (index + i) % set.Length;
                        if (set[index] == null)
                        {
                            return false;
                        }
                        else
                        {
                            if (!set[index].isDeleted && set[index].data.Equals(item))
                            {
                                count--;
                                set[index].isDeleted = true;
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public void Print()
        {
            for (int i = 0; i < set.Length; i++)
            {
                if (set[i] == null)
                {
                    Console.WriteLine($"{i}: empty");
                }
                else
                {
                    if (!set[i].isDeleted)
                    {
                        Console.WriteLine($"{i}: {set[i].data}");
                    }
                    else
                    {
                        Console.WriteLine($"{i}: empty");
                    }
                }
            }
        }

        public bool RemoveByKey(int key)
        {
            int index = Math.Abs(key) % set.Length; // используем переданный ключ для расчёта индекса

            if (set[index] != null)
            {
                if (!set[index].isDeleted && index == key) // теперь сравниваем ключи
                {
                    count--;
                    set[index].isDeleted = true;
                    return true;
                }
                else
                {
                    for (int i = 0; i < set.Length; i++) // пробуем искать дальше в цепочке
                    {
                        index = (index + i) % set.Length;

                        if (set[index] == null)
                        {
                            return false;
                        }
                        else
                        {
                            if (!set[index].isDeleted && index == key) // снова сравниваем ключи
                            {
                                count--;
                                set[index].isDeleted = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public T FindByKey(int key)
        {
            int index = Math.Abs(key) % set.Length; // вычисляем начальный индекс на основе ключа

            if (set[index] != null)
            {
                if (!set[index].isDeleted && index == key) // нашли элемент прямо по индексу
                {
                    return set[index].data;
                }
                else
                {
                    for (int i = 0; i < set.Length; i++) // проходим по возможным позициям
                    {
                        index = (index + i) % set.Length; // рассчитываем следующий индекс

                        if (set[index] == null)
                        {
                            return default(T); // элемента с таким ключом нет
                        }
                        else
                        {
                            if (!set[index].isDeleted && index == key) // нашли нужный элемент
                            {
                                return set[index].data;
                            }
                        }
                    }
                }
            }
            return default(T); // элемент не найден
        }
    }
}