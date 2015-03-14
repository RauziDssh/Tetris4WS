using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{
    class GameManager
    {
        //スコア
        public int score = 0;
        //グリッド全体のブロックの配置
        int[,] grid = new int[10, 20];
        //グリッド内で積まれているブロックの配置
        int[,] stack = new int[10, 20];

        public GameManager(){
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    grid[x, y] = 0;
                }
            }
        }
        
        public int[,] getGrid()
        {
            return grid;
        }

        //操作中のテトリミノの原点位置
        int[] position_tetrimino;
        //操作中のテトリミノの形状
        int[,] tetrimino_current;

        public void getNewBlock()
        {
            position_tetrimino = new int[]{4,1};
            Tetrimino tetrimino_new = new Tetrimino();
            tetrimino_current = tetrimino_new.getNewTetrimino();
            for (int i = 0; i < 4; i++)
            {
                grid[position_tetrimino[0] + tetrimino_current[i, 0], position_tetrimino[1] + tetrimino_current[i, 1]] = 1;
            }
            updateGrid();
        }

        public void setBlockDown()
        {
            if (shiftCheck_Vertical())
            {
                //下方向に移動する
                position_tetrimino[1] = position_tetrimino[1] + 1;
                reset();
                updateGrid();
            }
            else
            {
                //ブロックを積む
                for (int i = 0; i < 4; i++)
                {
                    grid[position_tetrimino[0] + tetrimino_current[i, 0], position_tetrimino[1] + tetrimino_current[i, 1]] = 1;
                }
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        if (grid[x, y] == 1)
                        {
                            stack[x, y] = 1;
                        }
                    }
                }
                updateGrid();

                //消せるラインをチェックする
                for (int y = 0; y < 20; y++)
                {
                    int count = 0;
                    for (int x = 0; x < 10; x++)
                    {
                        count += stack[x,y];
                    }
                    if (count >= 10)
                    {
                        destroyLine(y);
                    }
                }
                this.getNewBlock();

            }
        }

        public void shiftHorizontal(int x)
        {
            if (shiftCheck_Horizontal(x))
            {
                //横方向に移動する
                position_tetrimino[0] = position_tetrimino[0] + x;
                reset();
                updateGrid();
            }
        }

        private void destroyLine(int row)
        {
            score += 100;
            //rowの行を上の行で上書きする
            for (int i = row; i >= 0; i--)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (i == 0) 
                    {
                        stack[x, i] = 0;
                    }
                    else
                    {
                        stack[x, i] = stack[x, i - 1];
                    }
                }
            }
        }

        private void reset()
        {
            //全体の初期化
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    grid[x, y] = 0;
                }
            }
        }

        private void updateGrid()
        {
            //操作ブロックの反映
            for (int i = 0; i < 4; i++)
            {
                grid[position_tetrimino[0] + tetrimino_current[i, 0], position_tetrimino[1] + tetrimino_current[i, 1]] = 1;
            }
            //積まれているブロックの反映
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (stack[x, y] == 1)
                    {
                        grid[x, y] = 1;
                    }
                }
            }
        }

        public void setRotate()
        {
            //90度回転する
            int[,] pattern_rot90 = new int[4, 2];
            for (int m = 0; m < 4; m++)
            {
                pattern_rot90[m, 0] = 0 * tetrimino_current[m, 0] + (-1) * tetrimino_current[m, 1];
                pattern_rot90[m, 1] = 1 * tetrimino_current[m, 0] + 0 * tetrimino_current[m, 1];
            }
            if (rotationCheck(pattern_rot90))
            {
                tetrimino_current = pattern_rot90;
                reset();
                updateGrid();
            }
        }
        
        /*
         *動かせるかどうかの判定
         *移動先が0だったらtrue
         *1orNullだったらfalseを返す処理をつくる
         */

        //配列のオーバーフローが最初におきるからnullでの判定は無理っぽい

        //下方向のチェック
        private bool shiftCheck_Vertical()
        {
            bool movable = true;
            for (int i = 0; i < 4; i++)
            {
                if (position_tetrimino[1] + tetrimino_current[i, 1]  + 1 >= 20) 
                {
                    //壁の外側
                    movable = false;
                }
                else if (stack[position_tetrimino[0] + tetrimino_current[i, 0], position_tetrimino[1] + 1 +  tetrimino_current[i, 1]] != 0) {
                    //壁の内側での判定
                    movable = false;
                };
            }
            return movable;
        }
        //左右方向のチェック
        private bool shiftCheck_Horizontal(int x_shift)
        {
            bool movable = true;
            /*
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    grid[x, y] = 0;
                }
            }*/
            for (int i = 0; i < 4; i++)
            {
                if (position_tetrimino[0] + tetrimino_current[i, 0] + x_shift <= -1)
                {
                    //壁の外側
                    movable = false;
                }
                else if (position_tetrimino[0] + tetrimino_current[i, 0] + x_shift >= 10)
                {
                    //壁の外側
                    movable = false;
                }
                else if (stack[position_tetrimino[0] + tetrimino_current[i, 0] + x_shift , position_tetrimino[1] + tetrimino_current[i, 1]] != 0)
                {
                    //壁の内側での判定
                    movable = false;
                };
            }
            return movable;
        }
        //回転のチェック
        private bool rotationCheck(int[,] temp)
        {
            bool movable = true;
            for (int i = 0; i < 4; i++)
            {
                if (position_tetrimino[0] + temp[i, 0] < 0 || position_tetrimino[0] + temp[i, 0] + temp[i, 0] > 9
                    || position_tetrimino[1] + temp[i,1] > 19)
                { 
                    movable = false;
                }
                else if (stack[position_tetrimino[0] + temp[i, 0], position_tetrimino[1] + temp[i, 1]] != 0)
                {
                    movable = false;
                }
            }
                return movable;
        }
    }
}
