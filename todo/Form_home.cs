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
        private Form? progressForm;
        string username = "";
        UpdateFunction update;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (update == updateGraph)
            {
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
            else
            {
                e.Graphics.Clear(BackColor);
            }

        }
        void defaultUpdate()
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

                Controls.Add(checkBox);
                taskIndex++;
            }
            Invalidate();
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
            for (int i = layer.Count - 1; i >= 0; i--)
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

            int initialX = 30;
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

                Controls.Add(checkBox);
                taskIndex++;
            }
            Invalidate();
        }


        private Color GetColorForPriority(double priority)
        {
            // 確保 priority 在 0.0 到 1.0 之間
            double clampedPriority = Math.Max(0.0, Math.Min(priority, 1.0));

            // 使用線性插值進行 RGB 分量計算
            int red, green;

            if (clampedPriority > 0.5)
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
            Text = "使用者(" + Program.currentuser.name + ")";
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
        private void Form_home_MouseDoubleClick(object sender, MouseEventArgs e)
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
        private void 存檔ToolStripMenuItem_Click(object sender, EventArgs e)
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
            Form_quadrant quadrant = new Form_quadrant();
            quadrant.ShowDialog();
        }

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

                Controls.Add(checkBox);
                taskIndex++;
            }
        }

        private void 順序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update = updateGraph;
            update();
        }
    }
}
