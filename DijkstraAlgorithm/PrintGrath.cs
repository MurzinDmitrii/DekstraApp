using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DekstraAlgorithm
{
    /// <summary>
    /// для печати графа
    /// </summary>
    public static class PrintGrath
    {
        public static List<string> PrintAllPoints(DekstraAlgorim da)
        {
            List<string> retListOfPoints = new List<string>();
            foreach (Point p in da.points)
            {
                string pred = "нет предка";
                if(p.predPoint != null)
                    pred = p.predPoint.Name;
                retListOfPoints.Add(string.Format("Точка={0}, вес={1}, предок={2}", p.Name, p.ValueMetka, pred));
            }
            return retListOfPoints;
        }
        public static List<string> PrintAllMinPaths(DekstraAlgorim da)
        {
            List<string> retListOfPointsAndPaths = new List<string>();
            foreach (Point p in da.points)
            {

                double d = 0.0;
                if (p != da.BeginPoint)
                {
                    string s = string.Empty;
                    foreach (Point p1 in da.MinPath1(p))
                    {
                        s += string.Format("{0}-", p1.Name);
                    }
                    d += p.ValueMetka;
                    retListOfPointsAndPaths.Add(string.Format("Для точки {0} минимальный путь из точки {1} = {3}{2}(путь = {4})", p.Name, da.BeginPoint.Name, Reverse(s), da.BeginPoint.Name, d));
                }

            }
            return retListOfPointsAndPaths;

        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
