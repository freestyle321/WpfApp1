using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Command;
using WpfApp1.Tool;

namespace WpfApp1
{
    public class MainViewModel: NotifyObject
    {
        #region  Proprety
        private ObservableCollection<ModelClass> _ModelPartList1 { get; set; }
        FileHelper FileHelper = new FileHelper();
        CacheData CacheData = new CacheData();
        public ObservableCollection<ModelClass> ModelPartList1
        {
            set
            {
                _ModelPartList1 = value;

                NotifyPropertyChanged("ModelPartList1");

            }
            get { return this._ModelPartList1; }
        }
        private ObservableCollection<ModelClass> _ModelPartList2 { get; set; }

        public ObservableCollection<ModelClass> ModelPartList2
        {
            set
            {
                _ModelPartList2 = value;
                NotifyPropertyChanged("ModelPartList2");
            }
            get { return this._ModelPartList2; }
        }

        private ModelClass _LeftCurrentModel { get; set; }

        public ModelClass LeftCurrentModel
        {
            set
            {
                _LeftCurrentModel = value;
                NotifyPropertyChanged("LeftCurrentModel");
            }
            get { return this._LeftCurrentModel; }
        }


        private ModelClass _RightCurrentModel { get; set; }
        public ModelClass RightCurrentModel
        {
            set
            {
                _RightCurrentModel = value;
                NotifyPropertyChanged("RightCurrentModel");
            }
            get { return this._LeftCurrentModel; }
        }

        private ModelVisual3DWithName _PartModel3DOne { get; set; }
        public ModelVisual3DWithName PartModel3DOne
        {
            set
            {
                _PartModel3DOne = value;
                NotifyPropertyChanged("PartModel3DOne");
            }
            get { return this._PartModel3DOne; }
        }

        private ModelVisual3DWithName _PartModel3DTwo { get; set; }
        public ModelVisual3DWithName PartModel3DTwo
        {
            set
            {
                _PartModel3DTwo = value;
                NotifyPropertyChanged("PartModel3DTwo");
            }
            get { return this._PartModel3DTwo; }
        }

        #endregion
        public MainViewModel()
        {
            FileHelper.CreateDictionarty(); 
            ModelPartList1 = CacheData.GetModelPartList1();
            ModelPartList2 = CacheData.GetModelPartList2();
        }

        private RelayCommand _ImportPart1Comand;

        public ICommand ImportPart1Comand
        {
            get
            {
                if (_ImportPart1Comand == null)
                {
                    _ImportPart1Comand = new RelayCommand(
                        p => ImportPart1ComandExec(p),
                        p => CanImportPart1ComandExec(p));
                }

                return _ImportPart1Comand;
            }
        }

        private bool CanImportPart1ComandExec(object o)
        {

            return true;
        }

        private void ImportPart1ComandExec(object o)
        {
            ImportModel imp = new ImportModel(ModelPartList1);
            imp.ShowDialog();
            NotifyPropertyChanged("ModelPartList1");


        }
    }
}
