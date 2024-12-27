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
    enum EditStatus { Add, Modify }
    public partial class Form_edit : Form
    {
        string oldTask;
        User curUser;
        EditStatus status;
        private Form_home formhome;

        /* add new mission */
        public Form_edit(Form_home home,string user)
        {
            InitializeComponent();
            status = EditStatus.Add;
            label_name.Text = "任務名稱：";
            oldTask = textBox_input.Text = "";
            curUser = Program.userList[user];
            formhome = home;
        }

        /* edit old mission */
        public Form_edit(Form_home home, string user, string task)
        {
            InitializeComponent();
            status = EditStatus.Modify;
            label_name.Text = "新任務名稱：";
            oldTask = textBox_input.Text = task;
            curUser = Program.userList[user];
            formhome = home;
        }

        private void Form_edit_Load(object sender, EventArgs e)
        {

        }
        private void add()
        {
            if (textBox_input.Text == "")
            {
                return;
            }

            string taskName = textBox_input.Text;

            // 檢查是否任務已存在
            if (curUser.taskList.ContainsKey(taskName))
            {
                MessageBox.Show("任務已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 新建任務
            Task newTask = new Task(taskName);

            // 新建 CheckBox 並關聯到 Task
            CheckBox taskCheckBox = new CheckBox
            {
                Text = taskName,
                Location = new Point(200, 200), // 動態定位
                AutoSize = true
            };

            // 綁定事件（可選）
            taskCheckBox.CheckedChanged += newTask.OnCheckBoxChanged;

            // 將 CheckBox 保存到 Task
            newTask.TaskCheckBox = taskCheckBox;

            // 將 Task 加入到使用者的任務列表
            curUser.taskList[taskName] = newTask;

            // 新增 CheckBox 到當前表單
            formhome.Controls.Add(taskCheckBox);

            
        }
        void modify() 
        {
            if (string.IsNullOrEmpty(textBox_input.Text) || textBox_input.Text == oldTask)
            {
                return;
            }
            if (curUser.taskList.ContainsKey(textBox_input.Text))
            {
                MessageBox.Show("任務已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 修改任務名稱
            Task taskToUpdate = curUser.taskList[oldTask];
            curUser.taskList.Remove(oldTask);
            taskToUpdate.name = textBox_input.Text;
            taskToUpdate.TaskCheckBox.Text = textBox_input.Text;
            curUser.taskList[textBox_input.Text] = taskToUpdate;
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
                // 更新 CheckBox 的文字
                var checkBoxToUpdate = formhome.Controls.OfType<CheckBox>()
                    .FirstOrDefault(cb => cb.Text == oldTask);
                if (checkBoxToUpdate != null)
                {
                    checkBoxToUpdate.Text = textBox_input.Text;
                }
            }
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
