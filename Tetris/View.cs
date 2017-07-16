using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Core;

namespace Tetris
{
    public partial class View : UserControl
    {
        public int CellCountX => 10;
        public int CellCountY => 20;

        public int CellWidth => Width / CellCountX;
        public int CellHeight => Height / CellCountY;

        Timer timer;
        Game game;

        public View()
        {
            game = new Game(10, 20)
            {
                CellHeight = CellHeight,
                CellWidth = CellWidth
            };

            InitializeComponent();
            
            timer = new Timer()
            {
                Interval = 16
            };

            PreviewKeyDown += (s, e) => e.IsInputKey = true;
            KeyDown += (s, e) => game.MoveBrick(e, true);
            KeyUp += (s, e) => game.MoveBrick(e, false);

            timer.Tick += (s, e) => Invalidate();
            timer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();

            game.CellHeight = CellHeight;
            game.CellWidth = CellWidth;

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            using (var pen = new Pen(new SolidBrush(Color.Black)))
            {
                for (int x = 0; x < CellCountX; x++)
                {
                    for (int y = 0; y < CellCountY; y++)
                    {
                        e.Graphics.DrawRectangle(pen, new Rectangle(
                            x * CellWidth, y * CellHeight, CellWidth, CellHeight));
                    }
                }
            }

            game.OnDraw(e.Graphics);

            base.OnPaint(e);
        }

    }
}
