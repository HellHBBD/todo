using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo
{
    [Serializable]
    internal class Task
    {
        public string name;
        //public string description;
        public CheckBox checkBox = new CheckBox();

        public Task(string name)
        {
            this.name = name;
        }
    }
}
