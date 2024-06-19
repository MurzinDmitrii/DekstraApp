using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DekstraApp.Classes
{
    internal class DrawLine: DrawFigure, IDraw
    {
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public DrawLine(int x1, int y1, int x2, int y2) : base(x1, y1)
        {
            this.X2 = x2;
            this.Y2 = y2;
        }

        public Line Draw()
        {
            Line line = new Line();
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 4;
            line.Stroke = System.Windows.Media.Brushes.Black;
            line.X1 = X;
            line.X2 = X2;
            line.Y1 = Y;
            line.Y2 = Y2;
            return line;
        }
    }
}
