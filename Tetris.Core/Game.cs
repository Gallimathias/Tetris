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
        public List<Type> BlockTypes { get; set; }

        /// <summary>
        /// Used for Input. Check if Key is pressed
        /// </summary>
        Dictionary<Keys, KeyValuePair<bool, sbyte>> KeyDictionary;

        System.Timers.Timer timer;
        BaseFigure stab;
        Border bottom;
        Border left;
        Border right;
        Random random;

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

            stab = new Tee();
            CurrentFigure = stab;
            left = new Border(-1, 0, 1, Height);
            right = new Border(Width, 0, 1, Height);
            bottom = new Border(0, Height, Width, 1);
            AllFigures = new List<BaseFigure> { stab, bottom, left, right };
            BlockTypes = new List<Type> {typeof(Square), typeof(Stab), typeof(Tee) };
            random = new Random();

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

            if (frames % 4 == 0)
            {
                if (KeyDictionary[Keys.Up].Key)
                {
                    KeyDictionary[Keys.Up] = new KeyValuePair<bool, sbyte>(false, 0);
                    TryRotate(CurrentFigure);
                }
            }

            KeyDictionary.Where(
               x => x.Key == Keys.Right || x.Key == Keys.Left).Where(
               x => x.Value.Key).Select(
               x => x.Value.Value).ToList().ForEach(
               x => TryMove(
               x < 0 ? -1 : 1, 0, CurrentFigure));

            if (KeyDictionary[Keys.Down].Key || frames % 10 == 0)
                if (!TryMove(0, 1, CurrentFigure))
                {
                    CurrentFigure.IsActive = false;
                    CurrentFigure = (BaseFigure)Activator.CreateInstance(BlockTypes[random.Next(0, BlockTypes.Count)]);
                    if(Collision(CurrentFigure))
                        AllFigures.RemoveAll(x=> !(x is Border));
                    AllFigures.Add(CurrentFigure);
                }
        }

        public void OnDraw(Graphics graphics)
        {
            AllFigures.ForEach(f =>
            {
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

        public bool Collision(BaseFigure figure) => AllFigures.Where(f => f != CurrentFigure).Any(b => CurrentFigure.Intersect(b));

        public bool TryMove(int x, int y, BaseFigure figure)
        {
            figure.Move(x, y);

            if (!Collision(figure))
                return true;

            figure.Move(-x, -y);
            return false;
        }

        public bool TryRotate(BaseFigure figure)
        {
            figure.Rotate();

            if (!Collision(figure))
                return true;

            figure.CounterRotate();
            return false;
        }
    }
}
