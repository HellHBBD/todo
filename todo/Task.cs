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

        public Task(string name)
        {
            this.name = name;
        }

        public override bool Equals(object? obj)
        {
            var item = obj as Task;

            if (item == null)
            {
                return false;
            }

            return name.Equals(item.name) && date.Equals(item.date) && description.Equals(item.description) && important.Equals(item.important);
        }
        public override int GetHashCode()
        {
            int hash = 17;

            hash = hash * 23 + (name != null ? name.GetHashCode() : 0);
            hash = hash * 23 + date.GetHashCode();
            hash = hash * 23 + (description != null ? description.GetHashCode() : 0);
            hash = hash * 23 + important.GetHashCode();

            return hash;
        }

    }
}
