using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Tool;

namespace WpfApp1
{

    public static class CacheData
    {
        public static List<string> ProductedFileList { get; private set; }

        private static List<ModelClass> ModelPartList1 { get;  set; }

        private static List<ModelClass> ModelPartList2 { get;  set; }
      
        public static List<string> GetProductedFileList()
        {
            if (ProductedFileList == null) ProductedFileList = new List<string>();
            var result = FileHelper.GetTxtFileContent(FileHelper.ProductedDirectory + "\\producted.txt");
            if (result != null)
            {
                ProductedFileList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result[0]);
            }
            return ProductedFileList;
        }
        public static List<ModelClass> GetModelPartList1()
        {

            if (ModelPartList1 == null) ModelPartList1 = new List<ModelClass>();
            var result = FileHelper.GetTxtFileContent(FileHelper.Part1Directory + "\\part1.txt");
            if (result != null)
            {
                ModelPartList1 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelClass>>(result[0]);
            }
            return ModelPartList1;
        }

        public static List<ModelClass> GetModelPartList2()
        {
            if (ModelPartList2 == null) ModelPartList2 = new List<ModelClass>();
            var result = FileHelper.GetTxtFileContent(FileHelper.Part2Directory + "\\part2.txt");
            if (result != null)
            {
                ModelPartList2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ModelClass>>(result[0]);
            }
            return ModelPartList2;
        }

        public static void WriteModelPart()
        {

        }
         
    }
}
