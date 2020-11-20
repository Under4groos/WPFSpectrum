using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpectrum
{
    class Json
    {
        public Configuration Config
        {
            get; set;
        }
        string NameFile
        {
            get; set;
        } = "Data.json";
        public void SaveJson()
        {
            if (Config == null)
                return;
            string output = JsonConvert.SerializeObject(Config);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(NameFile))
            {
                file.WriteLine(output);
            }
        }
        public bool IsVoidFile()
        {
            return File.Exists(NameFile) || Directory.Exists(NameFile);
        }
        public void OpenJson()
        {
            using (FileStream fstream = File.Open(NameFile, FileMode.Open))
            {
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);

                Debug.WriteLine("-----\n\n" + textFromFile + "-------\n\n");

                Config = JsonConvert.DeserializeObject<Configuration>(textFromFile);
            }
        }
       
    }
}
