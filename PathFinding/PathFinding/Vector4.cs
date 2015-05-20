using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PathFinding
{
    class Vector4
    {
        int x, y, w, h;

        public Vector4(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public Rectangle getPosition()
        {
            return new Rectangle(this.x, this.y, this.w, this.h);
        }

        public void SetPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetScale(int w, int h)
        {
            this.w = w;
            this.h = h;
        }

        public int X()
        {
            return this.x;
        }
        public int Y()
        {
            return this.y;
        }
        public int W()
        {
            return this.w;
        }
        public int H()
        {
            return this.h;
        }
    }
}
