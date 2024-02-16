using DekstraApp.Views;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DekstraApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainView());
        }

        private void DeveloperMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Мы - команда студентов, которым задали сделать программу по алгоритму Дейкстры. Нам пришлось делать все самим, поэтому мы надеемся, что этой программой поможем Вам избежать данного неприятного момента! P.S. этот пункт нас тоже заставили сделать)", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void HelpMenu_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/MurzinDmitrii/DekstraApp") { UseShellExecute = true });
        }
    }
}