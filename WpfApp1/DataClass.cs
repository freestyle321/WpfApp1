using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ModelClass
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _Name;
        public string  Name
        {
            set {
                _Name = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
            get { return this._Name; }
        }

        private string _File;
        public string File
        {
            set
            {
                _File = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("File"));
            }
            get { return this._File; }
        }

        private string _Image;
        public string Image
        {
            set
            {
                _Image = value;
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Image"));
            }
            get { return this._Image; }
        }
    }

   
 
}
