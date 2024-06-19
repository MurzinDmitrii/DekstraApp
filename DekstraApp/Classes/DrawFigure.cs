using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DekstraApp.Classes
{
    abstract public class DrawFigure
    {
        public int X {  get; set; }
        public int Y {  get; set; }

        public DrawFigure(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
