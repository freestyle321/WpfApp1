using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Tool;

namespace WpfApp1
{
    public class ModelClass: NotifyObject
    {
     
        private string _Name;
        public string  Name
        {
            set {
                _Name = value;

                NotifyPropertyChanged("Name");
            }
            get { return this._Name; }
        }

        private string _File;
        public string File
        {
            set
            {
                _File = value;
                NotifyPropertyChanged("File");
            }
            get { return this._File; }
        }

        private string _Image;
        public string Image
        {
            set
            {
                _Image = value;
                NotifyPropertyChanged("Image");
            }
            get { return this._Image; }
        }
    }

   
 
}
