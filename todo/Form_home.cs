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
    public partial class Form_home : Form
    {
        string username;
        void updateListBox()
        {
            foreach (var item in Program.currentuser.taskList)
            {
                Controls.Add(item.Value.TaskCheckBox);
            }
        }
        public Form_home(User user)
        {
            InitializeComponent();
            Text = username = user.name;
            updateListBox();
        }

        private void 切換使用者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* change mainForm to Form_login and exit Form_home */
            Program.mainForm = new Form_login();
            Close();
        }

        private void Form_home_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                /* check if user click on the blank area */
                var clickedControl = GetChildAtPoint(e.Location);
                if (clickedControl == null)
                {
                    Form_edit form = new Form_edit(this);
                    form.ShowDialog();
                }
            }
        }

        private void Form_home_Checkbox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CheckBox? clickedCheckBox = sender as CheckBox;
                if (clickedCheckBox != null)
                {
                    /* store the text */
                    string taskText = clickedCheckBox.Text;
                    Form_edit form = new Form_edit(this, taskText);
                    form.ShowDialog();
                }
            }
        }
        public void AddCheckBox(string text)
        {
            /* add checkBox in runtime */
            int offsetY = 100 + Program.currentuser.Count() * 30;
            string taskName = text;

            CheckBox checkBox = new CheckBox
            {
                Text = text,
                Location = new Point(200, offsetY),
                AutoSize = true
            };
            Task newTask = new Task(taskName);
            newTask.TaskCheckBox = checkBox;
            Program.currentuser.taskList[taskName] = newTask;
            /* bind event handler */
            checkBox.MouseDown += Form_home_Checkbox_MouseDown;
            Controls.Add(checkBox);
        }
    }
}
