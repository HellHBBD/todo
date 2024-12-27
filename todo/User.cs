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
        public Dictionary<string, Task> taskList = new Dictionary<string, Task>();

        public User(string name)
        {
            this.name = name;
        }

        public void rename(string newName)
        {
            name = newName;
        }

        public int Count()
        {
            return taskList.Count;
        }
    }
}
