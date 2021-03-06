﻿using System;
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
        int h, g = 0;

        bool w = true;

        bool n = false;

        string type;

        public Cube() { this.n = true; }

        public Cube(int x, int y, int w, int h, Pen color, string type, int ha, int ga)
        {
            this.pos = new Vector4(x, y, w, h);
            this.h = ha;
            this.g = ga;
            this.colorPen = color;
            this.type = type;
            this.n = false;
        }

        public Cube(int x, int y, int w, int h, Brush color, string type, int ha, int ga)
        {
            this.pos = new Vector4(x, y, w, h);
            this.h = ha;
            this.g = ga;
            this.colorBrush = color;
            this.type = type;
            this.n = false;
        }

        public bool none
        {
            get
            {
                return n;
            }
        }

        public bool walkable
        {
            get
            {
                return w;
            }
            set
            {
                w = value;
            }
        }

        public int H
        {
            get
            {
                return h;
            }
            set
            {
                if (h == 0)
                    h = value;
            }
        }

        public int G
        {
            get
            {
                return g;
            }
            set
            {
                if (g == 0)
                    g = value;
            }
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
