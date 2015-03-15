using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{
    class Tetrimino
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

        public Tetrimino(int num)
        {
            //新たなテトリミノをつくる

               /* colors of Tetriminos
                * 1:blue
                * 2:red
                * 3:yellow
                * 4:skyblue
                * 5:orange
                * 6:green
                * 7:viored
                */

            switch (num)
            {
                case 0:
                    pattern = o;
                    color = 3;
                    break;
                case 1:
                    pattern = i;
                    color = 4;
                    break;
                case 2:
                    pattern = t;
                    color = 7;
                    break;
                case 3:
                    pattern = s;
                    color = 2;
                    break;
                case 4:
                    pattern = z;
                    color = 6;
                    break;
                case 5:
                    pattern = l;
                    color = 5;
                    break;
                case 6:
                    pattern = j;
                    color = 1;
                    break;
            }
        }

        
    }
}
