using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PathFinding
{
    class Cube
    {
        Vector4 pos;
        Pen colorPen;
        Brush colorBrush;

        string type;

        public Cube(int x, int y, int w, int h, Pen color, string type)
        {
            this.pos = new Vector4(x, y, w, h);
            this.colorPen = color;
            this.type = type;
        }

        public Cube(int x, int y, int w, int h, Brush color, string type)
        {
            this.pos = new Vector4(x, y, w, h);
            this.colorBrush = color;
            this.type = type;
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public void SetColor(Pen color)
        {
             colorPen = null;
             colorBrush = null;
             colorPen = color;
        }

        public void SetColor(Brush color)
        {
            colorPen = null;
            colorBrush = null;
            colorBrush = color;
        }

        public Brush GetColor()
        {
            return colorBrush;
        }

        public Pen GetColorPen()
        {
            return colorPen;
        }

        public Vector4 Pos
        {
            get
            {
                return pos;
            }
            set
            {
                this.pos = value;
            }

        }

        public void Draw(Graphics g)
        {

            if (colorPen != null)
            {
                g.DrawRectangle(colorPen, pos.getPosition());
            }
            else
            {
                g.FillRectangle(colorBrush, pos.getPosition());
            }
        }
    }
}
