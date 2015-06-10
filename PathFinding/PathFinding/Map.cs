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
        List<Cube> cubes;
        List<List<Cube>> map;

        string type;

        int aux;
        Cube cubeAux;

        int column = 10;
        int row = 10;

        public Map()
        {
            ResetMap();
        }

        void ResetMap()
        {
            map = new List<List<Cube>>();
            for (int i = 1; i <= row; i++)
            {
                cubes = new List<Cube>();
                for (int j = 1; j <= column; j++)
                {
                    cubes.Add(new Cube(20 * i, 20 * j, 20, 20, Pens.Black, "Chão", 0, 0));
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

            /*if (toX == 0 && toY == 0)
            {
                map[x - 2][y - 2].Type = type;
                map[x - 2][y - 2].SetColor(color);
            }
            else if (toX == 0 && toY == 1)
            {
                map[x-1][y - 2].Type = type;
                map[x-1][y - 2].SetColor(color);
            }
            else if (toX == 0 && toY == 2)
            {
                map[x][y - 2].Type = type;
                map[x][y - 2].SetColor(color);
            }
            else if (toX == 1 && toY == 0)
            {
                map[x - 2][y-1].Type = type;
                map[x - 2][y-1].SetColor(color);
            }
            else if (toX == 1 && toY == 1)
            {
                map[x][y].Type = type;
                map[x][y].SetColor(color);
            }
            else if (toX == 1 && toY == 2)
            {
                map[x][y - 2].Type = type;
                map[x][y - 2].SetColor(color);
            }
            else if (toX == 2 && toY == 0)
            {
                map[x - 2][y].Type = type;
                map[x - 2][y].SetColor(color);
            }
            else if (toX == 2 && toY == 1)
            {
                map[x-1][y].Type = type;
                map[x-1][y].SetColor(color);
            }
            else if (toX == 2 && toY == 2)
            {
                map[x][y].Type = type;
                map[x][y].SetColor(color);
            }*/
        }

        public int GetValueAdjacents(int x, int y, int posX, int posY)
        {
            if(x==0&&y==0)
            {
                type = map[posX-1][posY - 1].Type;
            }
            else if (x == 0 && y == 1)
            {
                type = map[x][posY - 1].Type;
            }
            else if (x == 0 && y == 2)
            {
                type = map[posX+1][posY - 1].Type;
            }
            else if (x == 1 && y == 0)
            {
                type = map[posX - 1][y].Type;
            }
            else if (x == 1 && y == 1)
            {
                type = map[x][y].Type;
            }
            else if (x == 1 && y == 2)
            {
                type = map[1 + posX][y].Type;
            }
            else if (x == 2 && y == 0)
            {
                type = map[posX - 1][1 + posY].Type;
            }
            else if (x == 2 && y == 1)
            {
                type = map[x][1 + posY].Type;
            }
            else if (x == 2 && y == 2)
            {
                type = map[1+ posX][1+ posY].Type;
            }

            if (x == 0 && y == 0 || x == 0 && y == 2 || x == 2 && y == 2 || x == 2 && y == 0)
            {
                aux = 4;
            }
            else
            {
                aux = 0;
            }

           switch(type)
           {
               case "Chão":
                   return 10 + aux;
               case "Objetivo":
                   return 5 + aux;
               case "Wall":
                   return 1000+aux;
           }
           return 100;
        }

        public Cube GetCubesAdjacents(int x, int y, int posX, int posY)
        {
            if (x == 0 && y == 0)
            {
                cubeAux = map[posX - 1][posY - 1];
            }
            else if (x == 0 && y == 1)
            {
                cubeAux = map[x][posY - 1];
            }
            else if (x == 0 && y == 2)
            {
                cubeAux = map[posX + 1][posY - 1];
            }
            else if (x == 1 && y == 0)
            {
                cubeAux = map[posX - 1][y];
            }
            else if (x == 1 && y == 1)
            {
                cubeAux = map[x][y];
            }
            else if (x == 1 && y == 2)
            {
                cubeAux = map[1 + posX][y];
            }
            else if (x == 2 && y == 0)
            {
                cubeAux = map[posX - 1][1 + posY];
            }
            else if (x == 2 && y == 1)
            {
                cubeAux = map[x][1 + posY];
            }
            else if (x == 2 && y == 2)
            {
                cubeAux = map[1 + posX][1 + posY];
            }

            return cubeAux;
        }

        public void Draw(Graphics g)
        {
            foreach (List<Cube> c in map)
            {
                foreach (Cube a in c)
                {
                    a.Draw(g);
                }
            }
        }
    }
}
