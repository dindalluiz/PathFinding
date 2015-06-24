using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PathFinding
{
    class Map
    {
        #region Variables
        List<Cube> cubes;
        List<List<Cube>> map;

        string type;

        int aux;
        Cube cubeAux;

        int column = 10;
        int row = 10;
        #endregion

        public Map()
        {
            ResetMap();
        }

        public void ResetMap()
        {
            map = new List<List<Cube>>();
            for (int i = 1; i <= row; i++)
            {
                cubes = new List<Cube>();
                for (int j = 1; j <= column; j++)
                {
                    cubes.Add(new Cube(40 * i, 40 * j, 40, 40, Pens.Black, "Chão", 0, 0));
                }
                map.Add(cubes);
            }
        }

        public int Row
        {
            get
            {
                return row;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }
        }

        public List<List<Cube>> GetMap()
        {
            return map;
        }

        public void AddElement(int x, int y, string type, Brush color)
        {
            map[x - 1][y - 1].Type = type;
            map[x - 1][y - 1].SetColor(color);
        }

        public Cube GetElement(string type)
        {
            foreach (List<Cube> c in map)
            {
                foreach (Cube a in c)
                {
                    if (a.Type == type)
                    {
                        return a;
                    }
                }
            }
            return new Cube();
        }

        public void UpdateElement(int x, int y, int toX, int toY, string type, Brush color)
        {
            map[x - 1][y - 1].Type = "Chão";
            map[x - 1][y - 1].SetColor(Pens.Black);

            map[toX - 1][toY - 1].Type = type;
            map[toX - 1][toY - 1].SetColor(color);

            map[toX - 1][toY - 1].walkable = false;
        }

        public int GetValueAdjacents(int x, int y, int posX, int posY)
        {
            if(posX <0 || posY<0)
            {
                return 0;
            }

            if (x == 0 && y == 0 && map[posX - 1][posY - 1].G == 0 && map[posX - 1][posY - 1].H == 0)
            {
                type = map[posX - 1][posY - 1].Type;
            }
            else if (x == 0 && y == 1 && map[posX][posY - 1].G == 0 && map[posX][posY - 1].H == 0)
            {
                type = map[posX][posY - 1].Type;
            }
            else if (x == 0 && y == 2 && map[posX + 1][posY - 1].G == 0 && map[posX + 1][posY - 1].H == 0)
            {
                type = map[posX + 1][posY - 1].Type;
            }
            else if (x == 1 && y == 0 && map[posX - 1][posY].G == 0 && map[posX - 1][posY].H == 0)
            {
                type = map[posX - 1][posY].Type;
            }
            else if (x == 1 && y == 1 && map[posX][posY].G == 0 && map[posX][posY].H == 0)
            {
                type = map[posX][posY].Type;
            }
            else if (x == 1 && y == 2 && map[posX + 1][posY].G == 0 && map[posX + 1][posY].H == 0)
            {
                type = map[1 + posX][posY].Type;
            }
            else if (x == 2 && y == 0 && map[posX - 1][posY + 1].G == 0 && map[posX - 1][posY + 1].H == 0)
            {
                type = map[posX - 1][1 + posY].Type;
            }
            else if (x == 2 && y == 1 && map[posX][posY + 1].G == 0 && map[posX][posY + 1].H == 0)
            {
                type = map[posX][1 + posY].Type;
            }
            else if (x == 2 && y == 2 && map[posX + 1][posY + 1].G == 0 && map[posX + 1][posY + 1].H == 0)
            {
                type = map[1 + posX][1 + posY].Type;
            }

            if (x == 0 && y == 0 || x == 0 && y == 2 || x == 2 && y == 2 || x == 2 && y == 0)
            {
                aux = 4;
            }
            else
            {
                aux = 0;
            }

                switch (type)
                {
                    case "Chão":
                        return 10 + aux;
                    case "Objetivo":
                        return 5 + aux;
                    case "Wall":
                        return 1000 + aux;
                }
           return 2000;
        }

        public Cube GetCubesAdjacents(int x, int y, int posX, int posY)
        {
            if (posX-1>= 0 && posY-1 >= 0)
            {
                if (x == 0 && y == 0)
                {
                    cubeAux = map[posX - 1][posY - 1];
                }
                else if (x == 0 && y == 1)
                {
                    cubeAux = map[posX][posY - 1];
                }
                else if (x == 0 && y == 2)
                {
                    cubeAux = map[posX + 1][posY - 1];
                }
                else if (x == 1 && y == 0)
                {
                    cubeAux = map[posX - 1][posY];
                }
                else if (x == 1 && y == 1)
                {
                    cubeAux = map[posX][posY];
                }
                else if (x == 1 && y == 2)
                {
                    cubeAux = map[1 + posX][posY];
                }
                else if (x == 2 && y == 0)
                {
                    cubeAux = map[posX - 1][1 + posY];
                }
                else if (x == 2 && y == 1)
                {
                    cubeAux = map[posX][1 + posY];
                }
                else if (x == 2 && y == 2)
                {
                    cubeAux = map[1 + posX][1 + posY];
                }
            }

            return cubeAux;
        }

        public void Draw(Graphics g)
        {
            Font drawFont = new Font("Arial", 10);

            foreach (List<Cube> c in map)
            {
                foreach (Cube a in c)
                {
                    a.Draw(g);

                    g.DrawString(a.H.ToString(), drawFont, Brushes.Black, a.Pos.X(), a.Pos.Y()+25);
                    g.DrawString(a.G.ToString(), drawFont, Brushes.Brown, a.Pos.X() + 20, a.Pos.Y() + 25);
                    g.DrawString((a.G + a.H).ToString(), drawFont, Brushes.DarkViolet, a.Pos.X() + 18, a.Pos.Y());
                }
            }
        }
    }
}
