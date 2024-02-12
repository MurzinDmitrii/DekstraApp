using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DekstraAlgorithm
{
    /// <summary>
    /// Класс, реализующий ребро
    /// </summary>
    public class Rebro
    {
        public Point FirstPoint { get; private set; }
        public Point SecondPoint { get; private set; }
        public double Weight { get; private set; }

        public Rebro(Point first, Point second, double valueOfWeight)
        {
            FirstPoint = first;
            SecondPoint = second;
            Weight = valueOfWeight;
        }

    }
}
