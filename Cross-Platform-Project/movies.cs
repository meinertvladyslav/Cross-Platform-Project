using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class Movies
    {
        //variables for json
        [JsonPropertyName("title")]
        public string title { get; set; }
        [JsonPropertyName("year")]
        public int year { get; set; }
        [JsonPropertyName("genre")]
        public string genre { get; set; }
        [JsonPropertyName("director")]
        public string director { get; set; }
        [JsonPropertyName("rating")]
        public double rating { get; set; }
        [JsonPropertyName("emoji")]
        public string emoji { get; set; }
  
    }

}

