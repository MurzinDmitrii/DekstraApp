using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DekstraApp.Classes
{
    internal class myCanvasSize
    {
        public int MyCanvasWidth { get { return (x1 + 10) * 10; } }
        public int MyCanvasHeight { get { return (y1 + 10) * 10;  } }
        public int x1 { get; set; }
        public int y1 { get; set; }
    }
}
