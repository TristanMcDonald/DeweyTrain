using DeweyTrain.Views;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeweyTrain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Initializing pages to be displayed in the content frame
        ReplacingBooksPage replacingBooksPage = new ReplacingBooksPage();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReplacingBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = replacingBooksPage;
        }
    }
}
