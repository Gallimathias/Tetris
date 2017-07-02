using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;

namespace Tetris.Core
{
    public class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int CellWidth { get; set; }
        public int CellHeight { get; set; }


        Timer timer;

        Brick brick;

        public Game(int width, int height)
        {

            Width = width;
            Height = height;

            brick = new Brick() {
                Width = 2,
                Height = 2
            };


            timer = new Timer() {
                Interval = 750
            };

            timer.Elapsed += Timer_Elapsed;
            
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            brick.SetPosition(brick.X, brick.Y + 3 > Height ? Height - 2 : brick.Y + 1);
            brick.Rotate();
        }

        public void OnDraw(Graphics graphics)
        {
            brick.Draw(graphics, CellWidth, CellHeight);
        }

    }
}
