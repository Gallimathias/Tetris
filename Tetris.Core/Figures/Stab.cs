using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core.Figures
{
    public class Stab : BaseFigure
    {
        public Stab()
        {
            for (int i = 0; i < 4; i++)
            {
                var b = new Brick { X = 0, Y = i };
                Bricks.Add(b);
                if (i == 1)
                    RotationBrick = b;
            }
        }

        public override void Rotate()
        {
            if(Bricks.GroupBy(x=>x.Y).ToList().Count > 1)
            {
                for (int i = -1; i < 3; i++)
                {
                    Bricks[i+1].Y = RotationBrick.Y;
                    Bricks[i+1].X = RotationBrick.X + i;
                }
            }
            else
            {
                for (int i = -1; i < 3; i++)
                {
                    Bricks[i + 1].Y = RotationBrick.Y + i;
                    Bricks[i + 1].X = RotationBrick.X;
                }
            }
        }
        public override void CounterRotate() => Rotate();
        
    }
}
