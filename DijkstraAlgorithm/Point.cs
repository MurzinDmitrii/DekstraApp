using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DekstraAlgorithm
{
    /// <summary>
    /// Класс, реализующий вершину графа
    /// </summary>
    public class Point
    {
        public double ValueMetka { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public Point predPoint { get; set; }
        public object SomeObj { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public Point(int value, bool ischecked)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            predPoint = new Point();
        }
        public Point(int value, bool ischecked, string name)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            Name = name;
            predPoint = new Point();
        }
        public Point()
        {
        }
    }
}
