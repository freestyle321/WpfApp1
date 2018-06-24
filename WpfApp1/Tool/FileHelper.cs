﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Tool
{
    public class FileHelper
    {
        private static string fileDirectory = System.IO.Directory.GetCurrentDirectory();

        public static string Part1Directory;
        public static string Part2Directory;
        public static string ProductedDirectory;
        public FileHelper()
        {
            Part1Directory = fileDirectory + "\\Data\\Part1";
            Part2Directory = fileDirectory + "\\Data\\Part2";
            ProductedDirectory = fileDirectory + "\\Data\\Producted";
            CreateDictionarty(Part1Directory);
            CreateDictionarty(Part2Directory);
            CreateDictionarty(Part2Directory);
        }

        /// <summary>
        /// 获取指定路径下所有文件
        /// </summary>        
        /// 
        public static List<string> GetFiles(string path)
        {
            //using System.IO;
            if (!Directory.Exists(path))  //路径不存在
                return null;
            List<string> result = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] subfolder = dir.GetDirectories();
            if (subfolder != null && subfolder.Length > 0)
            {
                foreach (DirectoryInfo item in subfolder)
                {
                    List<string> subFiles = GetFiles(item.FullName);  //FullName:完整路径
                    if (subFiles != null && subFiles.Count > 0)
                        result.AddRange(subFiles);
                }
            }
            FileInfo[] files = dir.GetFiles();
            if (files != null)
            {
                foreach (FileInfo file in files)
                {
                    result.Add(file.FullName);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取指定文件内容
        /// 注：这里适用于读取文本类型文件
        /// </summary>
        public static List<string> GetTxtFileContent(string fileName)
        {
            if (!File.Exists(fileName)) //文件不存在
                return null;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs, Encoding.Default);
            List<string> result = new List<string>();
            string line = "";
            while ((line = reader.ReadLine()) != null)
            {
                result.Add(line);
            }
            reader.Close();
            fs.Close();
            return result;
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>        
        public static void DeleteFile(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件全路径</param>
        /// <param name="content">写入内容</param>
        public static void CreateFile(string fileName, List<string> content = null)
        {
            if (!File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                if (content != null && content.Count > 0)
                {
                    StreamWriter sw = new StreamWriter(fs);
                    foreach (string line in content)
                    {
                        sw.WriteLine(line);
                    }
                    sw.Close();
                }
                fs.Close();
            }
        }

        public static void CreateDictionarty(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }
    }
}