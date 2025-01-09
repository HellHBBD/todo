using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todo
{
    //public enum LinkStatus { Prev, Next }
    public partial class Form_link : Form
    {
        User currentUser = Program.currentuser;
        HashSet<string> prev_temp = new HashSet<string>();
        HashSet<string> next_temp = new HashSet<string>();
        string currentTask = "";
        //LinkStatus status;
        public Form_link(string currentTask)
        {
            InitializeComponent();
            this.currentTask = currentTask;

            foreach (string item in currentUser.taskList.Keys)
            {
                if (item == currentTask)
                {
                    ; // exclude currentTask itself
                }
                else if (currentUser.taskList[currentTask].prev.Contains(item))
                {
                    prev_temp.Add(item);
                    listBox_prev.Items.Add(item);
                }
                else if (currentUser.taskList[currentTask].next.Contains(item))
                {
                    next_temp.Add(item);
                    listBox_next.Items.Add(item);
                }
                else
                {
                    listBox_available.Items.Add(item);
                }
            }
        }

        private void button_left_Click(object sender, EventArgs e)
        {
            string? task = null;
            /* available -> prev */
            if (listBox_available.SelectedItem != null && (task = listBox_available.SelectedItem.ToString()) != null)
            {
                listBox_available.Items.Remove(task);

                prev_temp.Add(task);
                listBox_prev.Items.Add(task);
                listBox_prev.SelectedItem = task;
            }
            /* next -> available */
            else if (listBox_next.SelectedItem != null && (task = listBox_next.SelectedItem.ToString()) != null)
            {
                // TODO check cycle

                next_temp.Remove(task);
                listBox_next.Items.Remove(task);

                listBox_available.Items.Add(task);
                listBox_available.SelectedItem = task;
            }
        }

        private void button_right_Click(object sender, EventArgs e)
        {
            string? task = null;
            /* prev -> available */
            if (listBox_prev.SelectedItem != null && (task = listBox_prev.SelectedItem.ToString()) != null)
            {
                prev_temp.Remove(task);
                listBox_prev.Items.Remove(task);

                listBox_available.Items.Add(task);
                listBox_available.SelectedItem = task;
            }
            /* available -> next */
            else if (listBox_available.SelectedItem != null && (task = listBox_available.SelectedItem.ToString()) != null)
            {
                // TODO check cycle

                listBox_available.Items.Remove(task);

                next_temp.Add(task);
                listBox_next.Items.Add(task);
                listBox_next.SelectedItem = task;
            }
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            currentUser.taskList[currentTask].prev = prev_temp;
            foreach (string task in prev_temp)
            {
                currentUser.taskList[task].next.Add(currentTask);
            }
            currentUser.taskList[currentTask].next = next_temp;
            foreach (string task in next_temp)
            {
                currentUser.taskList[task].prev.Add(currentTask);
            }
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox_prev_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox_prev.SelectedIndex;
            listBox_available.SelectedIndex = -1;
            listBox_next.SelectedIndex = -1;
            listBox_prev.SelectedIndex = index;
        }

        private void listBox_available_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox_available.SelectedIndex;
            listBox_prev.SelectedIndex = -1;
            listBox_next.SelectedIndex = -1;
            listBox_available.SelectedIndex = index;
        }

        private void listBox_next_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox_next.SelectedIndex;
            listBox_prev.SelectedIndex = -1;
            listBox_available.SelectedIndex = -1;
            listBox_next.SelectedIndex = index;
        }
    }
}
