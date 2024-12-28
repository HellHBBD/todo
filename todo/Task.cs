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
        //public string description;
        public CheckBox TaskCheckBox = new CheckBox();

        public Task(string name)
        {
            this.name = name;
        }
        /*
        public void OnCheckBoxChanged(object sender, EventArgs e)
        {
            if (TaskCheckBox != null)
            {
                Console.WriteLine($"Task '{name}' is {(TaskCheckBox.Checked ? "completed" : "not completed")}");
            }
        }
        */
    }
}
