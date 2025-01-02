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

            monthCalendar = new MonthCalendar
            {
                Dock = DockStyle.Top,
                MaxSelectionCount = 1
            };
            monthCalendar.DateSelected += MonthCalendar_DateSelected;

            listBoxTasks = new ListBox
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
                .Select(task => task.name);

            listBoxTasks.Items.AddRange(tasksOnSelectedDate.ToArray());

            if (!listBoxTasks.Items.Cast<object>().Any())
            {
                listBoxTasks.Items.Add("此日期沒有任務");
            }
        }
    }
}
