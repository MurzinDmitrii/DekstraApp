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
using System.Text.RegularExpressions;

namespace DekstraApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void TextBlock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            var text = tb.Text.Insert(tb.CaretIndex, e.Text);

            Regex _numMatch = new Regex(@"^((?:[0-9]\d*)|(\d+\.)|(?:(?=[\d.]+)(?:[0-9]\d*|0)\.\d{0,2}))$");
            e.Handled = !Regex.IsMatch(text, _numMatch.ToString());
        }
    }
}
