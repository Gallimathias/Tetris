using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Tetris.Core.Figures
{
    public abstract class BaseFigure
    {
        public int Width => Bricks.Max(b => b.X) - Bricks.Min(b => b.X);
        public int Height => Bricks.Max(b => b.Y) - Bricks.Min(b => b.Y);
        public bool IsActive { get; set; }

        protected Brick RotationBrick;

        protected List<Brick> Bricks;
        
        public BaseFigure()
        {
            Bricks = new List<Brick>();
            IsActive = true;
        }

        public abstract void Rotate();

        public void Draw(Graphics graphics, int cellWidth, int cellHeight) =>
           Bricks.ForEach(b => b.Draw(graphics, cellWidth, cellHeight));

        public void SetRelativePosition(int x, int y) =>
            Bricks.ForEach(b => b.SetRelativePosition(x, y));

        public virtual bool Intersect(int x, int y) => !Bricks.TrueForAll(b => !b.Intersect(x, y));
        public virtual bool Intersect(Brick brick) => !Bricks.TrueForAll(b => !b.Intersect(brick.X, brick.Y));

        public virtual bool Intersect(BaseFigure figure) => !figure.Bricks.TrueForAll(b => !Intersect(b));

    }
}