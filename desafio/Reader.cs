using desafio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace desafio
{
    public class Reader
    {
        public List<CarrouselCulture> ReadFile()
        {
            using (StreamReader reader = new StreamReader("cultura.json"))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<CarrouselCulture>>(json);
            }
        }
    }
}
