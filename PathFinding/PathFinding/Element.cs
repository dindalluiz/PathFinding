using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class Element
    {
        Element prev, next;
        int value;

        public Element(int v, Element p = null, Element n = null)
        {
            value = v;
            next = n;
            prev = p;
        }

        public Element Previous
        {
            get { return prev; }
            set { prev = value; }
        }

        public Element Next
        {
            get { return next; }
            set { next = value; }
        }

        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public override string ToString()
        {
            return "" + value;
        }
    }
}
