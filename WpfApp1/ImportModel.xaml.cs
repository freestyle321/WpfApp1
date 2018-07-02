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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// ImportModel.xaml 的交互逻辑
    /// </summary>
    public partial class ImportModel : Window
    {
        #region  Proprety
        private List<ModelClass> _DataBaseList { get; set; }


        public List<ModelClass> DataBaseList
        {
            set
            {
                _DataBaseList = value;

            }
            get { return this._DataBaseList; }
        }


        public ModelClass ImportModel1 { get; set; }



        #endregion


        public ImportModel()
        {
            InitializeComponent();
            ImportModel1 = new ModelClass();
        }
        public ImportModel(List<ModelClass> list) : base()
        {
            DataBaseList = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filter = "(*.obj)|*.obj";
            string fileName = string.Empty;
           string filePath= GetFilePath(out fileName, filter);
            if(!string.IsNullOrEmpty(fileName)&& !string.IsNullOrEmpty(filePath))
            {
                ImportModel1.File = filePath;
                if(Name.Text==string.Empty)
                    ImportModel1.Name = fileName;
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string filter = "(*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif";
            string fileName = string.Empty;
            string filePath = GetFilePath(out fileName, filter);
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(filePath))
            {
                ImportModel1.Image = filePath;
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //ImportModel1.Name = Name.Text;
           
            if (!string.IsNullOrEmpty(ImportModel1.Name) && !string.IsNullOrEmpty(ImportModel1.File))
            {
                DataBaseList.Add(ImportModel1);
            }
            this.Close();
        }

        private string GetFilePath(out string FileName, string Filter)
        {
            FileName = string.Empty;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = Filter;
            dlg.Multiselect = false;

            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                FileName = dlg.SafeFileName;
                return dlg.FileName;
            }

            return string.Empty;
        }


    }


}

