using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApp1.Tool;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        MainViewModel model = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
          
          
            //right.DataContext = ModelPartList2;
            this.Closed += MainWindow_Closed;
            this.DataContext = model;
          //  this.DataContext = this;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void MenuItem_ImportPart1(object sender, RoutedEventArgs e)
        {
         
        }
        private void MenuItem_ImportPart2(object sender, RoutedEventArgs e)
        {
            ImportModel imp = new ImportModel(model.ModelPartList2);
            imp.ShowDialog();
            model.ModelPartList2 = imp.DataBaseList;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //var aa= Rhino.FileIO.File3dm.Read("");
                
        }

        private void _3DViewList_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
