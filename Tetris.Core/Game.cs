using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace Tetris.Core
{
    public class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int CellWidth { get; set; }
        public int CellHeight { get; set; }

        Dictionary<Keys, KeyValuePair<bool, sbyte>> KeyDictionary;

        System.Timers.Timer timer;
        Brick brick;

        ulong frames = 0;

        public Game(int width, int height)
        {
            Width = width;
            Height = height;

            KeyDictionary = new Dictionary<Keys, KeyValuePair<bool, sbyte>>()
            {
                [Keys.Down] = new KeyValuePair<bool, sbyte>(false, 0),
                [Keys.Left] = new KeyValuePair<bool, sbyte>(false, -1),
                [Keys.Right] = new KeyValuePair<bool, sbyte>(false, 1),
                [Keys.Up] = new KeyValuePair<bool, sbyte>(false, 0)
            };

            brick = new Brick()
            {
                Width = 2,
                Height = 2
            };
            
            timer = new System.Timers.Timer()
            {
                Interval = 33
            };

            timer.Elapsed += Timer_Elapsed;

            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            frames++;
            if(frames % 10 == 0)
                brick.SetRelativePosition(0, brick.Y + 3 > Height ? 0 : 1);
            KeyDictionary.Where(x => x.Value.Key).Select(x => x.Value.Value).ToList().ForEach(x => brick.SetRelativePosition(x, 0));
        }

        public void OnDraw(Graphics graphics)
        {
            brick.Draw(graphics, CellWidth, CellHeight);
        }

        public void MoveBrick(KeyEventArgs keyEventArgs, bool keyUp)
        {
            if (KeyDictionary.TryGetValue(keyEventArgs.KeyCode, out var val))
                KeyDictionary[keyEventArgs.KeyCode] = new KeyValuePair<bool, sbyte>(keyUp, val.Value);
        }
    }
}
