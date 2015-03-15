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
        public int color;

        int[,] o = {
                        {0,0},
                        {1,0},
                        {0,1},
                        {1,1},
                    };
        int[,] i = {
                        {0,-1},
                        {0,0},
                        {0,1},
                        {0,2},
                    };
        int[,] s = {
                        {0,1},
                        {0,0},
                        {1,0},
                        {-1,1},
                    };
        int[,] z = {
                        {-1,0},
                        {0,0},
                        {0,1},
                        {1,1},
                    };
        int[,] l = {
                       {1,1},
                       {0,-1},
                       {0,0},
                       {0,1},
                   };
        int[,] j = {
                       {-1,0},
                       {0,0},
                       {1,0},
                       {1,1},
                   };
        int[,] t = {
                       {-1,0},
                       {0,0},
                       {0,1},
                       {1,0},
                   };

        enum Colors {black,blue,red,yellow,skyblue,orange,green,viored };

        public Tetrimino(int num)
        {
            //新たなテトリミノをつくる

            switch (num)
            {
                case 0:
                    pattern = o;
                    color = (int)Colors.yellow;
                    break;
                case 1:
                    pattern = i;
                    color = (int)Colors.skyblue;
                    break;
                case 2:
                    pattern = t;
                    color = (int) Colors.viored;
                    break;
                case 3:
                    pattern = s;
                    color = (int) Colors.red;
                    break;
                case 4:
                    pattern = z;
                    color = (int)Colors.green;
                    break;
                case 5:
                    pattern = l;
                    color = (int) Colors.orange;
                    break;
                case 6:
                    pattern = j;
                    color = (int)Colors.blue;
                    break;
            }
        }

        
    }
}
