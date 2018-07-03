using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Tool
{
    public class NotifyWindow:Window
    {
        #region INotifyPropertyChanged Members



        public event PropertyChangedEventHandler PropertyChanged;



        // 添加一个触发 PropertyChanged 事件的通用方法

        protected virtual void NotifyPropertyChanged(string propertyName)

        {
            if (PropertyChanged != null)

            {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }

        #endregion
    }
}
