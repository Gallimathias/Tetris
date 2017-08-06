using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core.Figures
{
    public class Tee : BaseFigure
    {
        public Brick this[int x, int y]
        {
            get
            {
                var originX = RotationBrick.X - 1;
                var originY = RotationBrick.Y - 1;

                return Bricks.FirstOrDefault(o => o.X == x + originX && o.Y == y + originY);
            }
        }
        Brick a;
        Brick b;
        Brick c;
        Brick d;

        public Tee()
        {
            var rotBrick = new Brick { X = 1, Y = 1 };

            var b = new List<Brick> {
                new Brick { X = 0, Y = 1 },
                new Brick { X = 2, Y = 1 },
                new Brick { X = 1, Y = 0 },
                rotBrick, };
            Bricks.AddRange(b);
            RotationBrick = rotBrick;

        }

        public override void Rotate()
        {
            /*
            [0,1] (x+1 y+1)=> [1,2]
            [1,0] (x-1 y+1)=> [0,1]
            [2,1] (x-1 y-1)=> [1,0]
            [1,2] (x+1 y-1)=> [2,1]
            */
            a = this[0, 1];
            b = this[1, 0];
            c = this[2, 1];
            d = this[1, 2];
            if (a != null)
                a.SetRelativePosition(1, 1);
            if (b != null)
                b.SetRelativePosition(-1, 1);
            if (c != null)
                c.SetRelativePosition(-1, -1);
            if (d != null)
                d.SetRelativePosition(1, -1);
        }

        public override void CounterRotate()
        {
            for (int i = 0; i < 3; i++)
                Rotate();
        }

    }
}
