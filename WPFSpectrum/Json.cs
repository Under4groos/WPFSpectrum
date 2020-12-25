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

        private int lastupdate = 0;

        public bool isOpen
        {
            get; set;
        } = false;

        public async void SaveJson()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(NameFile))
            {
                string output = JsonConvert.SerializeObject(Config);
                await file.WriteAsync(output);
                string date = DateTime.Now.ToLocalTime().ToString();
                Debug.WriteLine($"[{date}] Сохранение конфигурации.\n => {NameFile}");
            }
        }
        public bool IsVoidFile()
        {
            return File.Exists(NameFile) || Directory.Exists(NameFile);
        }
        public bool isUpdateFile()
        {
            try
            {
                if (IsVoidFile())
                {
                    int Millisecond = File.GetLastWriteTime(NameFile).Millisecond;
                    if (lastupdate != Millisecond)
                    {
                        lastupdate = Millisecond;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return isUpdateFile();
            }
            
            
        }
        
        public void OpenJson()
        {
            try
            {
                using (FileStream fstream = File.Open(NameFile, FileMode.Open))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);

                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                    string date = DateTime.Now.ToLocalTime().ToString();
                    try
                    {
                        Config = JsonConvert.DeserializeObject<Configuration>(textFromFile);

                        Debug.WriteLine($"[Config {date}] Открытие файла конфигурации\n");
                        isOpen = true;
                    }
                    catch
                    {
                        Debug.WriteLine($"[Config {date}] Ошибка открытия файла конфигурации\n");
                        isOpen = false;
                    }


                }
            }
            catch (Exception)
            {
                OpenJson();               
            }
            
        }     
    }
}
