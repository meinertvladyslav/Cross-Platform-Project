using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Platform_Project
{
    public class Account
    {
        //account have their own history and favorite
        public string Name { get; set; }
        public List<Movies> History { get; set; } = new();
        public List<Movies> Favorite { get; set; } = new();
    }
}