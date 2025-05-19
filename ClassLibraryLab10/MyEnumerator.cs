using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class MyEnumerator<T>: IEnumerator<T> where T : IInit, new()
    {
        PointList<T> begin, current;

        public MyEnumerator()
        {
            begin = null;
            current = null;
        }
        
        public MyEnumerator(MyList<T> list)
        {
            begin = list.begin;
            current = list.begin;
        }

        public MyEnumerator(int length)
        {
            // Проверяем длину на допустимость
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Длина должна быть больше нуля");
            }

            // Создаем первый узел
            begin = new PointList<T>(new T());
            current = begin;

            // Заполняем список элементами
            for (int i = 1; i < length; i++)
            {
                PointList<T> point = new PointList<T>(new T()); // Создаем новый элемент
                point.data.IRandomInit();                       // Меняем элемент случайным образом
                current.next = point;                           // Присоединяем следующий узел
                current = point;                                // Переходим на следующий узел
            }
        }

        public T Current => current.data;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() { }

        public bool MoveNext()
        {
            if (current.next == null)
            {
                Reset();
                return false;
            }
            else
            {
                current = current.next;
                return true;
            }
        }

        public void Reset()
        {
            current = this.begin;
        }
    }
}