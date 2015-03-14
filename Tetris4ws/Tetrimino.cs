using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{
    class Tetrimino
    {
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
                       {-1,1},
                       {-1,0},
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
        int[][,] patterns;

        public int[,] getNewTetrimino()
        {
            //新たなテトリミノをつくる
            patterns = new int[][,] { i, s, z, t, j, o, l };
            Random R1 = new Random();
            return patterns[R1.Next(7)];
        }

        
    }
}
