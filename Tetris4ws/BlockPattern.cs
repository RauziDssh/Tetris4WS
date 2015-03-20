﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{
    static class BlockPattern
    {
        public static int[][][,] allpatterns = new int[7][][,]{
                        //o,i,t,s,z,l,j
                        //o
                        new int[4][,]{ 
                          new int[,]{{0,0},{1,0},{0,1},{1,1}},
                          new int[,]{{0,0},{1,0},{0,1},{1,1}},
                          new int[,]{{0,0},{1,0},{0,1},{1,1}},
                          new int[,]{{0,0},{1,0},{0,1},{1,1}}
                        },
                        //i
                        new int[4][,]{
                          new int[,]{{0,-1},{0,0},{0,1},{0,2}},
                          new int[,]{{-1,0},{0,0},{1,0},{2,0}},
                          new int[,]{{0,-1},{0,0},{0,1},{0,2}},
                          new int[,]{{-1,0},{0,0},{1,0},{2,0}}
                        },
                        //t
                        new int[4][,]{
                             new int[,]{{-1,0},{0,0},{0,1},{1,0}},
                             new int[,]{{0,-1},{0,0},{-1,0},{0,1}},
                             new int[,]{{1,0},{0,0},{0,-1},{-1,0}},
                             new int[,]{{0,1},{0,0},{1,0},{0,-1}}
                       },
                       //s
                       new int[4][,]{
                           new int[,]{{0,1},{0,0},{1,0},{-1,1}},
                           new int[,]{{-1,-1},{-1,0},{0,0},{0,1}},
                           new int[,]{{1,-1},{0,-1},{0,0},{-1,0}},
                           new int[,]{{1,1},{1,0},{0,0},{0,-1}}
                       },
                       //z
                       new int[4][,]{
                          new int[,]{{-1,0},{0,0},{0,1},{1,1}},
                          new int[,]{{0,-1},{0,0},{-1,0},{-1,1}},
                          new int[,]{{1,0},{0,0},{0,-1},{-1,-1}},
                          new int[,]{{0,1},{0,0},{1,0},{1,-1}}
                        },
                        //l
                        new int[4][,]{
                         new int[,]{{1,1},{0,-1},{0,0},{0,1}},
                         new int[,]{{-1,1},{1,0},{0,0},{-1,0}},
                         new int[,]{{-1,-1},{0,1},{0,0},{0,-1}},
                         new int[,]{{1,-1},{-1,0},{0,0},{1,0}}
                       },
                       //j
                       new int[4][,]{
                         new int[,]{{-1,0},{0,0},{1,0},{1,1}},
                         new int[,]{{0,-1},{0,0},{0,1},{-1,1}},
                         new int[,]{{1,0},{0,0},{-1,0},{-1,-1}},
                         new int[,]{{0,1},{0,0},{0,-1},{1,-1}}
                       }
                  };

    }
}