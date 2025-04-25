using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class MyList<T>
    {
        PointList<T>? begin; // начало списка

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
    }
}