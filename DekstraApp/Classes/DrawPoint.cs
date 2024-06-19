using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DekstraApp.Classes
{
    public class DrawPoint : DrawFigure, IDraw
    {
        public DrawPoint(int x, int y) : base(x, y)
        {

        }

        public Line Draw()
        {
            Line line = new Line();
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 8;
            line.Stroke = System.Windows.Media.Brushes.Black;
            line.X1 = X;
            line.X2 = X + 4;
            line.Y1 = Y;
            line.Y2 = Y;
            return line;
        }
    }
}
