using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class PointHS<T>
    {
        public T? data { get; set; }
        public bool isDeleted { get; set; }

        public PointHS()
        {
            data = default(T);
            isDeleted = false;
        }
        public PointHS(T info)
        {
            data = info;
            isDeleted = false;
        }
        public override string ToString()
        {
            if (data != null)
                return data.ToString();
            else
                return null;
            
        }
    }
}