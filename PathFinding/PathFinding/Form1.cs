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

        List<Cube> cubeAdjAux;
        List<List<Cube>> cubeAdj;

        List<Cube> path;

        int cost = 1000;

        public Form1()
        {

            cubeAdj = new List<List<Cube>>();
            for (int i = 0; i < 3; i++)
            {
                cubeAdjAux = new List<Cube>();
                for (int j = 0; j < 3; j++)
                {
                    cubeAdjAux.Add(new Cube());
                }
                cubeAdj.Add(cubeAdjAux);
            }

            this.path = new List<Cube>();

            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Timer timer = new Timer();
            timer.Interval = 1;
            timer.Start();
            timer.Tick += new EventHandler(Update);

            Paint += new PaintEventHandler(Draw);

            map.AddElement(2, 2, "Player", Brushes.Red);
            map.AddElement(2, 9, "Objetivo", Brushes.Blue);
            map.AddElement(4, 4, "Wall", Brushes.Green);
            map.AddElement(3, 4, "Wall", Brushes.Green);
            map.AddElement(5, 4, "Wall", Brushes.Green);
            map.AddElement(6, 4, "Wall", Brushes.Green);
            map.AddElement(7, 4, "Wall", Brushes.Green);

            AddPath();
        }

        void AddPath()
        {
            if(path.Count == 0)
            {
                path.Add(map.GetElement("Player"));
                CalcAdj();
                Distance();
                return;
            }
            this.cost = 10000;
            Cube aux = new Cube();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine(i+","+j+":"+Math.Abs(cubeAdj[i][j].H + cubeAdj[i][j].G));
                    if(Math.Abs(cubeAdj[i][j].H + cubeAdj[i][j].G) < this.cost)
                    {
                        this.cost = cubeAdj[i][j].H + cubeAdj[i][j].G;
                        aux = cubeAdj[i][j];
                    }
                }
            }
            path.Add(aux);
            CalcAdj();
            Distance();

            Cube player = map.GetElement("Player");

            map.UpdateElement(player.Pos.X()/20, player.Pos.Y()/20, aux.Pos.X()/20, aux.Pos.Y()/20, "Player", Brushes.Red);
        }

        void Distance()
        {
            Vector4 auxObj = map.GetElement("Objetivo").Pos;

            int totalCount;
            
            for (int i = 0; i < 3; i++) 
            {
                for (int j = 0; j < 3; j++)
                {
                    totalCount =  Math.Abs((((auxObj.X() / 20) - (cubeAdj[i][j].Pos.X() / 20)) * 10)) +  Math.Abs((((auxObj.Y() / 20) - (cubeAdj[i][j].Pos.Y() / 20)) * 10));
                    cubeAdj[i][j].G = totalCount;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                   //Console.WriteLine(cubeAdj[i][j].G);
                }
            }
        }

        void CalcAdj()
        {
            for (int h = 0; h < map.Row; h++)
            {
                for (int w = 0; w < map.Column; w++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            /*cubeAdj[i][j] = map.GetCubesAdjacents(i, j, (path[path.Count - 1].Pos.X() - 20) / 20, (path[path.Count - 1].Pos.Y() - 20) / 20);
                            map.GetMap()[0][0].H = (map.GetValueAdjacents(i, j, (path[path.Count - 1].Pos.X() - 20) / 20, (path[path.Count - 1].Pos.Y() - 20) / 20)) + cubeAdj[i][j].H;
                            cubeAdj[i][j].H = (map.GetValueAdjacents(i, j, (path[path.Count - 1].Pos.X() - 20) / 20, (path[path.Count - 1].Pos.Y() - 20) / 20)) + cubeAdj[i][j].H;
                             */

                            map.GetMap()[h][w].H = (map.GetValueAdjacents(i, j, (path[path.Count - 1].Pos.X() - 20) / 20, (path[path.Count - 1].Pos.Y() - 20) / 20)) + cubeAdj[i][j].H;
                        }
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //Console.WriteLine(cubeAdj[i][j].H);
                }
            }
        }

        //void MovePlayer()
        //{
        //    foreach (List<Cube> c in map.GetMap())
        //    {
        //        foreach (Cube a in c)
        //        {
        //            if (a.Type == "Player")
        //            {
        //                auxPlayer = a.Pos;
        //            }
        //        }
        //    }
        //    this.cost = 1000;
        //    foreach (List<Cube> c in map.GetMap())
        //    {
        //        foreach (Cube a in c)
        //        {
        //            if (a.Type == "Player")
        //            {
        //                for (int i = 0; i < 3; i++)
        //                {
        //                    for (int j = 0; j < 3; j++)
        //                    {
        //                        Console.WriteLine(adj[i][j] + Distance(map.GetVector(i, j, (a.Pos.X() - 20) / 20, (a.Pos.Y() - 20) / 20)));
        //                        if ( adj[i][j] + Distance(map.GetVector(i, j, (a.Pos.X() - 20) / 20, (a.Pos.Y() - 20) / 20)) < this.cost)
        //                        {
        //                            this.cost = adj[i][j] + Distance(map.GetVector(i, j, (a.Pos.X() - 20) / 20, (a.Pos.Y() - 20) / 20));
        //                            moveObj = new Vector4(i, j, 0, 0);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    map.UpdateElement(auxPlayer.X() / 20, auxPlayer.Y() / 20, moveObj.X(), moveObj.Y(), "Player", Brushes.Red);
        //}

        void Update(object sender, EventArgs e)
        {
            Invalidate();
        }

        void Draw(Object sender, PaintEventArgs PaintNow)
        {
            map.Draw(PaintNow.Graphics);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MovePlayer();
            AddPath();
        }
    }
}
