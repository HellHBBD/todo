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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace todo
{
    delegate void UpdateFunction();
    public partial class Form_home : Form
    {
        private Form? progressForm;
        string username;
        UpdateFunction update;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);

            foreach (Task task in Program.currentuser.taskList.Values)
            {
                Point start = new Point(task.checkBox.Left + task.checkBox.Width, task.checkBox.Top + 14);
                foreach (string item in task.next)
                {
                    CheckBox next = Program.currentuser.taskList[item].checkBox;
                    Point end = new Point(next.Left + 5, next.Top + 14);
                    g.DrawLine(pen, start, end);
                }
            }

            pen.Dispose();
        }
        void defaultUpdate()
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is CheckBox)
                {
                    Controls.RemoveAt(i);
                }
            }

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

        void updateGraph()
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is CheckBox)
                {
                    Controls.RemoveAt(i);
                }
            }

            List<List<CheckBox>> layer = new List<List<CheckBox>>();
            layer.Add(new List<CheckBox>());
            foreach (Task item in Program.currentuser.taskList.Values)
            {
                if (item.prev.Count == 0)
                {
                    item.checkBox = new CheckBox
                    {
                        Text = item.name,
                        Checked = item.Checked,
                        Location = new Point(50, 50),
                        AutoSize = true,
                        BackColor = Color.Transparent,
                    };
                    /* bind event handler */
                    item.checkBox.MouseDown += Form_home_Checkbox_MouseDown;
                    item.checkBox.MouseEnter += CheckBox_MouseEnter;
                    item.checkBox.MouseLeave += CheckBox_MouseLeave;
                    Controls.Add(item.checkBox);

                    layer[0].Add(item.checkBox);
                }
            }
            int maxCount = layer[0].Count;
            for (int i = 0; layer[i].Count > 0; i++)
            {
                layer.Add(new List<CheckBox>());
                foreach (CheckBox task in layer[i])
                {
                    foreach (string item in Program.currentuser.taskList[task.Text].next)
                    {
                        Task temp = Program.currentuser.taskList[item];
                        temp.checkBox = new CheckBox
                        {
                            Text = temp.name,
                            Checked = temp.Checked,
                            Location = new Point(100, 100),
                            AutoSize = true,
                            BackColor = Color.Transparent,
                        };
                        /* bind event handler */
                        temp.checkBox.MouseDown += Form_home_Checkbox_MouseDown;
                        temp.checkBox.MouseEnter += CheckBox_MouseEnter;
                        temp.checkBox.MouseLeave += CheckBox_MouseLeave;
                        Controls.Add(temp.checkBox);

                        layer[i + 1].Add(temp.checkBox);
                    }
                }
                if (layer[i + 1].Count > maxCount)
                {
                    maxCount = layer[i + 1].Count;
                }
            }

            //check duplicate
            HashSet<string> record = new HashSet<string>();
            for (int i = layer.Count-1; i >= 0; i--)
            {
                HashSet<CheckBox> remove = new HashSet<CheckBox>();
                for (int j = 0; j < layer[i].Count; j++)
                {
                    if (record.Contains(layer[i][j].Text))
                    {
                        remove.Add(layer[i][j]);
                    }
                    else
                    {
                        record.Add(layer[i][j].Text);
                    }
                }
                foreach (CheckBox item in remove)
                {
                    layer[i].Remove(item);
                    Controls.Remove(item);
                }
            }

            int initialX = 20;
            int maxWidth = maxCount * 27 + (maxCount - 1) * 40;
            for (int i = 0; i < layer.Count; i++)
            {
                int m = layer[i].Count;
                int offsetX = initialX + 150 * i;
                int padding = (maxWidth - m * 27) / (m + 1);
                int initialY = 30 + padding;
                for (int j = 0; j < m; j++)
                {
                    int offsetY = initialY + (27 + padding) * j;
                    layer[i][j].Location = new Point(offsetX, offsetY);
                }
            }
            Invalidate();
        }

        public Form_home(User user)
        {
            InitializeComponent();
            Text = username = user.name;
            update = defaultUpdate;
            update();
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
                    update();
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
                    update();
                }
            }
        }
        public void AddCheckBox(string text, DateTime date, int important, string descrip)
        {
            Task newTask = new Task(text);
            newTask.date = date;
            newTask.important = important;
            newTask.description = descrip;
            newTask.percentage = 10;
            Program.currentuser.taskList[text] = newTask;
            update();
        }

        private void Form_home_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO
            //string jsonString = JsonConvert.SerializeObject(Program.userList);
            //File.WriteAllText("data.json", jsonString, Encoding.UTF8);
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
            //MessageBox.Show(Program.currentuser.taskList[checkBox.Text].percentage.ToString());
            progressForm.Controls.Add(progressBar);
        }
        private void CheckBox_MouseEnter(object? sender, EventArgs e)
        {
            // 獲取 CheckBox 的螢幕位置
            CheckBox? checkBox = sender as CheckBox;
            if (checkBox != null && progressForm != null)
            {
                Point location = checkBox.PointToScreen(new Point(0, checkBox.Height));

                InitializeProgressForm(Program.currentuser.taskList[checkBox.Text].percentage);
                // 設定小表單位置並顯示
                progressForm.Location = location;
                progressForm.Show();
            }
        }

        private void CheckBox_MouseLeave(object? sender, EventArgs e)
        {
            if (progressForm != null)
            {
                // 隱藏小表單
                progressForm.Hide();
            }
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is CheckBox)
                {
                    Controls.RemoveAt(i);
                }
            }
        }

        private void button_update_Click(object sender, EventArgs e)
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

        private void 順序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update = updateGraph;
            update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is CheckBox)
                {
                    Controls.RemoveAt(i);
                }
            }
        }
    }
}
