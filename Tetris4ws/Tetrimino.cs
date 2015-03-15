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
            int num = R1.Next(7);
            switch (num)
            {
                case 0:
                    color = (int)Colors.yellow;
                    break;
                case 1:
                    color = (int)Colors.skyblue;
                    break;
                case 2:
                    color = (int)Colors.viored;
                    break;
                case 3:
                    color = (int)Colors.red;
                    break;
                case 4:
                    color = (int)Colors.green;
                    break;
                case 5:
                    color = (int)Colors.orange;
                    break;
                case 6:
                    color = (int)Colors.blue;
                    break;
            }

            patterns = BlockPattern.allpatterns[num];
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
