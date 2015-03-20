using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{

    class GameManager
    {
        const int column_main = 10;
        const int row_main = 20;
        const int column_next = 4;
        const int row_next = 4;

        //スコア
        public int score = 0;
        //グリッド全体のブロックの配置
        int[,] grid = new int[column_main, row_main];
        //グリッド内で積まれているブロックの配置
        int[,] stack = new int[column_main, row_main];

        int[,] grid_next = new int[column_next, row_next];

        //操作中のテトリミノの原点位置
        int[] position_tetrimino;
        //操作中のテトリミノ
        Tetrimino tetrimino_Controlling = new Tetrimino();
        //次のテトリミノ
        public Tetrimino tetrimino_Next = new Tetrimino();
        //ホールド中のテトリミノ
        public Tetrimino tetrimino_Hold = new Tetrimino();

        public MainPage MP;

        Random R1 = new Random();

        public void startGame(MainPage mainpage)
        {
            MP = mainpage;
            for (int x = 0; x < column_main; x++)
            {
                for (int y = 0; y < row_main; y++)
                {
                    grid[x, y] = 0;
                    stack[x, y] = 0;
                }
            }

            //操作中テトリミノとNEXTテトリミノを取得する
            tetrimino_Controlling = Tetrimino.Get(R1.Next(7));
            position_tetrimino = new int[] { 4, 1 };

            tetrimino_Next = Tetrimino.Get(R1.Next(7));
            //MP.drawNext(tetrimino_Next);

                //グリッドの更新
                for (int i = 0; i < 4; i++)
                {
                    grid[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]] = (int)tetrimino_Controlling.Color;
                }
            updateGrid();

        }
        
        public int[,] getGrid()
        {
            return grid;
        }

        public void getNewBlock()
        {
            position_tetrimino = new int[]{4,1};

            tetrimino_Controlling = Tetrimino.Get((int)tetrimino_Next.Color - 1);
            tetrimino_Next = Tetrimino.Get(R1.Next(7));
            //MP.drawNext(tetrimino_Next);

            

            for (int i = 0; i < 4; i++)
            {
                grid[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]] = (int)tetrimino_Controlling.Color;
            }
            updateGrid();

            if (checkGameOver(tetrimino_Controlling.Pattern))
            {
                //ゲームオーバー
                MP.gameOver();
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    grid[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]] = (int)tetrimino_Controlling.Color;
                }
                updateGrid();
            }
        }

        private bool checkGameOver(int[,] temp)
        {
            //出現ブロックが積んでいるブロックとかぶっていたらtrue
            bool movable = false;

            for(int i = 0;i < 4;i++)
            {
                if (stack[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]] != 0)
                {
                    movable = true;
                }
            }
            return movable;
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
                    grid[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]] = (int)tetrimino_Controlling.Color;
                    stack[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]] = (int)tetrimino_Controlling.Color;
                }
                updateGrid();

                //消せるラインをチェックする
                for (int y = 0; y < row_main; y++)
                {
                    bool destroyable = true;
                    for (int x = 0; x < column_main; x++)
                    {
                        if (stack[x, y] == 0)
                        {
                            destroyable = false;
                        }
                    }
                    if (destroyable)
                    {
                        destroyLine(y);
                    }
                }
                getNewBlock();
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
            reset();
            //rowの行を上の行で上書きする
            for (int i = row; i >= 0; i--)
            {
                for (int x = 0; x < column_main; x++)
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
            for (int x = 0; x < column_main; x++)
            {
                for (int y = 0; y < row_main; y++)
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
                grid[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]] = (int)tetrimino_Controlling.Color;
            }
            //積まれているブロックの反映
            for (int x = 0; x < column_main; x++)
            {
                for (int y = 0; y < row_main; y++)
                {
                    if (stack[x, y] != 0)
                    {
                        grid[x, y] = stack[x,y];
                    }
                }
            }
        }

        public void setRotate()
        {
            var rotate_Right = tetrimino_Controlling.Left;
            if (rotationCheck(rotate_Right.Pattern))
            {
                tetrimino_Controlling = rotate_Right;
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
                if (position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]  + 1 >= row_main) 
                {
                    //壁の外側
                    movable = false;
                }
                else if (stack[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0], position_tetrimino[1] + 1 + tetrimino_Controlling.Pattern[i, 1]] != 0)
                {
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
                if (position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0] + x_shift <= -1)
                {
                    //壁の外側
                    movable = false;
                }
                else if (position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0] + x_shift >= column_main)
                {
                    //壁の外側
                    movable = false;
                }
                else if (stack[position_tetrimino[0] + tetrimino_Controlling.Pattern[i, 0] + x_shift, position_tetrimino[1] + tetrimino_Controlling.Pattern[i, 1]] != 0)
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
                if (position_tetrimino[0] + temp[i, 0] < 0 || position_tetrimino[0] + temp[i, 0] + temp[i, 0] > column_main - 1
                    || position_tetrimino[1] + temp[i,1] > row_main - 1 || position_tetrimino[1] + temp[i,1] < 0)
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
