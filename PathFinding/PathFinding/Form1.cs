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

        bool playerMap, objMap, wallMap = false;

        int cost = 1000;

        Vector4 obj;

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

            /*map.AddElement(8, 3, "Player", Brushes.Red);
            map.AddElement(2, 9, "Objetivo", Brushes.Blue);
            map.AddElement(3, 4, "Wall", Brushes.Green);
            map.AddElement(3, 2, "Wall", Brushes.Green);
            map.AddElement(3, 3, "Wall", Brushes.Green);
            map.AddElement(4, 5, "Wall", Brushes.Green);
            map.AddElement(3, 5, "Wall", Brushes.Green);
            map.AddElement(5, 5, "Wall", Brushes.Green);
            map.AddElement(6, 5, "Wall", Brushes.Green);
            map.AddElement(7, 5, "Wall", Brushes.Green);*/
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

            Cube player = map.GetElement("Player");

            if (player.Pos == obj)
            {
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.button3.Enabled = false;
                this.button4.Enabled = false;
                return;
            }

            CalcAdj();
            Distance();

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

            map.UpdateElement(player.Pos.X() / 40, player.Pos.Y() / 40, aux.Pos.X() / 40, aux.Pos.Y() / 40, "Player", Brushes.Red);
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
            if (map.GetElement("Player").Pos == obj)
            {
                MessageBox.Show("Já está no objetivo, reseta ae");
                return;
            }
            else if (map.GetElement("Player").none == new Cube().none || map.GetElement("Objetivo").none == new Cube().none)
            {
                MessageBox.Show("Coloque o player e o objetivo no mapa");
                return;
            }

            AddPath();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (map.GetElement("Player").none == new Cube().none)
            {
                this.playerMap = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (map.GetElement("Objetivo").none == new Cube().none)
            {
                this.objMap = true;
                obj = map.GetElement("Objetivo").Pos;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.wallMap = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                double posX = e.X / 40;
                double posY = e.Y / 40;
                if(posX <=10 && posY <=10 && posX >= 1 && posY >= 1)
                {
                    if(this.playerMap)
                    {
                        this.playerMap = false;
                        map.AddElement((int)Math.Round(posX), (int)Math.Round(posY), "Player", Brushes.Red);
                    }
                    if(this.objMap)
                    {
                        this.objMap = false;
                        map.AddElement((int)Math.Round(posX), (int)Math.Round(posY), "Objetivo", Brushes.Blue);
                        obj = map.GetElement("Objetivo").Pos;
                    }
                    if(this.wallMap)
                    {
                        this.wallMap = false;
                        map.AddElement((int)Math.Round(posX), (int)Math.Round(posY), "Wall", Brushes.Green);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
            //map.ResetMap();
            //cubeAdj.Clear();
            //path.Clear();
            //this.cost = 1000;
        }
    }
}
