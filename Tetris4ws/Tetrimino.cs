using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris4ws
{
    // enumは、Tetriminoの外で定義する
    public enum RotationAngle { Degree0 = 0, Degree90, Degree180, Degree270 }
    public enum Colors { Yellow = 1, Skyblue = 2, Viored = 3, Red = 4, Green = 5, Orange = 6, Blue = 7, Black = 0 }
    public enum TetriminoType { O = 0, I = 1, T = 2, S = 3 ,Z = 4, L = 5, J = 6 }

    // クラスの基本原則は情報隠蔽。
    // 外部から直接触れるような変数は極力減らす
    // 変数を変更できる入り口を極力減らす
    // 大規模なプログラムではどこから変数を修正しているか分からなくなることが多い
    class Tetrimino
    {
        static Tetrimino[] tetriminos;

        static Tetrimino()
        {
            // 起動時に、全ての Tetrimino を作成してしまう。
            tetriminos = new Tetrimino[patterns.Length];
            for (int i = 0; i < patterns.Length; i++)
            {
                tetriminos[i] = new Tetrimino { Pattern = patterns[i] };
            }
            for (int i = 0; i < patterns.Length; i++)
            {
                var typeBase = i & ~3;
                var rotate = i & 3;
                // 色はインデックスを4で割った値 + 1 になる
                tetriminos[i].Color = (Colors)((typeBase / 4) + 1);
                // +1で左回転,-1で右回転
                tetriminos[i].Left = tetriminos[typeBase + ((rotate + 3) & 3)];
                tetriminos[i].Right = tetriminos[typeBase + ((rotate + 1) & 3)];
            }
        }

        //ランダムなパターンを取得する
        public static Tetrimino Get(int num)
        {
            return tetriminos[num* 4];
        }

        // 指定されたパターンを取得する
        public static Tetrimino Get(TetriminoType type)
        {
            return tetriminos[(int)type* 4];
        }

        // 指定されたパターンを取得する
        public static Tetrimino Get(TetriminoType type, RotationAngle angle)
        {
            return tetriminos[(int)type * 4 + (int)angle];
        }

        // パターンは読み込みしか出来なくて良いはず
        public int[,] Pattern { get; private set; }

        // これも外部から、直接、色を変更できないようにする
        public Colors Color { get; private set; }

        public Tetrimino Left { get; private set; }

        public Tetrimino Right { get; private set; }

        // BlockPattern.cs は要らない
        // ここで直接定義するべき
        private static int[][,] patterns = new[]
        {
              // o
              new int[,]{{0,0},{1,0},{0,1},{1,1}},
              new int[,]{{0,0},{1,0},{0,1},{1,1}},
              new int[,]{{0,0},{1,0},{0,1},{1,1}},
              new int[,]{{0,0},{1,0},{0,1},{1,1}},
              // i
              new int[,]{{0,-1},{0,0},{0,1},{0,2}},
              new int[,]{{-1,0},{0,0},{1,0},{2,0}},
              new int[,]{{0,-1},{0,0},{0,1},{0,2}},
              new int[,]{{-1,0},{0,0},{1,0},{2,0}},
              // t
              new int[,]{{-1,0},{0,0},{0,1},{1,0}},
              new int[,]{{0,-1},{0,0},{-1,0},{0,1}},
              new int[,]{{1,0},{0,0},{0,-1},{-1,0}},
              new int[,]{{0,1},{0,0},{1,0},{0,-1}},
              // s
              new int[,]{{0,1},{0,0},{1,0},{-1,1}},
              new int[,]{{-1,-1},{-1,0},{0,0},{0,1}},
              new int[,]{{1,-1},{0,-1},{0,0},{-1,0}},
              new int[,]{{1,1},{1,0},{0,0},{0,-1}},
              // z
              new int[,]{{-1,0},{0,0},{0,1},{1,1}},
              new int[,]{{0,-1},{0,0},{-1,0},{-1,1}},
              new int[,]{{1,0},{0,0},{0,-1},{-1,-1}},
              new int[,]{{0,1},{0,0},{1,0},{1,-1}},
              // l
              new int[,]{{1,1},{0,-1},{0,0},{0,1}},
              new int[,]{{-1,1},{1,0},{0,0},{-1,0}},
              new int[,]{{-1,-1},{0,1},{0,0},{0,-1}},
              new int[,]{{1,-1},{-1,0},{0,0},{1,0}},
              // j
              new int[,]{{-1,0},{0,0},{1,0},{1,1}},
              new int[,]{{0,-1},{0,0},{0,1},{-1,1}},
              new int[,]{{1,0},{0,0},{-1,0},{-1,-1}},
              new int[,]{{0,1},{0,0},{0,-1},{1,-1}}
        };
    }
}
