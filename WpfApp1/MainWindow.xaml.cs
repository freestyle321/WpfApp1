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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region  Proprety
        private List<ModelClass> _ModelPartList1 { get; set; }


        public List<ModelClass> ModelPartList1
        {
            set
            {
                _ModelPartList1 = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("ModelPartList1"));
            }
            get { return this._ModelPartList1; }
        }
        private List<ModelClass> _ModelPartList2 { get; set; }

        public List<ModelClass> ModelPartList2
        {
            set
            {
                _ModelPartList2 = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("ModelPartList2"));
            }
            get { return this._ModelPartList2; }
        }

        private ModelClass _LeftCurrentModel { get; set; }

        public ModelClass LeftCurrentModel
        {
            set
            {
                _LeftCurrentModel = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("LeftCurrentModel"));
            }
            get { return this._LeftCurrentModel; }
        }


        private ModelClass _RightCurrentModel { get; set; }
        public ModelClass RightCurrentModel
        {
            set
            {
                _RightCurrentModel = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("RightCurrentModel"));
            }
            get { return this._LeftCurrentModel; }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            ModelPartList1 = CacheData.GetModelPartList1();
            ModelPartList2 = CacheData.GetModelPartList2();
            left.DataContext = ModelPartList1;
            left.DataContext = ModelPartList2;
            this.Closed += MainWindow_Closed;
            this.DataContext = this;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void MenuItem_ImportPart1(object sender, RoutedEventArgs e)
        {
            ImportModel imp = new ImportModel(ModelPartList1);
            imp.ShowDialog();
       
                
        }
        private void MenuItem_ImportPart2(object sender, RoutedEventArgs e)
        {
            ImportModel imp = new ImportModel(ModelPartList2);
            imp.ShowDialog();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var aa= Rhino.FileIO.File3dm.Read("");
                
        }

        private void _3DViewList_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
