using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace todo
{
    public partial class Form_home : Form
    {
        private Form progressForm;
        string username;
        void updateCheckBox()
        {
            int initialX = 20;
            int initialY = 30;
            int rowHeight = 30;
            int columnWidth = 150;
            int maxRows = ClientSize.Height / rowHeight - 1;

            int taskIndex = 0;


            foreach (var item in Program.currentuser.taskList.Values)
            {
                int column = taskIndex / maxRows;
                int row = taskIndex % maxRows;

                int offsetX = initialX + column * columnWidth;
                int offsetY = initialY + row * rowHeight;

                CheckBox checkBox = new CheckBox
                {
                    Text = item.name,
                    Location = new Point(offsetX, offsetY),
                    Checked = item.Checked,
                    AutoSize = true
                };
                /* bind event handler */
                checkBox.MouseDown += Form_home_Checkbox_MouseDown;
                checkBox.MouseEnter += CheckBox_MouseEnter;
                checkBox.MouseLeave += CheckBox_MouseLeave;

                Controls.Add(checkBox);
                taskIndex++;
            }
        }
        public Form_home(User user)
        {
            InitializeComponent();
            Text = username = user.name;
            updateCheckBox();
        }

        private void 切換使用者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(Program.userList);
            File.WriteAllText("data.json", jsonString, Encoding.UTF8);
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
            if (e.Button == MouseButtons.Left)
            {
                CheckBox? clickedCheckBox = sender as CheckBox;
                if (clickedCheckBox != null)
                {
                    /* update Checked */
                    Program.currentuser.taskList[clickedCheckBox.Text].Checked = !Program.currentuser.taskList[clickedCheckBox.Text].Checked;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                CheckBox? clickedCheckBox = sender as CheckBox;
                if (clickedCheckBox != null)
                {
                    /* store the text */
                    string taskText = clickedCheckBox.Text;
                    DateTime taskDate = Program.currentuser.taskList[taskText].date;
                    int taskImpo = Program.currentuser.taskList[taskText].important;
                    string taskDes = Program.currentuser.taskList[taskText].description;
                    Form_edit form = new Form_edit(this, taskText, taskDate, taskImpo, taskDes);
                    form.ShowDialog();
                }
            }
        }
        public void AddCheckBox(string text, DateTime date, int important, string descrip)
        {
            Task newTask = new Task(text);
            newTask.date = date;
            newTask.important = important;
            newTask.description = descrip;
            //newTask.percentage = 10;
            Program.currentuser.taskList[text] = newTask;
            updateCheckBox();
        }

        private void Form_home_FormClosing(object sender, FormClosingEventArgs e)
        {
            string jsonString = JsonConvert.SerializeObject(Program.userList);
            File.WriteAllText("data.json", jsonString, Encoding.UTF8);
        }

        private void 月曆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calendar calendarForm = new Calendar(Program.currentuser);
            calendarForm.ShowDialog();
        }

        private void 象限圖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_quadrant quadrant = new Form_quadrant(Program.currentuser);
            quadrant.ShowDialog();
        }
        private void InitializeProgressForm(int progressrate)
        {
            // 創建小表單
            progressForm = new Form
            {
                Size = new Size(50, 20),
                FormBorderStyle = FormBorderStyle.None, // 無邊框
                StartPosition = FormStartPosition.Manual, // 手動定位
                TopMost = true, // 保持在最前
                ShowInTaskbar = false, // 不在任務欄顯示
                BackColor = Color.LightGray
            };
            // 添加進度條
            ProgressBar progressBar = new ProgressBar
            {
                //Dock = DockStyle.Top,
                Minimum = 0,
                Maximum = 100,
                Value = progressrate // 假設的進度
            };
            progressForm.Controls.Add(progressBar);
        }
        private void CheckBox_MouseEnter(object? sender, EventArgs e)
        {
            // 獲取 CheckBox 的螢幕位置
            CheckBox? checkBox = sender as CheckBox;
            Point location = checkBox.PointToScreen(new Point(0, checkBox.Height));

            InitializeProgressForm(Program.currentuser.taskList[checkBox.Text].percentage);
            // 設定小表單位置並顯示
            progressForm.Location = location;
            progressForm.Show();
        }

        private void CheckBox_MouseLeave(object? sender, EventArgs e)
        {
            // 隱藏小表單
            progressForm.Hide();
        }
    }
}
