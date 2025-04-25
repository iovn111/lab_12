using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class PointList<T>
    {
        public T? data { get; set; }
        public PointList<T>? next { get; set; } //адрес следующего элемента
        public PointList<T>? pred { get; set; }//адрес предыдущего элемента

        public PointList()
        {
            data = default(T);
            next = null;
            pred = null;
        }
        public PointList(T info)
        {
            data = info;
            next = null;
            pred = null;
        }
        public override string ToString()
        {
            return data?.ToString() ?? "null";
        }

        public static PointList<T> MakePoint(T info)
        {
            PointList<T> p = new PointList<T>(info);
            return p;
        }
    }
}