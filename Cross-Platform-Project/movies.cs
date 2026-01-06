using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cross_Platform_Project
{
    public class Movies
    {
        //variables for json
        [JsonPropertyName("title")]
        public string title { get; set; }
        [JsonPropertyName("year")]
        public int year { get; set; }
        [JsonPropertyName("genre")]
        public List<string> genre { get; set; }
        [JsonPropertyName("director")]
        public string director { get; set; }
        [JsonPropertyName("rating")]
        public double rating { get; set; }
        [JsonPropertyName("emoji")]
        public string emoji { get; set; }

        //genre has problem loading properly, so its helping the data to show properly, only genre had this problem, because it is an array
        [JsonIgnore]
        public string GenreText => string.Join(", ", genre);

        [JsonIgnore]
        //id for history feature, so I could keep track of the movies and work with them
        public string Id => $"{title}_{year}";

        
    }

}

