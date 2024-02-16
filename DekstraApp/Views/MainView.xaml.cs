using DekstraApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DekstraAlgorithm;
using Point = DekstraAlgorithm.Point;
using Microsoft.Win32;

namespace DekstraApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        int name = 0;
        myCanvasSize myCanvasSize;
        DekstraAlgorithm.Point selectedPoint = null;
        public MainView()
        {
            InitializeComponent();
            myCanvasSize = new myCanvasSize
            {
                x1 = 0,
                y1 = 0
            };
            MyCanvasPanel.DataContext = myCanvasSize;
        }

        public void DrawLine(int x1, int x2, int y1, int y2)
        {
            Line line = new Line();
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 4;
            line.Stroke = System.Windows.Media.Brushes.Black;
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            MyCanvas.Children.Add(line);
        }

        public void DrawPoint(int x, int y)
        {
            Line line = new Line();
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 8;
            line.Stroke = System.Windows.Media.Brushes.Black;
            line.X1 = x-4;
            line.X2 = x+4;
            line.Y1 = y;
            line.Y2 = y;
            
            MyCanvas.Children.Add(line);
            MyCanvasPanel.DataContext = myCanvasSize;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DekstraAlgorithm.Point point = new ();
            if (Convert.ToInt32(xBox.Text) > 200 || Convert.ToInt32(yBox.Text) > 200)
            {
                MessageBox.Show("Слишком большие данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            point.x = Convert.ToInt32(xBox.Text);
            point.y = Convert.ToInt32(yBox.Text);
            point.Name = name.ToString();
            PointsList.Items.Add (point);
            DrawPoint(point.x, point.y);
            Text(point);
            name++;
        }

        private void DekstraButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DekstraAlgorithm.Point> points = new List<DekstraAlgorithm.Point>();
                foreach (DekstraAlgorithm.Point item in PointsList.Items)
                {
                    points.Add(item);
                }
                List<DekstraAlgorithm.Rebro> edges = new List<DekstraAlgorithm.Rebro>();
                foreach (DekstraAlgorithm.Rebro item in EdgesList.Items)
                {
                    edges.Add(item);
                }

                DekstraAlgorim dekstraAlgorim = new DekstraAlgorim(points.ToArray(), edges.ToArray());
                dekstraAlgorim.BeginPoint = points[Convert.ToInt32(BeginPointBox.Text)];

                foreach (var item in dekstraAlgorim.points)
                {
                    if (item != dekstraAlgorim.BeginPoint)
                    {
                        item.ValueMetka = int.MaxValue;
                    }
                }

                dekstraAlgorim.AlgoritmRun(dekstraAlgorim.BeginPoint);
                List<string> printPoints = PrintGrath.PrintAllPoints(dekstraAlgorim);
                List<string> printPaths = PrintGrath.PrintAllMinPaths(dekstraAlgorim);
                string otvet = "";

                foreach (var item in printPoints)
                {
                    otvet += item + "\n";
                }
                foreach (var item in printPaths)
                {
                    otvet += item + "\n";
                }

                MessageBox.Show(otvet);
                WorkWithWord.SavePhoto(MyCanvas);
                WorkWithWord.PrintPhoto(otvet);
            }
            catch
            {
                MessageBox.Show("Выберите корректную стартовую точку!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Text(Point p)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = p.Name;
            textBlock.FontSize = 8;
            textBlock.Foreground = new SolidColorBrush(Colors.DarkGoldenrod);
            Canvas.SetLeft(textBlock, p.x-2);
            Canvas.SetTop(textBlock, p.y-6);
            MyCanvas.Children.Add(textBlock);
        }

        private void PrintSize(Rebro rebro)
        {
            TextBlock textBlock = new TextBlock();
            var a = Math.Round(rebro.Weight, 2);
            textBlock.Text = a.ToString();
            textBlock.FontSize = 8;
            textBlock.Foreground = new SolidColorBrush(Colors.DarkGoldenrod);
            Canvas.SetLeft(textBlock, (rebro.FirstPoint.x + rebro.SecondPoint.x)/2);
            Canvas.SetTop(textBlock, (rebro.FirstPoint.y + rebro.SecondPoint.y)/2);
            MyCanvas.Children.Add(textBlock);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();

                MyCanvas.Children.Clear();
                PointsList.Items.Clear();
                EdgesList.Items.Clear();

                var alg = WorkWithExcel.Import(openFileDialog.FileName);
                foreach (var item in alg.points.ToList())
                {
                    PointsList.Items.Add(item);
                }
                foreach (var item in alg.rebra.ToList())
                {
                    EdgesList.Items.Add(item);
                }
                name = PointsList.Items.Count;
                Load();
            }
            catch
            {
                MessageBox.Show("Выберите корректный файл!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            var itemSendet = (MenuItem)sender;
            var itemDel = itemSendet.DataContext as DekstraAlgorithm.Point;
            PointsList.Items.Remove(itemDel);

            List<Rebro> list = new List<Rebro>();
            foreach (var item in EdgesList.Items)
            {
                Rebro rebro = item as Rebro;
                list.Add(rebro);
            }

            foreach (var item in list)
            {
                Rebro rebro = item as Rebro;
                if(rebro.FirstPoint.Name == itemDel.Name || rebro.SecondPoint.Name == itemDel.Name)
                {
                    EdgesList.Items.Remove(item);
                }
            }

            Load();
        }

        private void ChooseMenu_Click(object sender, RoutedEventArgs e)
        {
            var itemSendet = (MenuItem)sender;
            selectedPoint = itemSendet.DataContext as DekstraAlgorithm.Point;
        }

        private void JoinMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Rebro> list = new List<Rebro>();
                foreach (var item in EdgesList.Items)
                {
                    Rebro rebro1 = item as Rebro;
                    list.Add(rebro1);
                }
                var itemSendet = (MenuItem)sender;
                var selectedPoint2 = itemSendet.DataContext as DekstraAlgorithm.Point;
                double len = Math.Sqrt(Math.Pow((selectedPoint.x - selectedPoint2.x), 2) + Math.Pow((selectedPoint.y - selectedPoint2.y), 2));
                Rebro rebro = new(selectedPoint, selectedPoint2, len);

                if (list.FirstOrDefault(c => c.FirstPoint == rebro.FirstPoint && c.SecondPoint == rebro.SecondPoint) != null)
                {
                    return;
                }

                Line line = new();
                line.Visibility = System.Windows.Visibility.Visible;
                line.StrokeThickness = 1;
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.X1 = selectedPoint.x;
                line.X2 = selectedPoint2.x;
                line.Y1 = selectedPoint.y;
                line.Y2 = selectedPoint2.y;
                MyCanvas.Children.Add(line);
                EdgesList.Items.Add(rebro);
                PrintSize(rebro);
                foreach (var item in PointsList.Items)
                {
                    Point a = item as Point;
                    Text(a);
                }
            }
            catch
            {
                MessageBox.Show("Выберите корректное ребро!","Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Load()
        {
            MyCanvas.Children.Clear();
            foreach (DekstraAlgorithm.Point item in PointsList.Items)
            {
                Line line = new Line();
                line.Visibility = System.Windows.Visibility.Visible;
                line.StrokeThickness = 8;
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.X1 = item.x - 4;
                line.X2 = item.x + 4;
                line.Y1 = item.y;
                line.Y2 = item.y;
                MyCanvas.Children.Add(line);
                Text(item);

                if (myCanvasSize.x1 < line.X2)
                {
                    myCanvasSize.x1 = Convert.ToInt32(line.X2);
                }
                if (myCanvasSize.y1 < line.Y2)
                {
                    myCanvasSize.y1 = Convert.ToInt32(line.Y2);
                }

                if (myCanvasSize.x1 > myCanvasSize.y1)
                {
                    MyCanvasPanel.Width = myCanvasSize.x1;
                }
                else
                {
                    MyCanvasPanel.Width = myCanvasSize.y1;
                }
            }
            foreach (DekstraAlgorithm.Rebro item in EdgesList.Items)
            {
                Line line = new();
                line.Visibility = System.Windows.Visibility.Visible;
                line.StrokeThickness = 1;
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.X1 = item.FirstPoint.x;
                line.X2 = item.SecondPoint.x;
                line.Y1 = item.FirstPoint.y;
                line.Y2 = item.SecondPoint.y;
                MyCanvas.Children.Add(line);
                PrintSize(item);
            }
        }

        private void DeleteEdgeMenu_Click(object sender, RoutedEventArgs e)
        {
            var itemSendet = (MenuItem)sender;
            var itemDel = itemSendet.DataContext as DekstraAlgorithm.Rebro;
            EdgesList.Items.Remove(itemDel);

            Load();
        }

        private void ChangeMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itemSendet = (MenuItem)sender;
                var itemChange = itemSendet.DataContext as DekstraAlgorithm.Rebro;
                itemChange.Weight = Convert.ToInt32(ChangeBox.Text);
                Load();
            }
            catch
            {
                MessageBox.Show("Введите длину!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBlock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
