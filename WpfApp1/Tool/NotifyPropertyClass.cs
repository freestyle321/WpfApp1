using System;
using System.ComponentModel;
using Rhino;


namespace WpfApp1.Tool
{
    public partial class NotifyObject : INotifyPropertyChanged
    {


        #region INotifyPropertyChanged Members



        public event PropertyChangedEventHandler PropertyChanged;

      
        // 添加一个触发 PropertyChanged 事件的通用方法

        public virtual void NotifyPropertyChanged(string propertyName)

        {
            if (PropertyChanged != null)

            {

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            }
        }

        #endregion
    }

}
