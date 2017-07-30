using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Tetris.Core.Figures;

namespace Tetris.Core
{
    public class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        public BaseFigure CurrentFigure { get; set; }
        public List<BaseFigure> AllFigures { get; set; }

        /// <summary>
        /// Used for Input. Check if Key is pressed
        /// </summary>
        Dictionary<Keys, KeyValuePair<bool, sbyte>> KeyDictionary;

        System.Timers.Timer timer;
        Stab stab;
        Border bottom;
        Border left;
        Border right;

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

            stab = new Stab();
            CurrentFigure = stab;
            left = new Border(-1, 0, 1, Height);
            right = new Border(Width, 0, 1, Height);
            bottom = new Border(0, Height, Width, 1);
            AllFigures = new List<BaseFigure> { stab, bottom, left, right };

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

            //KeyDictionary.Where(
            //    x => x.Key == Keys.Right || x.Key == Keys.Left).Where(
            //    x => x.Value.Key).Select(
            //    x => x.Value.Value).ToList().ForEach(
            //    x => CurrentFigure.SetRelativePosition(
            //        x > 0 ?
            //        CurrentFigure.Intersect(right) ? 0 : 1 :// stab.X + stab.Width + 1 > Width ? 0 : x :
            //        CurrentFigure.Intersect(left) ? 0 : -1, 0));

            if (KeyDictionary[Keys.Down].Key)
                if (!CurrentFigure.Intersect(bottom))
                    CurrentFigure.SetRelativePosition(0, 1);

            if (frames % 4 == 0)
            {
                if (KeyDictionary[Keys.Up].Key)
                {
                    KeyDictionary[Keys.Up] = new KeyValuePair<bool, sbyte>(false, 0);
                    CurrentFigure.Rotate();
                }
            }

            if (frames % 10 == 0)
                CurrentFigure.SetRelativePosition(0, 1);

            bool intersect = false;
            foreach (var item in AllFigures.Where(x => x != CurrentFigure))
            {
                if (intersect)
                    continue;
                intersect = CurrentFigure.Intersect(item);
            }
            if(!intersect)
                KeyDictionary.Where(
                   x => x.Key == Keys.Right || x.Key == Keys.Left).Where(
                   x => x.Value.Key).Select(
                   x => x.Value.Value).ToList().ForEach(
                   x => CurrentFigure.SetRelativePosition(
                   x > 0 ? intersect ? -1 : 1 : intersect ? 1 : -1, 0));

            while (CurrentFigure.Intersect(bottom))
            {
                CurrentFigure.SetRelativePosition(0, -1);
                CurrentFigure.IsActive = false;
                CurrentFigure = new Stab();
                AllFigures.Add(CurrentFigure);
            }
        }

        public void OnDraw(Graphics graphics)
        {
            AllFigures.ForEach(f => {
                if (!(f is Border))
                    f.Draw(graphics, CellWidth, CellHeight);
            });
        }

        public void MoveBrick(KeyEventArgs keyEventArgs, bool keyUp)
        {
            if (keyEventArgs.KeyCode == Keys.Up && !keyUp)
                return;
            if (KeyDictionary.TryGetValue(keyEventArgs.KeyCode, out var val))
                KeyDictionary[keyEventArgs.KeyCode] = new KeyValuePair<bool, sbyte>(keyUp, val.Value);
        }
    }
}
