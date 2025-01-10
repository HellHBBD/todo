using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace todo
{
    [Serializable]
    public class Task
    {
        public string name;
        public DateTime date;
        public string description = "";
        public bool Checked;
        public int important;
        public int percentage = 10; // cP/pP
        // public int progressPoint
        // public int completePoint
        public HashSet<string> prev = new HashSet<string>();
        public HashSet<string> next = new HashSet<string>();

        [JsonIgnore]
        public CheckBox checkBox = new CheckBox();

        public Task(string name)
        {
            this.name = name;
        }
    }
}
