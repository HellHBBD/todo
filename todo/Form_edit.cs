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

        //add new mission
        public Form_edit(string user)
        {
            InitializeComponent();
            status = EditStatus.Add;
            label_name.Text = "任務名稱：";
            oldTask = textBox_input.Text = "";
            curUser = Program.userList[user];
        }

        //edit old mission
        public Form_edit(string user, string task)
        {
            InitializeComponent();
            status = EditStatus.Modify;
            label_name.Text = "新任務名稱：";
            oldTask = textBox_input.Text = task;
            curUser = Program.userList[user];
        }

        private void Form_edit_Load(object sender, EventArgs e)
        {

        }
        void add()
        {
            if (textBox_input.Text == "")
            {
                return;
            }
            if (curUser.taskList.ContainsKey(textBox_input.Text))
            {
                MessageBox.Show("任務已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            curUser.taskList[textBox_input.Text] = new Task(textBox_input.Text);
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
            curUser.taskList[textBox_input.Text] = curUser.taskList[oldTask];
            curUser.taskList.Remove(oldTask);
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
    }
}
