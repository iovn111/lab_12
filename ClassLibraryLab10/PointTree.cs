using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class PointTree
    {
        public Clock data;
        public PointTree left, right;

        public PointTree()
        {
            Clock clock = new Clock();
            clock.IRandomInit();
            data = clock;
            left = null;
            right = null;
        }

        public PointTree(Clock clock)
        {
            data = clock;
            left = null;
            right = null;
        }

        public override string ToString()
        {
            return data + " ";
        }
    }
}