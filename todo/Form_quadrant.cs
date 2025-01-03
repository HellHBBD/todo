using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace todo
{
    public partial class Form_quadrant : Form
    {
        private User currentUser;
        private List<Label> taskLabels;

        public Form_quadrant(User user)
        {
            InitializeComponent();
            currentUser = user;
            taskLabels = new List<Label>();

            this.Text = "任務四象限圖";
            this.Size = new Size(900, 600);

            this.Paint += Form_quadrant_Paint;
            this.Resize += Form_quadrant_Resize;

            DisplayTasks();
        }

        private void Form_quadrant_Paint(object sender, PaintEventArgs e)
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

            if (currentUser.taskList.Count == 0) return;

            var tasks = currentUser.taskList.Values.ToList();
            DateTime minDate = tasks.Min(t => t.date);
            DateTime maxDate = tasks.Max(t => t.date);

            // 如果所有日期相同，設置固定範圍
            if (minDate == maxDate) maxDate = minDate.AddDays(1);

            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            int centerX = width / 2;
            int centerY = height / 2;

            foreach (var task in tasks)
            {
                double totalDays = (maxDate - minDate).TotalDays;
                double taskDays = (task.date - minDate).TotalDays;
                int x = centerX + (int)((taskDays / totalDays) * (width / 2));

                int y = centerY - (int)((task.important - 5) / 5.0 * (height / 2));

                Label taskLabel = new Label
                {
                    Text = task.name,
                    Location = new Point(x - 20, y - 10), // 調整中心點
                    AutoSize = true,
                    BackColor = Color.LightBlue,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(2),
                    Cursor = Cursors.Hand
                };

                taskLabel.Click += (s, e) =>
                {
                    MessageBox.Show($"任務名稱: {task.name}\n日期: {task.date.ToShortDateString()}\n重要性: {task.important}\n描述: {task.description}", "任務詳情");
                };

                taskLabels.Add(taskLabel);
                this.Controls.Add(taskLabel);
            }
        }

        private void Form_quadrant_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // 重繪坐標系
            DisplayTasks();    // 更新任務標籤位置
        }
    }
}
