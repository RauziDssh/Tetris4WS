using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{
    static class GameManager
    {
        //スコア
        public static int score = 0;
        //グリッド全体のブロックの配置
        static int[,] grid = new int[10, 20];
        //グリッド内で積まれているブロックの配置
        static int[,] stack = new int[10, 20];

        //操作中のテトリミノの原点位置
        static int[] position_tetrimino;
        //操作中のテトリミノ
        static Tetrimino tetrimino_Controlling = new Tetrimino();
        //次のテトリミノ
        public static Tetrimino tetrimino_Next = new Tetrimino();

        public static MainPage MP;

        public static void startGame(MainPage mainpage)
        {
            MP = mainpage;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    grid[x, y] = 0;
                    stack[x, y] = 0;
                }
            }

            //操作中テトリミノとNEXTテトリミノを取得する
            tetrimino_Controlling.getNewTetrimino();
            position_tetrimino = new int[] { 4, 1 };

            tetrimino_Next.getNewTetrimino();
            MP.drawNext(tetrimino_Next);

            //グリッドの更新
            for (int i = 0; i < 4; i++)
            {
                grid[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]] = tetrimino_Controlling.color;
            }
            updateGrid();

        }
        
        public static int[,] getGrid()
        {
            return grid;
        }

        private static void clone_Tetrimino(Tetrimino curr,Tetrimino next)
        {
            curr.pattern = next.pattern;
            curr.patterns = next.patterns;
            curr.color = next.color;
            curr.rotation = next.rotation;
        }

        public static void getNewBlock()
        {
            position_tetrimino = new int[]{4,1};

            //参照型だから = で渡すとダメっぽい
            clone_Tetrimino(tetrimino_Controlling,tetrimino_Next);
            tetrimino_Next.getNewTetrimino();
            MP.drawNext(tetrimino_Next);

            for (int i = 0; i < 4; i++)
            {
                grid[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]] = tetrimino_Controlling.color;
            }
            updateGrid();

            if (checkGameOver(tetrimino_Controlling.pattern))
            {
                //ゲームオーバー
                MP.gameOver();
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    grid[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]] = tetrimino_Controlling.color;
                }
                updateGrid();
            }
        }

        private static bool checkGameOver(int[,] temp)
        {
            //出現ブロックが積んでいるブロックとかぶっていたらtrue
            bool movable = false;

            for(int i = 0;i < 4;i++)
            {
                if (stack[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]] != 0)
                {
                    movable = true;
                }
            }
            return movable;
        }

        public static void setBlockDown()
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
                    grid[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]] = tetrimino_Controlling.color;
                    stack[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]] = tetrimino_Controlling.color;
                }
                updateGrid();

                //消せるラインをチェックする
                for (int y = 0; y < 20; y++)
                {
                    bool destroyable = true;
                    for (int x = 0; x < 10; x++)
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

        public static void shiftHorizontal(int x)
        {
            if (shiftCheck_Horizontal(x))
            {
                //横方向に移動する
                position_tetrimino[0] = position_tetrimino[0] + x;
                reset();
                updateGrid();
            }
        }

        private static void destroyLine(int row)
        {
            score += 100;
            reset();
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

        private static void reset()
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

        private static void updateGrid()
        {
            //操作ブロックの反映
            for (int i = 0; i < 4; i++)
            {
                grid[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0], position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]] = tetrimino_Controlling.color;
            }
            //積まれているブロックの反映
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (stack[x, y] != 0)
                    {
                        grid[x, y] = stack[x,y];
                    }
                }
            }
        }

        public static void setRotate()
        {
            //90度回転する(チェックしてできなかったら戻す)
            tetrimino_Controlling.setRotate90(1);
            if (rotationCheck(tetrimino_Controlling.pattern))
            {
                reset();
                updateGrid();
            }
            else
            {
                tetrimino_Controlling.setRotate90(-1);
            }
        }
        
        /*
         *動かせるかどうかの判定
         *移動先が0だったらtrue
         *1orNullだったらfalseを返す処理をつくる
         */
        //配列のオーバーフローが最初におきるからnullでの判定は無理っぽい

        //下方向のチェック
        private static bool shiftCheck_Vertical()
        {
            bool movable = true;
            for (int i = 0; i < 4; i++)
            {
                if (position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]  + 1 >= 20) 
                {
                    //壁の外側
                    movable = false;
                }
                else if (stack[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0], position_tetrimino[1] + 1 + tetrimino_Controlling.pattern[i, 1]] != 0)
                {
                    //壁の内側での判定
                    movable = false;
                };
            }
            return movable;
        }
        //左右方向のチェック
        private static bool shiftCheck_Horizontal(int x_shift)
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
                if (position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0] + x_shift <= -1)
                {
                    //壁の外側
                    movable = false;
                }
                else if (position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0] + x_shift >= 10)
                {
                    //壁の外側
                    movable = false;
                }
                else if (stack[position_tetrimino[0] + tetrimino_Controlling.pattern[i, 0] + x_shift, position_tetrimino[1] + tetrimino_Controlling.pattern[i, 1]] != 0)
                {
                    //壁の内側での判定
                    movable = false;
                };
            }
            return movable;
        }
        //回転のチェック
        private static bool rotationCheck(int[,] temp)
        {
            bool movable = true;
            for (int i = 0; i < 4; i++)
            {
                if (position_tetrimino[0] + temp[i, 0] < 0 || position_tetrimino[0] + temp[i, 0] + temp[i, 0] > 9
                    || position_tetrimino[1] + temp[i,1] > 19 || position_tetrimino[1] + temp[i,1] < 0)
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
