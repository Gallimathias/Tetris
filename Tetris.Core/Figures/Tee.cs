using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core.Figures
{
    public class Tee : BaseFigure
    {
       
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

       
    }
}
