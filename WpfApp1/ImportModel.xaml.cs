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
   
        #endregion


        public ImportModel()
        {
            InitializeComponent();           
        }
        public ImportModel(List<ModelClass>  list) :base()
        {
            DataBaseList = list;
        }
    }
}
