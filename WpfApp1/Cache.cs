using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Tool;

namespace WpfApp1
{

    public static class CacheData
    {
        public static ObservableCollection<string> ProductedFileList { get; private set; }

        private static ObservableCollection<ModelClass> ModelPartList1 { get; set; }

        private static ObservableCollection<ModelClass> ModelPartList2 { get; set; }

        public static ObservableCollection<string> GetProductedFileList()
        {
            if (ProductedFileList == null) ProductedFileList = new ObservableCollection<string>();
            var result = FileHelper.GetTxtFileContent(FileHelper.ProductedDirectory + "\\producted.txt");
            if (result != null)
            {
                ProductedFileList = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<string>>(result[0]);
            }
            return ProductedFileList;
        }
        public static ObservableCollection<ModelClass> GetModelPartList1()
        {

            if (ModelPartList1 == null) ModelPartList1 = new ObservableCollection<ModelClass>();
            var result = FileHelper.GetTxtFileContent(FileHelper.Part1Directory + "\\part1.txt");
            string list = string.Empty;
            result.ForEach(r =>
            {
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelClass>(r);
                ModelPartList1.Add(model);
            });
    
            return ModelPartList1;
        }

        public static ObservableCollection<ModelClass> GetModelPartList2()
        {
            if (ModelPartList2 == null) ModelPartList2 = new ObservableCollection<ModelClass>();
            var result = FileHelper.GetTxtFileContent(FileHelper.Part2Directory + "\\part2.txt");
            string list = string.Empty;
            result.ForEach(r =>
            {
               var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelClass>(r);
                ModelPartList2.Add(model);
            });
     
            return ModelPartList2;
        }

        public static void WriteModelPart()
        {
            //List<string> list1 = new List<string>();
            //List<string> list2 = new List<string>();
            //ModelPartList1.ForEach(r => {
            //    var model =Newtonsoft.Json.JsonConvert.SerializeObject(r);
            //    list1.Add(model);
            //});
            //ModelPartList2.ForEach(r => {
            //    var model = Newtonsoft.Json.JsonConvert.SerializeObject(r);
            //    list2.Add(model);
            //});

            //FileHelper.CreateFile(FileHelper.Part1Directory + "\\part1.txt", list1);
            //FileHelper.CreateFile(FileHelper.Part1Directory + "\\part1.txt", list2);
        }
         
    }
}
