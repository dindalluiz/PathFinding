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

            map.AddElement(8, 3, "Player", Brushes.Red);
            map.AddElement(2, 9, "Objetivo", Brushes.Blue);
            map.AddElement(3, 4, "Wall", Brushes.Green);
            map.AddElement(3, 3, "Wall", Brushes.Green);
            map.AddElement(4, 5, "Wall", Brushes.Green);
            map.AddElement(3, 5, "Wall", Brushes.Green);
            map.AddElement(5, 5, "Wall", Brushes.Green);
            map.AddElement(6, 5, "Wall", Brushes.Green);
            map.AddElement(7, 5, "Wall", Brushes.Green);

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
                    if(Math.Abs(cubeAdj[i][j].H + cubeAdj[i][j].G) < this.cost && cubeAdj[i][j].walkable)
                    {
                        this.cost = cubeAdj[i][j].H + cubeAdj[i][j].G;
                        aux = cubeAdj[i][j];
                    }
                }
            }

            path.Add(aux);
            CalcAdj();
            Distance();

            Console.WriteLine(path[path.Count - 1].H);

            Cube player = map.GetElement("Player");

            map.UpdateElement(player.Pos.X()/40, player.Pos.Y()/40, aux.Pos.X()/40, aux.Pos.Y()/40, "Player", Brushes.Red);
        }

        void Distance()
        {
            Vector4 auxObj = map.GetElement("Objetivo").Pos;

            int totalCount;
            
            for (int i = 0; i < 3; i++) 
            {
                for (int j = 0; j < 3; j++)
                {
                    totalCount =  Math.Abs((((auxObj.X() / 40) - (cubeAdj[i][j].Pos.X() / 40)) * 10)) +  Math.Abs((((auxObj.Y() / 40) - (cubeAdj[i][j].Pos.Y() / 40)) * 10));
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
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cubeAdj[i][j] = map.GetCubesAdjacents(i, j, (path[path.Count - 1].Pos.X() - 40) / 40, (path[path.Count - 1].Pos.Y() - 40) / 40);
                    cubeAdj[i][j].H = map.GetValueAdjacents(i, j, (path[path.Count - 1].Pos.X() - 40) / 40, (path[path.Count - 1].Pos.Y() - 40) / 40);
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
            AddPath();
        }
    }
}
