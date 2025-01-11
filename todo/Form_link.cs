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
        HashSet<string> prev_temp;
        HashSet<string> next_temp;
        string currentTask = "";
        //LinkStatus status;
        public Form_link(string currentTask)
        {
            InitializeComponent();
            Text = "任務先後順序";
            this.currentTask = currentTask;
            prev_temp = currentUser.taskList[currentTask].prev;
            next_temp = currentUser.taskList[currentTask].next;

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

        bool checkP(HashSet<string> record, string start)
        {
            foreach (string item in currentUser.taskList[start].prev)
            {
                if (record.Contains(item))
                {
                    return true;
                }
                record.Add(item);
                if (checkP(record, item))
                {
                    return true;
                }
                record.Remove(item);
            }
            return false;
        }

        /*bool checkPrevCycle(string start)*/
        /*{*/
        /*    HashSet<string> record = new HashSet<string>();*/
        /*    return checkP(record, start);*/
        /*}*/

        bool checkN(HashSet<string> record, string start)
        {
            foreach (string item in currentUser.taskList[start].next)
            {
                if (record.Contains(item))
                {
                    return true;
                }
                record.Add(item);
                if (checkN(record, item))
                {
                    return true;
                }
                record.Remove(item);
            }
            return false;
        }

        /*bool checkNextCycle(string start)*/
        /*{*/
        /*    HashSet<string> record = new HashSet<string>();*/
        /*    return checkN(record, start);*/
        /*}*/

        private void button_left_Click(object sender, EventArgs e)
        {
            string? task = null;
            /* available -> prev */
            if (listBox_available.SelectedItem != null && (task = listBox_available.SelectedItem.ToString()) != null)
            {
                prev_temp.Add(task);

                HashSet<string> record = new HashSet<string>();
                record.Add(currentTask);
                if (checkP(record, task))
                {
                    prev_temp.Remove(task);
                    MessageBox.Show("先後關係出現循環", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    currentUser.taskList[task].next.Add(currentTask);

                    listBox_prev.Items.Add(task);
                    listBox_prev.SelectedItem = task;

                    listBox_available.Items.Remove(task);
                }
            }
            /* next -> available */
            else if (listBox_next.SelectedItem != null && (task = listBox_next.SelectedItem.ToString()) != null)
            {
                next_temp.Remove(task);
                currentUser.taskList[task].prev.Remove(currentTask);
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
                currentUser.taskList[task].next.Remove(currentTask);
                listBox_prev.Items.Remove(task);

                listBox_available.Items.Add(task);
                listBox_available.SelectedItem = task;
            }
            /* available -> next */
            else if (listBox_available.SelectedItem != null && (task = listBox_available.SelectedItem.ToString()) != null)
            {
                next_temp.Add(task);
                HashSet<string> record = new HashSet<string>();
                record.Add(currentTask);
                if (checkN(record, task))
                {
                    next_temp.Remove(task);
                    MessageBox.Show("先後關係出現循環", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    currentUser.taskList[task].prev.Add(currentTask);

                    listBox_available.Items.Remove(task);

                    listBox_next.Items.Add(task);
                    listBox_next.SelectedItem = task;
                }
            }
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
