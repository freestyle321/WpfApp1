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
        public List<ModelClass> _ModelPartList2 { get; set; }

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
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            ModelPartList1 = CacheData.GetModelPartList1();
            ModelPartList2 = CacheData.GetModelPartList2();
        }
        private void MenuItem_ImportPart1(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItem_ImportPart2(object sender, RoutedEventArgs e)
        {

        }

        public void ImportModel(List<ModelClass> list)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".obj";
            dlg.Multiselect = true;
           
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                List<string> filename = dlg.FileNames.ToList();
                List<string> Safefilename = dlg.SafeFileNames.ToList();
                filename.ForEach(r =>
                {
                if (r.IndexOf(".obj") == -1)
                {
                    MessageBox.Show("导入的文件格式错误！请重新选择!");

                    return;
                }
                else
                {
                        Safefilename.ForEach(p => {
                            if(r.Contains(p))
                            {
                                list.Add(new ModelClass() { File = r,Name=p });
                            }
                        });
                       
                    }
                });
                
               
            }
        }
    }
}
