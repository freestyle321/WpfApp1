using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Tool;
using System.Collections.ObjectModel;
using Aspose.ThreeD;
using System.Windows.Input;
using WpfApp1.Command;
using System.IO;

namespace WpfApp1
{
    public class ExportViewModel : NotifyObject
    {
        #region  Proprety
      
        private string _PartOneName { get; set; }

        public string PartOneName
        {
            set
            {
                _PartOneName = value;
                NotifyPropertyChanged("PartOneName");
            }
            get { return this._PartOneName; }
        }

        

        private string _PartTwoName { get; set; }

        public string PartTwoName
        {
            set
            {
                _PartTwoName = value;
                NotifyPropertyChanged("PartTwoName");
            }
            get { return this._PartTwoName; }
        }



        private string _PartOneALLName { get; set; }

        public string PartOneALLName
        {
            set
            {
                _PartOneALLName = value;
             //   NotifyPropertyChanged("PartOneName");
            }
            get { return this._PartOneALLName; }
        }



        private string _PartTwoALLName { get; set; }

        public string PartTwoALLName
        {
            set
            {
                _PartTwoALLName = value;
              //  NotifyPropertyChanged("PartTwoName");
            }
            get { return this._PartTwoALLName; }
        }


        private bool _IsOneModel { get; set; }

        public bool IsOneModel
        {
            set
            {
                _IsOneModel = value;
                NotifyPropertyChanged("IsOneModel");
            }
            get { return this._IsOneModel; }
        }


        private ObservableCollection<string> _FormatList { get; set; }

        public ObservableCollection<string> FormatList
        {
            set
            {
                _FormatList = value;
                NotifyPropertyChanged("FormatList");
            }
            get { return this._FormatList; }
        }

        private string _SelectedForMat { get; set; }

        public string SelectedForMat
        {
            set
            {
                _SelectedForMat = value;
                NotifyPropertyChanged("SelectedForMat");
            }
            get { return this._SelectedForMat; }
        }


        #endregion
        public ExportViewModel()
        {
            FormatList = new ObservableCollection<string>();
            FormatList.Add(FileFormat.Discreet3DS.Extension);
            FormatList.Add(FileFormat.PDF.Extension);
            FormatList.Add(FileFormat.STLASCII.Extension);
            FormatList.Add(FileFormat.STLBinary.Extension);
            FormatList.Add(FileFormat.WavefrontOBJ.Extension);      
        }

        private RelayCommand _ExportComand;

        public ICommand ExportComand
        {
            get
            {
                if (_ExportComand == null)
                {
                    _ExportComand = new RelayCommand(
                        p => ExportComandExec(p),
                        p => CanExportComandComandExec(p));
                }

                return _ExportComand;
            }
        }

        private bool CanExportComandComandExec(object o)
        {
            //if (string.IsNullOrEmpty(PartOneName) || string.IsNullOrEmpty(PartTwoName)|| string.IsNullOrEmpty(SelectedForMat))
            //    return false;
            return true;
        }

        private void ExportComandExec(object o)
        {
            string partOne = GetFileName(PartOneName);
            string partTwo = GetFileName(PartTwoName);
            var file = FileHelper.ProductedDirectory + partOne + "_" + partTwo + SelectedForMat;
            if (!File.Exists(file))
            {
                Task.Run(() => {
                    Scene scene1 = new Scene(PartOneALLName);
                    Scene scene2 = new Scene(PartTwoALLName);
                    if (!IsOneModel)
                    {
                        if (scene2.RootNode.ChildNodes.Count > 0)
                            scene1.RootNode.ChildNodes.ToList().AddRange(scene2.RootNode.ChildNodes);
                    }
                    else
                    {
                        if (scene1.RootNode.ChildNodes.Count > 0&& scene2.RootNode.ChildNodes.Count>0)
                        {
                            for (int i = 0; i < scene2.RootNode.ChildNodes.Count; i++)
                            {
                                scene1.RootNode.ChildNodes[0].Entities.ToList().AddRange(scene2.RootNode.ChildNodes[i].Entities);
                                scene1.RootNode.ChildNodes[0].Materials.ToList().AddRange(scene2.RootNode.ChildNodes[i].Materials);
                            }
                           
                        }
                    }
                     scene1.Save(file, GetFileFormat(SelectedForMat));
                });
            }
        }

        private FileFormat GetFileFormat(string format)
        {
          
            if (format == FileFormat.WavefrontOBJ.Extension)
            {
                return FileFormat.WavefrontOBJ;
            }
            else if (format == FileFormat.STLASCII.Extension)
            {
                return FileFormat.STLASCII;
            }
            else if (format == FileFormat.STLBinary.Extension)
            {
                return FileFormat.STLBinary;
            }
            else if (format == FileFormat.PDF.Extension)
            {
                return FileFormat.PDF;
            }
            else if (format == FileFormat.Discreet3DS.Extension)
            {
                return FileFormat.Discreet3DS;
            }
            else
            {
                return FileFormat.WavefrontOBJ;
            }


        }
        private string GetFileName(string allName)
        {
            int index = allName.IndexOf(".");
            if (index != -1)
                return allName.Substring(0, index);
            else
                return allName;
        }
    }
}
