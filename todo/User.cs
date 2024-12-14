using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo
{
    [Serializable]
    internal class User
    {
        public string name;
        //Dictionary<string, CheckBox> taskList = new Dictionary<string, CheckBox>();

        public User(string name) {
            this.name = name;
        }
    }
}
