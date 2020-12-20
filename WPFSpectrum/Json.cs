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
        } = "Configuration.json";
        public async void SaveJson()
        {
            if (Config == null)
                return;
          
            string output = JsonConvert.SerializeObject(Config);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(NameFile))
            {


                await file.WriteAsync(output);

                string date = DateTime.Now.ToLocalTime().ToString();
                Debug.WriteLine($"[{date}] Сохранение конфигурации.\n => {NameFile}");
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
                fstream.Read(array, 0, array.Length);
                
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                    
                Config = JsonConvert.DeserializeObject<Configuration>(textFromFile);
                string date = DateTime.Now.ToLocalTime().ToString();
                Debug.WriteLine($"[Config {date}] Open last config\n");
            }
        }
       
    }
}
