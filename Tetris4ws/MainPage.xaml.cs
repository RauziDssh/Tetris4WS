using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Tetris4ws
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //ゲームが進行中か
        static bool playable;

        static int column = 10;
        static int row = 20;

        static int[,] grid = new int[10,20];
        static Image[,] cells = new Image[10, 20];

        static Image[,] cells_Next = new Image[4, 4];

        static BitmapImage bmp_block;
        static BitmapImage bmp_black;
        static BitmapImage bmp_grey;
        static BitmapImage bmp_blue;
        static BitmapImage bmp_green;
        static BitmapImage bmp_yellow;
        static BitmapImage bmp_red;
        static BitmapImage bmp_orange;
        static BitmapImage bmp_viored;
        static BitmapImage bmp_skyblue;

        static DispatcherTimer timer;
        static DispatcherTimer timer_Down;

        public MainPage()
        {
            this.InitializeComponent();


            //Bitmapリソースの取得
            bmp_block = new BitmapImage(new Uri("ms-appx:///Assets/blockparts.bmp"));
            bmp_black = new BitmapImage(new Uri("ms-appx:///Assets/black.bmp"));
            bmp_grey = new BitmapImage(new Uri("ms-appx:///Assets/blockparts_Grey.bmp"));

            bmp_blue = new BitmapImage(new Uri("ms-appx:///Assets/blockparts_Blue.bmp"));
            bmp_red = new BitmapImage(new Uri("ms-appx:///Assets/blockparts_Red.bmp"));
            bmp_yellow = new BitmapImage(new Uri("ms-appx:///Assets/blockparts_Yellow.bmp"));
            bmp_viored = new BitmapImage(new Uri("ms-appx:///Assets/blockparts_viored.bmp"));
            bmp_skyblue = new BitmapImage(new Uri("ms-appx:///Assets/blockparts_Skyblue.bmp"));
            bmp_orange = new BitmapImage(new Uri("ms-appx:///Assets/blockparts_Orange.bmp"));
            bmp_green = new BitmapImage(new Uri("ms-appx:///Assets/blockparts_Green.bmp"));

            //グリッドの初期化,描画セルの確保
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    grid[x, y] = 0;

                    cells[x, y] = new Image();
                    Canvas01.Children.Add(cells[x,y]);
                    Canvas.SetLeft(cells[x,y], x * (Canvas01.Width / column));
                    Canvas.SetTop(cells[x,y], y * (Canvas01.Height / row));
                }
            }

            //NEXTの描画セルの確保
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    cells_Next[x, y] = new Image();
                    Canvas_next.Children.Add(cells_Next[x, y]);
                    Canvas.SetLeft(cells_Next[x, y], x * (Canvas_next.Width / 4));
                    Canvas.SetTop(cells_Next[x, y], y * (Canvas_next.Height / 4));
                }
            }

            //描画タイマのインターバルを設定
            timer = new DispatcherTimer();
            double interval = 1 / 30;
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Tick += update;
            timer.Start();

            playable = true;
            
            //操作タイマのインターバルを設定
            timer_Down = new DispatcherTimer();
            double interval_Down = 0.5;
            timer_Down.Interval = TimeSpan.FromSeconds(interval_Down);
            timer_Down.Tick += update_Down;
            timer_Down.Start();

            /*
            DispatcherTimer testtimer = new DispatcherTimer();
            double testinterval = 0.3;
            testtimer.Interval = TimeSpan.FromSeconds(testinterval);
            testtimer.Tick += testupdate;
            testtimer.Start();*/

            GameManager.startGame(this);
            GameManager.getNewBlock();
        }

        public void gameOver()
        {
            timer.Stop();
            timer_Down.Stop();
            playable = false;
            
            //ブロックをグレーに
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (grid[x, y] != 0)
                    {
                        cells[x, y].Source = bmp_grey;
                    }
                    else
                    {
                        cells[x, y].Source = bmp_black;
                    }
                }
            }
        }

        
        public void drawNext(Tetrimino nextblocks)
        {
            for (int x = 0; x < 4;x++)
            {
                for(int y = 0;y < 4;y++)
                {
                    cells_Next[x, y].Source = bmp_black;
                }
            }
            BitmapImage color = getColor(nextblocks.color);
            for (int i = 0; i < 4; i++)
            {
                cells_Next[1 + nextblocks.pattern[i, 0], 1 + nextblocks.pattern[i, 1]].Source = color;
            }
        }

        private BitmapImage getColor(int num)
        {
            switch (num)
            {
                case 0:
                    return bmp_black;
                case 1:
                    return bmp_blue;
                case 2:
                    return bmp_red;
                case 3:
                    return bmp_yellow;
                case 4:
                    return bmp_skyblue;
                case 5:
                    return bmp_orange;
                case 6:
                    return bmp_green;
                case 7:
                    return bmp_viored;
                default:
                    return bmp_black;
            }
        }

        private void Grid01_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //キーボード入力処理
            if (playable)
            {
                switch (e.Key.ToString())
                {
                    case "Left":
                        GameManager.shiftHorizontal(-1);
                        break;
                    case "Right":
                        GameManager.shiftHorizontal(1);
                        break;
                    case "Down":
                        GameManager.setBlockDown();
                        break;
                    case "Space":
                        GameManager.setRotate();
                        break;
                }
            }
        }

        int counter = 0;
        private void update_Down(object sender, object e)
        {
            tb1.Text = counter.ToString();
            counter++;

            if (playable)
            {
                GameManager.setBlockDown();
            }
        }

        private void button_Down_Click(object sender, RoutedEventArgs e)
        {
            if (playable)
            {
                GameManager.setBlockDown();
            }
        }

        private void button_Rotate_Click(object sender, RoutedEventArgs e)
        {
            if (playable)
            {
                GameManager.setRotate();
            }
        }

        private void button_Left_Click(object sender, RoutedEventArgs e)
        {
            if (playable)
            {
                GameManager.shiftHorizontal(-1);
            }
        }

        private void button_Right_Click(object sender, RoutedEventArgs e)
        {
            if (playable)
            {
                GameManager.shiftHorizontal(1);
            }
        }

        /*
        int testrow = 0;
        private void testupdate(object sender, object e)
        {
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    if (y == testrow)
                    {
                        grid[x, y] = 1;
                    }
                    else
                    {
                        grid[x, y] = 0;
                    }
                    
                }
            }
            testrow++;
            if (testrow == 21)
            {
                testrow = 0;
            }
        }*/

        /* colors of Tetriminos
        * 1:blue
        * 2:red
        * 3:yellow
        * 4:skyblue
        * 5:orange
        * 6:green
        * 7:viored
        */

        Tetrimino next = new Tetrimino();
        
        private void update(object sender, object e)
        {
            
            //タイマーのインターバルごとに処理
            debugbox.Text = GameManager.score.ToString();
            grid = GameManager.getGrid();

            //描画処理（毎フレームやる）
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    /*
                    if (grid[x, y] == 1)
                    {
                        cells[x,y].Source = bmp_blue;
                    }
                    else
                    {
                        cells[x,y].Source = bmp_black;
                    }*/
                    cells[x, y].Source = getColor(grid[x, y]);
                }
            }
            
        }

    }
}
