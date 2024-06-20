using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DekstraApp.Classes
{
    internal interface IDraw
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Line Draw();
    }
}
