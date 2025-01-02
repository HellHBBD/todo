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
        public Form_home(User user)
        {
            InitializeComponent();
            Text = username = user.name;
        }

        private void 切換使用者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* change mainForm to Form_login and exit Form_home */
            Program.mainForm = new Form_login();
            Close();
        }

        private void 查看月曆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calendar calendarForm = new Calendar(Program.currentuser);
            calendarForm.ShowDialog();
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
            int initialX = 20;
            int initialY = 30;
            int rowHeight = 30;
            int columnWidth = 150;
            int maxRows = ClientSize.Height / rowHeight - 1;


            int taskIndex = Program.currentuser.taskList.Count;
            int column = taskIndex / maxRows;
            int row = taskIndex % maxRows;

            int offsetX = initialX + column * columnWidth;
            int offsetY = initialY + row * rowHeight;

            CheckBox checkBox = new CheckBox
            {
                Text = text,
                Location = new Point(offsetX, offsetY),
                AutoSize = true
            };
            Task newTask = new Task(text);
            newTask.TaskCheckBox = checkBox;
            Program.currentuser.taskList[text] = newTask;
            /* bind event handler */
            checkBox.MouseDown += Form_home_Checkbox_MouseDown;
            Controls.Add(checkBox);
        }

        
    }
}
