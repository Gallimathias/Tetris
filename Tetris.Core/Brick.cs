using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Core
{
    public class Brick
    {
        public int Width { get; set; }
        public int Height { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }

        public Brick()
        {
        }
        
        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void SetRelativePosition(int x, int y)
        {
            X += x;
            Y += y;
        }
        public void Rotate() { }

        public void Draw(Graphics graphics, int cellWidth, int cellHeight)
        {
            using (var brush = new SolidBrush(Color.Red))
            {
                graphics.FillRectangle(brush, 
                    new Rectangle(cellWidth * X, cellHeight * Y, cellWidth * Width, cellHeight * Height));
            }
        }
    }
}
