using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
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
        public  delegate void OnSelectChange() ;
        public  delegate void OnSelectChange1();
        public event OnSelectChange change;
        public event OnSelectChange change1;
        //光源
        //AmbientLight （自然光）
        //DirectionalLight （方向光）
        //PointLight （点光源）
        //SpotLight （聚光源）
        AmbientLight myDirectionalLight = new AmbientLight();

        Model3DGroup myModel3DGroup = new Model3DGroup();
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
                    PartModel3DOne = new WavefrontObjLoader().LoadObjFile(_LeftCurrentModel.File);
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
                    PartModel3DTwo = new WavefrontObjLoader().LoadObjFile(_RightCurrentModel.File);
                    NotifyPropertyChanged("RightCurrentModel");
              
              
            }
            get { return this._RightCurrentModel; }
        }
        public string LastSelectLeftModelName { get; set; }
        public string LastSelectRightModelName { get; set; }
        private ModelVisual3DWithName _PartModel3DOne { get; set; }
        public ModelVisual3DWithName PartModel3DOne
        {
            set
            {
                LastSelectLeftModelName = _PartModel3DOne?.Name;
                _PartModel3DOne = value;
                _PartModel3DOne.Name = LeftCurrentModel.Name;
                myDirectionalLight.Color = Colors.White;
               
                myModel3DGroup.Children.Add(myDirectionalLight);
                _PartModel3DOne.Content = myModel3DGroup;
              if(change!=null)
                    change.Invoke();
                //NotifyPropertyChanged("PartModel3DOne");
            }
            get { return this._PartModel3DOne; }
        }

        private ModelVisual3DWithName _PartModel3DTwo { get; set; }
        public ModelVisual3DWithName PartModel3DTwo
        {
            set
            {
                LastSelectRightModelName = _PartModel3DTwo?.Name;
                _PartModel3DTwo = value;
                myDirectionalLight.Color = Colors.White;
                _PartModel3DTwo.Name = RightCurrentModel.Name;
                myModel3DGroup.Children.Add(myDirectionalLight);
                _PartModel3DTwo.Content = myModel3DGroup;
                if (change1 != null)
                    change1.Invoke();
                //   NotifyPropertyChanged("PartModel3DTwo");
            }
            get { return this._PartModel3DTwo; }
        }



        #endregion
        public MainViewModel()
        {
            FileHelper.CreateDictionarty(); 
            ModelPartList1 = CacheData.GetModelPartList1();
        //    if (ModelPartList1.Count > 0) LeftCurrentModel = ModelPartList1[0];
            ModelPartList2 = CacheData.GetModelPartList2();
         //   if (ModelPartList2.Count > 0) RightCurrentModel = ModelPartList2[0];
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
