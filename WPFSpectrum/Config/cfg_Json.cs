using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSpectrum.Config
{
    class cfg_Json
    {
        // ---------------------------------------------------------
        List<string> Errors = new List<string>();
        public string path = "cfg.json";
        private string TEXT_ALL = "";
        public bool isOpen
        {
            get; set;
        } = false;
        public Configuration Config;
        cfg_file file_ = new cfg_file();
        // ---------------------------------------------------------
        public cfg_Json(string name)
        {
            file_.file = path = name;    
        }
        public cfg_Json()
        {
            file_.file = path;    
        }
        public void Serialize()
        {
            if (Config.Equals(null))
                throw new ArgumentException("Config is null!");
            if (!file_.isExists())
                throw new ArgumentException("Файла блять нету!");
            try
            {
                
                TEXT_ALL = JsonConvert.SerializeObject(Config);
                file_.Write(TEXT_ALL);
            }
            catch (Exception e)
            {

                throw new ArgumentException("Вообще ошибка!");
            }
            
        }
        public override string ToString()
        {
            return TEXT_ALL;
        }
        public void Deserialize()
        {
            if(!file_.isExists())
                throw new ArgumentException("Файла блять нету!");
            try
            {
                TEXT_ALL = file_.Read();
                Config = Config.Equals(null) ? new Configuration() : JsonConvert.DeserializeObject<Configuration>(TEXT_ALL);
            }
            catch (Exception)
            {

                throw new ArgumentException("Вообще ошибка!");
            }
            
        }

        // ---------------------------------------------------------

    }
}
