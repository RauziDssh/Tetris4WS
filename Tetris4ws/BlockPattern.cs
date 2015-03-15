using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{
    static class BlockPattern
    {
        public static int[][,] patterns_o = new int[4][,]{ 
                          new int[,]{{0,0},{1,0},{0,1},{1,1}},
                          new int[,]{{0,0},{1,0},{0,1},{1,1}},
                          new int[,]{{0,0},{1,0},{0,1},{1,1}},
                          new int[,]{{0,0},{1,0},{0,1},{1,1}}
                    };
        public static int[][,] o =  new int[4][,]{
                      new int[,]{{0,0},{1,0},{0,1},{1,1}},
                      new int[,]{{0,0},{1,0},{0,1},{1,1}},
                      new int[,]{{0,0},{1,0},{0,1},{1,1}},
                      new int[,]{{0,0},{1,0},{0,1},{1,1}}
                    };
        public static int[][,] i = new int[4][,]{
                     new int[,]{{0,-1},{0,0},{0,1},{0,2}},
                     new int[,]{{-1,0},{0,0},{1,0},{2,0}},
                     new int[,]{{0,-1},{0,0},{0,1},{0,2}},
                     new int[,]{{-1,0},{0,0},{1,0},{2,0}}
                    };
        public static int[][,] s = new int[4][,]{
                     new int[,]{{0,1},{0,0},{1,0},{-1,1}},
                     new int[,]{{-1,-1},{-1,0},{0,0},{0,1}},
                     new int[,]{{1,-1},{0,-1},{0,0},{-1,0}},
                     new int[,]{{1,1},{1,0},{0,0},{0,-1}}
                    };
        public static int[][,] z = new int[4][,]{
                      new int[,]{{-1,0},{0,0},{0,1},{1,1}},
                      new int[,]{{0,-1},{0,0},{-1,0},{-1,1}},
                      new int[,]{{1,0},{0,0},{0,-1},{-1,-1}},
                      new int[,]{{0,1},{0,0},{1,0},{1,-1}}
                    };
        public static int[][,] l = new int[4][,]{
                     new int[,]{{1,1},{0,-1},{0,0},{0,1}},
                     new int[,]{{-1,1},{1,0},{0,0},{-1,0}},
                     new int[,]{{-1,-1},{0,1},{0,0},{0,-1}},
                     new int[,]{{1,-1},{-1,0},{0,0},{1,0}}
                   };
        public static int[][,] j = new int[4][,]{
                     new int[,]{{-1,0},{0,0},{1,0},{1,1}},
                     new int[,]{{0,-1},{0,0},{0,1},{-1,1}},
                     new int[,]{{1,0},{0,0},{-1,0},{-1,-1}},
                     new int[,]{{0,1},{0,0},{0,-1},{1,-1}}
                   };
        public static int[][,] t = new int[4][,]{
                     new int[,]{{-1,0},{0,0},{0,1},{1,0}},
                     new int[,]{{0,-1},{0,0},{-1,0},{0,1}},
                     new int[,]{{1,0},{0,0},{0,-1},{-1,0}},
                     new int[,]{{0,1},{0,0},{1,0},{0,-1}}
                   };
    }
}
