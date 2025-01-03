using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace todo
{
    enum EditStatus { Add, Modify }
    public partial class Form_edit : Form
    {
        string oldTask;
        EditStatus status;
        private Form_home formhome;

        /* add new mission */
        public Form_edit(Form_home home)
        {
            InitializeComponent();
            status = EditStatus.Add;
            label_name.Text = "任務名稱：";
            oldTask = textBox_input.Text = "";
            // 建立資料表
            DataTable dt = new DataTable();
            dt.Columns.Add("顯示文字");
            dt.Columns.Add("值");

            dt.Rows.Add("1", 1);
            dt.Rows.Add("2", 2);
            dt.Rows.Add("3", 3);
            dt.Rows.Add("4", 4);
            dt.Rows.Add("5", 5);
            dt.Rows.Add("6", 6);
            dt.Rows.Add("7", 7);
            dt.Rows.Add("8", 8);
            dt.Rows.Add("9", 9);
            dt.Rows.Add("10", 10);
            // 綁定資料
            comboBox_imortamt.DataSource = dt;
            comboBox_imortamt.DisplayMember = "顯示文字";  // 顯示在下拉選單中的文字
            comboBox_imortamt.ValueMember = "值";         // 實際值

            formhome = home;
        }

        /* edit old mission */
        public Form_edit(Form_home home, string task)
        {
            InitializeComponent();
            status = EditStatus.Modify;
            label_name.Text = "新任務名稱：";
            oldTask = textBox_input.Text = task;
            formhome = home;
            // 建立資料表
            DataTable dt = new DataTable();
            dt.Columns.Add("顯示文字");
            dt.Columns.Add("值");

            dt.Rows.Add("1", 1);
            dt.Rows.Add("2", 2);
            dt.Rows.Add("3", 3);
            dt.Rows.Add("4", 4);
            dt.Rows.Add("5", 5);
            dt.Rows.Add("6", 6);
            dt.Rows.Add("7", 7);
            dt.Rows.Add("8", 8);
            dt.Rows.Add("9", 9);
            dt.Rows.Add("10", 10);
            // 綁定資料
            comboBox_imortamt.DataSource = dt;
            comboBox_imortamt.DisplayMember = "顯示文字";  // 顯示在下拉選單中的文字
            comboBox_imortamt.ValueMember = "值";         // 實際值

            textBox_input.Focus();
            textBox_input.SelectAll();
        }
        private void add()
        {
            if (textBox_input.Text == "")
            {
                return;
            }

            string taskName = textBox_input.Text;

            /* check duplicate task */
            if (Program.currentuser.taskList.ContainsKey(taskName))
            {
                MessageBox.Show("任務已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime d = dateTimePicker_task.Value;
            int important = comboBox_imortamt.SelectedIndex+1;
            formhome.AddCheckBox(taskName, d, important);
        }
        void modify()
        {
            if (string.IsNullOrWhiteSpace(textBox_input.Text))
            {
                return;
            }
            if (Program.currentuser.taskList.ContainsKey(textBox_input.Text))
            {
                MessageBox.Show("任務已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime d = dateTimePicker_task.Value;
            int important = comboBox_imortamt.SelectedIndex + 1;
            /* modify task name */
            Task taskToUpdate = Program.currentuser.taskList[oldTask];
            Program.currentuser.taskList.Remove(oldTask);
            taskToUpdate.name = textBox_input.Text;
            taskToUpdate.date = d;
            taskToUpdate.important = important;
            Program.currentuser.taskList[textBox_input.Text] = taskToUpdate;

            /* update checkBox.Text */
            var checkBoxToUpdate = formhome.Controls.OfType<CheckBox>()
                .FirstOrDefault(cb => cb.Text == oldTask);
            if (checkBoxToUpdate != null)
            {
                checkBoxToUpdate.Text = textBox_input.Text;
            }
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            if (status == EditStatus.Add)
            {
                add();
            }
            if (status == EditStatus.Modify)
            {
                modify();
            }
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void textBox_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_confirm_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                button_cancel_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }
    }
}
