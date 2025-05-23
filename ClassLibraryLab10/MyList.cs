using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class MyList<T>: IList<T> where T : IInit, new()
    {
        public PointList<T>? begin; // начало списка

        public int Count
        {
            get
            {
                int count = 0;
                if (begin == null) return 0;
                PointList<T> current = begin;
                while (current != null)
                {
                    count++;
                    current = current.next;
                }
                return count;
            }
        }

        bool ICollection<T>.IsReadOnly => false;

        T IList<T>.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Clear()
        {
            begin = null;
        }

        public void Add(T item)
        {
            PointList<T> newPoint = new PointList<T>(item);
            if (begin == null)
            {
                begin = newPoint;
            }
            else
            {
                AddToEnd(newPoint);
            }
        }

        public void Add(int number, T item)
        {
            PointList<T> newPoint = new PointList<T>(item);
            if (number > Count + 1)
            {
                throw new Exception("Номер больше, чем количнство элементов в списке");
            }
            if (number == 1)
            {
                AddToBegin(newPoint);
                return;
            }
            int count = 1;
            PointList<T> current = begin;
            while (current.next != null)
            {
                if (count+1 == number)
                {
                    break;
                }
                count++;
                current = current.next;
            }
            if (current.next ==  null)
            {
                AddToEnd(newPoint);
            }
            else
            {
                newPoint.next = current.next;
                current.next = newPoint;
            }
        }

        void AddToEnd(PointList<T> item)
        {
            PointList<T> ptr = begin;
            while (ptr.next != null)
            {
                ptr = ptr.next;
            }
            ptr.next = item;
        }

        void AddToBegin(PointList<T> item)
        {
            item.next = begin;
            begin = item;
        }

        public bool Contains(T item)
        {
            PointList<T> current = begin;
            while (current != null && !current.data.Equals(item))
            {
                current = current.next;
            }
            return current != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new Exception("Массив не может быть null");
            }
            if (arrayIndex < 0)
            {
                throw new Exception("Индекс не может быть меньше 0");
            }
            if (array.Length - arrayIndex < Count)
            {
                throw new Exception("Список не помещается в массив");
            }
            PointList<T> current = begin;
            int i = arrayIndex;
            while (current != null && i < array.Length)
            {
                array[i++] = current.data;
                current = current.next;
            }
        }

        public bool Remove(T item)
        {
            if (begin.data.Equals(item))
            {
                begin = begin.next;
                return true;
            }
            else
            {
                PointList<T> current = begin;
                while (current.next !=  null && !current.data.Equals(item))
                {
                    current = current.next;
                }
                if (current.next == null)
                {
                    return false;
                }
                else
                {
                    current.next = current.next.next;
                    return true;
                }
            }
        }

        public void PrintList()
        {
            PointList<T> current = begin;
            int count = 1;
            while (current != null)
            {
                Console.WriteLine($"{count}: {current.data}");
                current = current.next;
                count++;
            }
        }

        int IList<T>.IndexOf(T item)
        {
            int index = 0;
            PointList<T> current = begin;
            while (current != null)
            {
                if (current.data.Equals(item)) // Проверяем равенство элементов
                {
                    return index;
                }
                current = current.next;
                index++;
            }
            return -1; // Элемент не найден
        }

        void IList<T>.Insert(int index, T item)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index)); // Индекс вне допустимого диапазона
            }

            PointList<T> newPoint = new PointList<T>(item);

            if (index == 0)
            {
                newPoint.next = begin;
                begin = newPoint;
            }
            else
            {
                PointList<T> prev = begin;
                for (int i = 0; i < index - 1; i++) // Проходим до предыдущего узла перед указанным индексом
                {
                    prev = prev.next;
                }
                newPoint.next = prev.next;
                prev.next = newPoint;
            }
        }

        void IList<T>.RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index)); // Индекс вне допустимого диапазона
            }

            if (index == 0)
            {
                begin = begin.next; // Удаление первого элемента
            }
            else
            {
                PointList<T> prev = begin;
                for (int i = 0; i < index - 1; i++) // Находим предыдущий узел перед указанным индексом
                {
                    prev = prev.next;
                }
                prev.next = prev.next.next; // Пропускаем удаляемый узел
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new MyCollection<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}