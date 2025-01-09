using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace todo
{
    enum EditStatus { Add, Modify }
    public partial class Form_edit : Form
    {
        string oldTask;
        public string tempDescrip = "";
        EditStatus status;
        private Form_home formhome;

        /* add new mission */
        public Form_edit(Form_home home)
        {
            InitializeComponent();
            status = EditStatus.Add;
            label_name.Text = "任務名稱：";
            oldTask = textBox_input.Text = "";
            formhome = home;
            comboBox_imortamt.SelectedIndex = 0;
            button_remove.Visible = false;
        }

        /* edit old mission */
        public Form_edit(Form_home home, string task, DateTime date, int important, string description)
        {
            InitializeComponent();
            status = EditStatus.Modify;
            label_name.Text = "新任務名稱：";
            oldTask = textBox_input.Text = task;
            formhome = home;
            dateTimePicker_task.Value = date;
            comboBox_imortamt.SelectedIndex = important - 1;
            tempDescrip = Program.currentuser.taskList[task].description;
            textBox_input.Focus();
            textBox_input.SelectAll();
        }
        private void add()
        {
            if (string.IsNullOrWhiteSpace(textBox_input.Text))
            {
                return;
            }

            string taskName = textBox_input.Text;

            /* check duplicate task */
            if (Program.currentuser.taskList.ContainsKey(taskName))
            {
                MessageBox.Show("任務已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime d = dateTimePicker_task.Value;
            int important = comboBox_imortamt.SelectedIndex + 1;
            formhome.AddCheckBox(taskName, d, important, tempDescrip);
        }
        void modify()
        {
            if (string.IsNullOrWhiteSpace(textBox_input.Text))
            {
                return;
            }
            if (textBox_input.Text != oldTask && Program.currentuser.taskList.ContainsKey(textBox_input.Text))
            {
                MessageBox.Show("任務已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime d = dateTimePicker_task.Value;
            int important = comboBox_imortamt.SelectedIndex + 1;
            /* modify task name */
            Task taskToUpdate = Program.currentuser.taskList[oldTask];
            Program.currentuser.taskList.Remove(oldTask);
            taskToUpdate.name = textBox_input.Text;
            taskToUpdate.date = d;
            taskToUpdate.important = important;
            taskToUpdate.description = tempDescrip; // 更新描述
            Program.currentuser.taskList[textBox_input.Text] = taskToUpdate;
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            if (status == EditStatus.Add)
            {
                add();
            }
            if (status == EditStatus.Modify)
            {
                modify();
            }
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void textBox_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_confirm_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button_cancel_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void button_descrip_Click(object sender, EventArgs e)
        {
            // 傳入目前描述
            Form_description form_Description = new Form_description(tempDescrip);
            form_Description.ShowDialog(); // 顯示描述視窗

            // 視窗關閉後，自動更新描述
            tempDescrip = form_Description.GetDescription();
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            Program.currentuser.taskList.Remove(oldTask);
            Close();
        }

        private void button_link_Click(object sender, EventArgs e)
        {
            Form_link f = new Form_link(oldTask);
            f.ShowDialog();
        }
    }
}
