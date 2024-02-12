using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using Worksheet = Aspose.Cells.Worksheet;
using DekstraAlgorithm;

namespace DekstraApp.Classes
{
    internal class WorkWithExcel
    {
        public static DekstraAlgorim Import(string path)
        {
            Aspose.Cells.Workbook wb = new (path);

            var page = wb.Worksheets[0];
            List<Point> points = new List<Point>();
            List<Rebro> rebros = new List<Rebro>();
            bool t = false;
            int index = 0;

            for (int i = 0; i < page.Cells.Rows.Count; i++)
            {
                string a = page.Cells.Rows[i].FirstCell.Value.ToString();
                if (a == "Edges")
                {
                    t = true;
                    continue;
                }

                if (!t)
                {
                    Point point = new Point();
                    point.x = Convert.ToInt32(page.Cells.Rows[i].FirstCell.Value);
                    point.y = Convert.ToInt32(page.Cells.Rows[i].LastCell.Value);
                    point.Name = index.ToString();
                    index++;
                    points.Add(point);
                }
                else
                {
                    Point fPoint = points.FirstOrDefault(c => c.Name == page.Cells.Rows[i].FirstCell.Value.ToString());
                    Point sPoint = points.FirstOrDefault(c => c.Name == page.Cells.Rows[i].LastCell.Value.ToString());
                    Rebro rebro = new Rebro(fPoint, sPoint, Math.Sqrt(Math.Pow((fPoint.x - sPoint.x), 2) + Math.Pow((fPoint.y - sPoint.y), 2)));
                    rebros.Add(rebro);
                }
            }

            DekstraAlgorim alg = new DekstraAlgorim(points.ToArray(), rebros.ToArray());
            return alg;
        }

        private static DataTable GetConstraints(Worksheet w1)
        {
            DataTable constr = new DataTable();

            var headers = w1.Cells.Rows[0];
            foreach (Aspose.Cells.Cell cell in headers)
            {
                constr.Columns.Add(cell.StringValue);
            }

            for (int i = 1; i < w1.Cells.Rows.Count; i++)
            {
                List<object> objValues = new List<object>();
                foreach (Aspose.Cells.Cell cell in w1.Cells.Rows[i])
                {
                    objValues.Add(cell.StringValue);
                }
                constr.Rows.Add(objValues.ToArray());
            }

            return constr;
        }

        private static bool IsMax(Worksheet w1) =>
            w1.Cells.Rows[2][0].StringValue.ToLower() == "max";

        private static DataTable GetFunction(Worksheet w1)
        {
            DataTable func = new DataTable();

            var headers = w1.Cells.Rows[0];
            foreach (Aspose.Cells.Cell cell in headers)
            {
                func.Columns.Add(cell.StringValue);
            }

            var values = w1.Cells.Rows[1];
            List<object> objValues = new List<object>();
            foreach (Aspose.Cells.Cell cell in values)
            {
                objValues.Add(cell.StringValue);
            }
            func.Rows.Add(objValues.ToArray());
            return func;
        }
    }
}
