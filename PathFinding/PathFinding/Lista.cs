using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    class List
    {
        Element first, last;

        public List()
        {
            first = null;
            last = null;
        }

        public List(params int[] args)
        {
            foreach (int arg in args)
            {
                Add(arg);
            }
        }

        public void Add(int value)
        {
            if (first == null)
            {
                first = new Element(value);
            }
            else if (last == null)
            {
                last = new Element(value, first);
                first.Next = last;
            }
            else
            {
                Element current = new Element(value, last);
                last.Next = current;
                last = current;
            }
        }
        public Element Get(int index)
        {
            Element result = first;
            if (index >= 0 && index < Count)
            {
                for (int i = 0; i < index; i++)
                {
                    result = result.Next;
                }

                return result;
            }

            return null;
        }

        public int Count
        {
            get
            {
                int count = 0;
                Element current = first;
                while (current != null)
                {
                    current = current.Next;
                    count++;
                }
                return count;
            }
        }

        public void SetFirst(Element f) { first = f; }

        public Element[] ToArray()
        {
            Element[] result = new Element[Count];
            for (int i = 0; i < result.Length; i++)
            {
                if (i == 0) result[i] = first;
                else result[i] = result[i - 1].Next;
            }
            return result;
        }
    }
}
