using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace todo
{
    public partial class Calendar : Form
    {
        private MonthCalendar monthCalendar;
        private ListBox listBoxTasks;
        private User curUser;

        public Calendar(User user)
        {
            InitializeComponent();
            curUser = user;
            Text = "任務月曆";

            monthCalendar = new MonthCalendar
            {
                Dock = DockStyle.Top,
                MaxSelectionCount = 1
            };
            monthCalendar.DateSelected += MonthCalendar_DateSelected;

            listBoxTasks = new CustomListBox
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(listBoxTasks);
            Controls.Add(monthCalendar);

            MarkTaskDates();
        }

        private void MarkTaskDates()
        {
            DateTime[] taskDates = curUser.taskList.Values
                .Where(task => task.date != default(DateTime))
                .Select(task => task.date.Date)
                .Distinct()
                .ToArray();

            monthCalendar.BoldedDates = taskDates;
        }

        private void MonthCalendar_DateSelected(object? sender, DateRangeEventArgs e)
        {
            listBoxTasks.Items.Clear();

            DateTime selectedDate = e.Start.Date;

            var tasksOnSelectedDate = curUser.taskList.Values
                .Where(task => task.date.Date == selectedDate)
                .Select(task => task);

            // 遍歷所有選擇日期的任務，根據完成狀態設置顏色
            foreach (var task in tasksOnSelectedDate)
            {
                string taskText = $"{task.name} (重要度: {task.important})";  // 顯示任務名稱和重要度
                Color textColor = task.Checked ? System.Drawing.Color.Gray : System.Drawing.Color.Black;

                // 標示已完成任務顏色為灰色，未完成任務顏色為黑色
                Color importanceColor = GetImportanceColor(task.important);

                // 每項任務添加顏色塊
                listBoxTasks.Items.Add(new ListBoxItemWithColor(taskText, textColor, importanceColor, task.Checked));
            }

            if (!listBoxTasks.Items.Cast<object>().Any())
            {
                listBoxTasks.Items.Add("此日期沒有任務");
            }
        }

        private Color GetImportanceColor(int importance)
        {
            // 重要度的漸層顏色邏輯
            if (importance >= 5)
            {
                // 漸層從紅色過渡到橙色
                int red = (int)(255);  // 5 到 10 的範圍
                int green = (int)((10 - importance) * 255 / 5);      // 5 到 10 的範圍
                return Color.FromArgb(red, green, 0);  // 返回漸層顏色
            }
            else if (importance >= 1)
            {
                // 漸層從橙色過渡到綠色
                int red = (int)(importance) * (255 / 4);  // 1 到 4 的範圍
                int green = (int)(255);  // 1 到 4 的範圍
                return Color.FromArgb(red, green, 0);  // 返回漸層顏色
            }
            else
            {
                // 返回綠色
                return Color.Green;
            }
        }

    }

    // 用於定義可設置顏色的 ListBox 項目類
    public class ListBoxItemWithColor
    {
        public string Text { get; }
        public System.Drawing.Color TextColor { get; }
        public System.Drawing.Color ImportanceColor { get; }
        public bool IsChecked { get; }

        public ListBoxItemWithColor(string text, System.Drawing.Color textColor, System.Drawing.Color importanceColor, bool isChecked)
        {
            Text = text;
            TextColor = textColor;
            ImportanceColor = importanceColor;
            IsChecked = isChecked;
        }

        public override string ToString() => Text;
    }

    // 用於自定義 ListBox 渲染項目顏色
    public class CustomListBox : ListBox
    {
        public CustomListBox()
        {
            // 啟用項目自定義繪製
            this.DrawMode = DrawMode.OwnerDrawVariable;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.Index < 0) return;

            var item = Items[e.Index] as ListBoxItemWithColor;
            if (item != null)
            {
                // 設置背景顏色
                e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), e.Bounds);

                // 計算顏色區塊顯示位置
                int colorBoxWidth = 20; // 顏色區塊寬度
                Rectangle colorBoxRect = new Rectangle(e.Bounds.Left, e.Bounds.Top, colorBoxWidth, e.Bounds.Height);

                // 繪製顏色區塊
                e.Graphics.FillRectangle(new System.Drawing.SolidBrush(item.ImportanceColor), colorBoxRect);

                // 繪製任務名稱，讓文本區域向右移動以避開顏色區塊
                Rectangle textRect = new Rectangle(e.Bounds.Left + colorBoxWidth, e.Bounds.Top, e.Bounds.Width - colorBoxWidth, e.Bounds.Height);

                // 如果任務已完成，添加槓線
                if (item.IsChecked)
                {
                    e.Graphics.DrawString(item.Text, e.Font, new System.Drawing.SolidBrush(item.TextColor), textRect);
                    e.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Gray, 2), textRect.Left, textRect.Top + textRect.Height / 2, textRect.Right, textRect.Top + textRect.Height / 2); // 這裡畫槓線
                }
                else
                {
                    e.Graphics.DrawString(item.Text, e.Font, new System.Drawing.SolidBrush(item.TextColor), textRect);
                }
            }
        }
    }
}
