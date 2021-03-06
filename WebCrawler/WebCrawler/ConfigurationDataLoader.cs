﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    class ConfigurationDataLoader
    {
        public Config LoadGonfiguration()
        {
            Config result;
            using (StreamReader streamReader = new StreamReader("config.json"))
            {
                using (JsonReader reader = new JsonTextReader(streamReader))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    result = serializer.Deserialize<Config>(reader);
                }
            }
            if(result.MaxDepth > 5)
            {
                result.MaxDepth = 5;
            }
            return result;
        }
    }
}
