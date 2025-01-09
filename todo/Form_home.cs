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
    delegate void UpdateFunction();
    public partial class Form_home : Form
    {
        private Form progressForm;
        UpdateFunction update;
        public void defaultUpdate()
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is CheckBox || Controls[i] is Panel)
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
                //checkBox.MouseEnter += CheckBox_MouseEnter;
                //checkBox.MouseLeave += CheckBox_MouseLeave;

                Controls.Add(checkBox);
                taskIndex++;
            }
        }
        public void importantUpdate()
        {
            // 清除現有的 CheckBox 和相關控件
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is CheckBox || Controls[i] is Panel)
                {
                    Controls.RemoveAt(i);
                }
            }

            int initialX = 20;
            int initialY = 30;
            int rowHeight = 30;
            int columnWidth = 200; // 調整列寬度以容納顏色標籤和 CheckBox
            int colorLabelWidth = 20; // 顏色標籤的寬度
            int maxRows = ClientSize.Height / rowHeight - 1;

            int taskIndex = 0;
            var tasks = Program.currentuser.taskList.Values.ToList();
            DateTime maxDate = tasks.Max(t => t.date);
            foreach (var item in tasks)
            {
                item.priority = CalculatePriority(maxDate, item);
            }

            var sortedTasks = Program.currentuser.taskList.Values
                .OrderByDescending(item => item.priority)
                .ThenBy(item => item.name)
                .ToList();

            foreach (var item in sortedTasks)
            {
                int column = taskIndex / maxRows;
                int row = taskIndex % maxRows;

                int offsetX = initialX + column * columnWidth;
                int offsetY = initialY + row * rowHeight;

                // 創建顏色標籤
                Panel colorLabel = new Panel
                {
                    Size = new Size(colorLabelWidth, rowHeight - 5),
                    Location = new Point(offsetX, offsetY),
                    BackColor = GetColorForPriority(item.priority), // 根據索引選擇顏色
                };
                Controls.Add(colorLabel);

                // 創建 CheckBox
                CheckBox checkBox = new CheckBox
                {
                    Text = item.name,
                    Location = new Point(offsetX + colorLabelWidth + 5, offsetY),
                    Checked = item.Checked,
                    AutoSize = true
                };

                // 綁定事件處理程序
                checkBox.MouseDown += Form_home_Checkbox_MouseDown;
                //checkBox.MouseEnter += CheckBox_MouseEnter;
                //checkBox.MouseLeave += CheckBox_MouseLeave;

                Controls.Add(checkBox);
                taskIndex++;
            }
        }


        private Color GetColorForPriority(double priority)
        {
            // 確保 priority 在 0.0 到 1.0 之間
            double clampedPriority = Math.Max(0.0, Math.Min(priority, 1.0));

            // 使用線性插值進行 RGB 分量計算
            int red, green;

            if (clampedPriority  > 0.5)
            {
                // 紅 -> 黃：紅色保持 255，綠色從 0 漸變到 255
                double ratio = (clampedPriority - 0.5) / 0.5; // normalize to [0, 1] for this range
                red = 255;
                green = (int)(255 * (1 - ratio));
            }
            else
            {
                // 黃 -> 綠：紅色從 255 漸變到 0，綠色保持 255
                double ratio = clampedPriority / 0.5; // normalize to [0, 1] for this range
                red = (int)(255 * ratio);
                green = 255;
            }

            return Color.FromArgb(red, green, 0);
        }
        private double CalculatePriority(DateTime maxDate, Task task)
        {
            // 緊急程度標準化 (0 ~ 1)
            double urgency = (double)(maxDate - task.date).TotalDays / (double)(maxDate - DateTime.Now).TotalDays;

            // 重要程度標準化 (0 ~ 1)
            double importance = (double)task.important / 10;

            // 綜合計算分數
            return 0.5 * urgency + 0.5 * importance;
        }
        public Form_home()
        {
            InitializeComponent();
            Text = Program.currentuser.name;
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
            //newTask.percentage = 10;
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
            Form_quadrant quadrant = new Form_quadrant();
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
        //private void CheckBox_MouseEnter(object? sender, EventArgs e)
        //{
        //    // 獲取 CheckBox 的螢幕位置
        //    CheckBox? checkBox = sender as CheckBox;
        //    Point location = checkBox.PointToScreen(new Point(0, checkBox.Height));

        //    InitializeProgressForm(Program.currentuser.taskList[checkBox.Text].percentage);
        //    // 設定小表單位置並顯示
        //    progressForm.Location = location;
        //    progressForm.Show();
        //}

        //private void CheckBox_MouseLeave(object? sender, EventArgs e)
        //{
        //    // 隱藏小表單
        //    progressForm.Hide();
        //}

        private void 以緊急重要程度排序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update = importantUpdate;
            update();
        }

        private void 以新增順序排序預設ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update = defaultUpdate;
            update();
        }
    }
}
