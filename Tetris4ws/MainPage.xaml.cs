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
        int column = 10;
        int row = 20;

        int[,] grid = new int[10,20];
        Image[,] cells = new Image[10, 20];

        BitmapImage bmp_block;
        BitmapImage bmp_black;
        GameManager GM;

        public MainPage()
        {
            this.InitializeComponent();


            //Bitmapリソースの取得
            bmp_block = new BitmapImage(new Uri("ms-appx:///Assets/blockparts.bmp"));
            bmp_black = new BitmapImage(new Uri("ms-appx:///Assets/black.bmp"));

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

            //描画タイマのインターバルを設定
            DispatcherTimer timer = new DispatcherTimer();
            double interval = 1 / 60;
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Tick += update;
            timer.Start();

            //操作タイマのインターバルを設定
            DispatcherTimer timer_Down = new DispatcherTimer();
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

            GM = new GameManager();
            GM.getNewBlock();
        }

        private void update_Down(object sender, object e)
        {
            GM.setBlockDown();
        }

        private void button_Down_Click(object sender, RoutedEventArgs e)
        {
            GM.setBlockDown();
        }

        private void button_Rotate_Click(object sender, RoutedEventArgs e)
        {
            GM.setRotate();
        }

        private void button_Left_Click(object sender, RoutedEventArgs e)
        {
            GM.shiftHorizontal(-1);
        }

        private void button_Right_Click(object sender, RoutedEventArgs e)
        {
            GM.shiftHorizontal(1);
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


        private void update(object sender, object e)
        {
            //タイマーのインターバルごとに処理
            debugbox.Text = GM.score.ToString();
            grid = GM.getGrid();

            //描画処理（毎フレームやる）
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (grid[x, y] == 1)
                    {
                        cells[x,y].Source = bmp_block;
                    }
                    else
                    {
                        cells[x,y].Source = bmp_black;
                    }
                }
            }
            
        } 
    }
}
