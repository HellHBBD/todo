using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
// 這裡指定使用 System.Windows.Forms 的 ToolTip
using System.Windows.Forms;


namespace todo
{
    public partial class Form_quadrant : Form
    {
        private List<Label> taskLabels;
        private ToolTip taskToolTip = new ToolTip();

        public Form_quadrant()
        {
            InitializeComponent();
            taskLabels = new List<Label>();

            this.Text = "任務四象限圖";
            this.Size = new Size(900, 600);

            this.Paint += Form_quadrant_Paint;
            this.Resize += Form_quadrant_Resize;

            DisplayTasks();
        }

        private void Form_quadrant_Paint(object? sender, PaintEventArgs e)
        {
            DrawCoordinateSystem(e.Graphics);
        }

        private void DrawCoordinateSystem(Graphics g)
        {
            g.Clear(Color.White);

            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            int centerX = width / 2;
            int centerY = height / 2;

            g.DrawLine(Pens.Black, 0, centerY, width, centerY);
            g.DrawString("日期", new Font("Arial", 10), Brushes.Black, width - 50, centerY + 5);

            g.DrawLine(Pens.Black, centerX, 0, centerX, height);
            g.DrawString("重要性", new Font("Arial", 10), Brushes.Black, centerX + 5, 5);

            g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.LightGreen)), centerX, 0, width / 2, height / 2); // 第一象限
            g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.LightYellow)), 0, 0, centerX, height / 2); // 第二象限
            g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.LightBlue)), 0, centerY, centerX, height / 2); // 第三象限
            g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.LightCoral)), centerX, centerY, width / 2, height / 2); // 第四象限
        }

        private void DisplayTasks()
        {
            foreach (var label in taskLabels)
            {
                this.Controls.Remove(label);
            }
            taskLabels.Clear();

            if (Program.currentuser.taskList.Count == 0) return;

            var tasks = Program.currentuser.taskList.Values.ToList();
            DateTime minDate = tasks.Min(t => t.date);
            DateTime maxDate = tasks.Max(t => t.date);

            int width = this.ClientSize.Width - 100; 
            int height = this.ClientSize.Height - 70; 
            int centerX = this.ClientSize.Width / 2; 
            int centerY = this.ClientSize.Height / 2; 

            var taskPositionMap = new Dictionary<(DateTime date, int importance), List<Point>>();

            foreach (var task in tasks)
            {
                double totalDays = (maxDate - minDate).TotalDays;
                double taskDays = (task.date - minDate).TotalDays;

                int rawX = centerX - (width / 2) + (int)((taskDays / totalDays) * width);

                int rawY = centerY - (int)(((task.important - 5) / 5.0) * (height / 2));

                int x = Math.Max(50, Math.Min(rawX, this.ClientSize.Width - 50));
                int y = Math.Max(20, Math.Min(rawY, this.ClientSize.Height - 20));

                // 分組偏移處理
                var key = (task.date, task.important);
                if (!taskPositionMap.ContainsKey(key))
                {
                    taskPositionMap[key] = new List<Point> { new Point(x, y) };
                }
                else
                {
                    // 偏移計算：讓新任務圍繞基礎位置排列
                    int offsetIndex = taskPositionMap[key].Count;
                    int offsetX = (offsetIndex % 3 - 1) * 30; 
                    int offsetY = (offsetIndex / 3) * 20; // 每三個向下移

                    // 加入偏移量
                    int adjustedX = x + offsetX;
                    int adjustedY = y + offsetY;

                    // 邊界限制
                    adjustedX = Math.Max(50, Math.Min(adjustedX, this.ClientSize.Width - 50));
                    adjustedY = Math.Max(20, Math.Min(adjustedY, this.ClientSize.Height - 20));

                    taskPositionMap[key].Add(new Point(adjustedX, adjustedY));
                }
            }

            // 創建標籤並顯示
            foreach (var task in tasks)
            {
                var position = taskPositionMap[(task.date, task.important)].First();
                taskPositionMap[(task.date, task.important)].Remove(position);

                Label taskLabel = new Label
                {
                    Text = task.name,
                    AutoSize = true,
                    BackColor = Color.LightBlue,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(2),
                    Cursor = Cursors.Hand,
                    Location = position
                };

                // 標示已完成的任務
                if (task.Checked)
                {
                    taskLabel.BackColor = Color.LightGray;
                    taskLabel.Font = new Font(taskLabel.Font, FontStyle.Strikeout); 
                    taskLabel.ForeColor = Color.DarkGreen; 
                }
                else
                {
                    taskLabel.BackColor = Color.LightBlue;
                    taskLabel.ForeColor = Color.Black;
                }

                // 滑鼠懸停事件
                taskLabel.MouseEnter += (s, e) =>
                {
                    taskToolTip.Show(
                        $"任務名稱: {task.name}\n日期: {task.date.ToShortDateString()}\n重要性: {task.important}\n描述: {task.description}",
                        taskLabel);
                };

                // 滑鼠移開事件
                taskLabel.MouseLeave += (s, e) =>
                {
                    taskToolTip.Hide(taskLabel);
                };

                taskLabels.Add(taskLabel);
                this.Controls.Add(taskLabel);
            }
        }


        private void Form_quadrant_Resize(object? sender, EventArgs e)
        {
            this.Invalidate(); // 重繪坐標系
            DisplayTasks(); 
        }
    }
}
