using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{
    public class Tetrimino
    {
        public int[,] pattern;
        public int[][,] patterns;
        public int color;
        public int rotation = 0;

        enum Colors {black,blue,red,yellow,skyblue,orange,green,viored};

        Random R1 = new Random();

        public void getNewTetrimino()
        {

            switch (R1.Next(7))
            {
                case 0:
                    patterns = BlockPattern.o;
                    color = (int)Colors.yellow;
                    break;
                case 1:
                    patterns = BlockPattern.i;
                    color = (int)Colors.skyblue;
                    break;
                case 2:
                    patterns = BlockPattern.t;
                    color = (int)Colors.viored;
                    break;
                case 3:
                    patterns = BlockPattern.s;
                    color = (int)Colors.red;
                    break;
                case 4:
                    patterns = BlockPattern.z;
                    color = (int)Colors.green;
                    break;
                case 5:
                    patterns = BlockPattern.l;
                    color = (int)Colors.orange;
                    break;
                case 6:
                    patterns = BlockPattern.j;
                    color = (int)Colors.blue;
                    break;
            }

            rotation = 0;
            pattern = patterns[rotation];
        }

        public void setRotate90(int num)
        {
            rotation += num;
            if (rotation == 4)
            {
                rotation = 0;
            }
            else if (rotation == -1)
            {
                rotation = 3;
            }
            pattern = patterns[rotation];
        }

    }
}
