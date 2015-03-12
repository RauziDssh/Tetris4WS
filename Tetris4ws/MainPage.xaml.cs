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
        BitmapImage bmp_block;
        BitmapImage bmp_black;

        public MainPage()
        {
            this.InitializeComponent();


            //Bitmapリソースの取得
            bmp_block = new BitmapImage(new Uri("ms-appx:///Assets/blockparts.bmp"));
            bmp_black = new BitmapImage(new Uri("ms-appx:///Assets/black.bmp"));

            //グリッドの初期化
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    grid[x, y] = 0;
                }
            }

            //タイマのインターバルを設定
            DispatcherTimer timer = new DispatcherTimer();
            double interval = 1 / 60;
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Tick += update;
            timer.Start();
        }

        private void update(object sender, object e)
        {
            //タイマーのインターバルごとに処理

            //描画処理（毎フレームやる）
            Canvas01.Children.Clear();
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (grid[x, y] == 1)
                    {
                        Image img_block = new Image();
                        img_block.Source = bmp_block;
                        Canvas01.Children.Add(img_block);
                        Canvas.SetLeft(img_block,x * (Canvas01.Width / column));
                        Canvas.SetTop(img_block, y * (Canvas01.Height / row));
                    }
                    else
                    {
                        Image img_black = new Image();
                        img_black.Source = bmp_black;
                        Canvas01.Children.Add(img_black);
                        Canvas.SetLeft(img_black, x * (Canvas01.Width / column));
                        Canvas.SetTop(img_black, y * (Canvas01.Height / row));
                    }
                }
            }
            
        }


    }
}
