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
    /// 

    public sealed partial class MainPage : Page
    {
        //ゲームが進行中か
        static bool playable;

        const int column_main = 10;
        const int row_main = 20;
        const int column_next = 4;
        const int row_next = 4;

        static int[,] grid = new int[column_main,row_main];
        static Image[,] cells = new Image[column_main, row_main];

        static Image[,] cells_Next = new Image[column_next, row_next];

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

        GameManager GM = new GameManager();

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
            for (int x = 0; x < column_main; x++)
            {
                for (int y = 0; y < row_main; y++)
                {
                    grid[x, y] = 0;

                    cells[x, y] = new Image();
                    Canvas01.Children.Add(cells[x,y]);
                    Canvas.SetLeft(cells[x,y], x * (Canvas01.Width / column_main));
                    Canvas.SetTop(cells[x,y], y * (Canvas01.Height / row_main));
                }
            }

            //NEXTの描画セルの確保
            for (int x = 0; x < column_next; x++)
            {
                for (int y = 0; y < row_next; y++)
                {
                    cells_Next[x, y] = new Image();
                    Canvas_next.Children.Add(cells_Next[x, y]);
                    Canvas.SetLeft(cells_Next[x, y], x * (Canvas_next.Width / column_next));
                    Canvas.SetTop(cells_Next[x, y], y * (Canvas_next.Height / row_next));
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
            
            GM.startGame(this);
            GM.getNewBlock();
        }

        public void gameOver()
        {
            //ブロックをグレーに
            for (int x = 0; x < column_main; x++)
            {
                for (int y = 0; y < row_main; y++)
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

            timer.Stop();
            timer_Down.Stop();
            playable = false;
            
        }

        
        public void drawNext(int[,] grid_next_new)
        {
            for (int x = 0; x < column_next;x++)
            {
                for(int y = 0;y < row_next;y++)
                {
                    cells_Next[x, y].Source = bmp_black;
                }
            }
            BitmapImage color = getColor(grid_next_new[1,1]);
            for (int x = 0; x < column_next;x++)
            {
                for(int y = 0;y < row_next;y++)
                {
                    if(grid_next_new[x,y] != 0)
                    {
                        cells_Next[x,y].Source = color;
                    }
                }
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
                        GM.shiftHorizontal(-1);
                        break;
                    case "Right":
                        GM.shiftHorizontal(1);
                        break;
                    case "Down":
                        GM.setBlockDown();
                        break;
                    case "Space":
                        GM.setRotate();
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
                GM.setBlockDown();
            }
        }

        private void button_Down_Click(object sender, RoutedEventArgs e)
        {
            if (playable)
            {
                GM.setBlockDown();
            }
        }

        private void button_Rotate_Click(object sender, RoutedEventArgs e)
        {
            if (playable)
            {
                GM.setRotate();
            }
        }

        private void button_Left_Click(object sender, RoutedEventArgs e)
        {
            if (playable)
            {
                GM.shiftHorizontal(-1);
            }
        }

        private void button_Right_Click(object sender, RoutedEventArgs e)
        {
            if (playable)
            {
                GM.shiftHorizontal(1);
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
            debugbox.Text = GM.score.ToString();
            grid = GM.getGrid();

            //描画処理（毎フレームやる）
            for (int x = 0; x < column_main; x++)
            {
                for (int y = 0; y < row_main; y++)
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
