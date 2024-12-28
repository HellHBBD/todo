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

        private void Form_home_Load(object sender, EventArgs e)
        {

        }

        private void Form_home_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 確認是否點擊在空白區域
                var clickedControl = GetChildAtPoint(e.Location);
                if (clickedControl == null) // 空白區域
                {
                    Form_edit form = new Form_edit(this);
                    form.ShowDialog();
                }
            }
        }

        private void Form_home_Checkbox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CheckBox clickedCheckBox = sender as CheckBox;
                if (clickedCheckBox != null)
                {
                    // 儲存文字
                    string taskText = clickedCheckBox.Text;
                    Form_edit form = new Form_edit(this,taskText);
                    form.ShowDialog();
                }
            }
        }
        public void AddCheckBox(string text)
        {
            // 動態新增 CheckBox
            int offsetY = 100 + Program.currentuser.Count() * 30;
            string taskName = text;

            CheckBox checkBox = new CheckBox
            {
                Text = text,
                Location = new Point(200, offsetY),
                AutoSize = true
            };
            Task newTask = new Task(taskName);
            // 綁定事件（可選）
            //checkBox.CheckedChanged += newTask.OnCheckBoxChanged;

            newTask.TaskCheckBox = checkBox;
            Program.currentuser.taskList[taskName] = newTask;
            checkBox.MouseDown += Form_home_Checkbox_MouseDown;
            this.Controls.Add(checkBox);
        }
    }
}
