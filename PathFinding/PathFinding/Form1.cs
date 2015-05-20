using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFinding
{
    public partial class Form1 : Form
    {
        Map map = new Map();

        List<int> adjAux;
        List<List<int>> adj;

        Vector4 auxObj;
        Vector4 auxPlayer;

        int cost;
        Vector4 moveObj;

        public Form1()
        {
            adj = new List<List<int>>();
            for (int i = 0; i < 3; i++)
            {
                adjAux = new List<int>();
                for (int j = 0; j < 3; j++)
                {
                    adjAux.Add(0);
                }
                adj.Add(adjAux);
            }

            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Timer timer = new Timer();
            timer.Interval = 1;
            timer.Start();
            timer.Tick += new EventHandler(Update);

            Paint += new PaintEventHandler(Draw);

            map.AddElement(3, 3, "Player", Brushes.Red);
            map.AddElement(9, 9, "Objetivo", Brushes.Blue);
            map.AddElement(4, 4, "Wall", Brushes.Green);
            map.AddElement(3, 4, "Wall", Brushes.Green);

            CalcAdj();
            MovePlayer();
        }

        int Distance()
        {
            foreach (List<Cube> c in map.GetMap())
            {
                foreach (Cube a in c)
                {
                    if(a.Type == "Objetivo")
                    {
                        auxObj = a.Pos;
                    }
                }
            }

            foreach (List<Cube> c in map.GetMap())
            {
                foreach (Cube a in c)
                {
                    if (a.Type == "Player")
                    {
                        auxPlayer = a.Pos;
                    }
                }
            }

            int totalCount;

            totalCount = (((auxObj.X() / 20) - (auxPlayer.X() / 20)) * 10) + (((auxObj.Y() / 20) - (auxPlayer.Y() / 20)) * 10);
            return totalCount;
        }

        void CalcAdj()
        {
            foreach (List<Cube> c in map.GetMap())
            {
                foreach (Cube a in c)
                {
                    if(a.Type == "Player")
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                adj[i][j] = map.GetElement(i, j, (a.Pos.X()-20)/20, (a.Pos.Y()-20)/20);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine(adj[i][j]);
                }
            }
        }

        void MovePlayer()
        {
            foreach (List<Cube> c in map.GetMap())
            {
                foreach (Cube a in c)
                {
                    if (a.Type == "Player")
                    {
                        auxPlayer = a.Pos;
                    }
                }
            }

            this.cost = 1000;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (adj[i][j] + Distance() < cost)
                    {
                        cost = adj[i][j] + Distance();
                        moveObj = new Vector4(i, j, 0, 0);
                    }
                }
            }
            moveObj = new Vector4(1, 2,0,0);
            map.UpdateElement(auxPlayer.X() / 20, auxPlayer.Y() / 20, moveObj.X(), moveObj.Y(), "Player", Brushes.Red);
        }

        void Update(object sender, EventArgs e)
        {
            Invalidate();
        }

        void Draw(Object sender, PaintEventArgs PaintNow)
        {
            map.Draw(PaintNow.Graphics);
        }
    }
}
