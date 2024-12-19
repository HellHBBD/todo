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
    enum InputStatus { Add, Rename }
    public partial class Form_input : Form
    {
        Dictionary<string, User> userList = Program.userList;
        string oldName;
        InputStatus status;

        // add new user
        public Form_input()
        {
            InitializeComponent();
            status = InputStatus.Add;
            label_prompt.Text = "名稱：";
            oldName = textBox_input.Text = "";
        }

        // rename user
        public Form_input(string name)
        {
            InitializeComponent();
            status = InputStatus.Rename;
            label_prompt.Text = "新名稱：";
            oldName = textBox_input.Text = name;
        }

        void add()
        {
            if (textBox_input.Text == "")
            {
                return;
            }
            if (userList.ContainsKey(textBox_input.Text))
            {
                MessageBox.Show("使用者已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            userList[textBox_input.Text] = new User(textBox_input.Text);
        }
        void rename()
        {
            if (string.IsNullOrEmpty(textBox_input.Text) || textBox_input.Text == oldName)
            {
                return;
            }
            if (userList.ContainsKey(textBox_input.Text))
            {
                MessageBox.Show("使用者已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Program.userList[textBox_input.Text] = Program.userList[oldName];
            userList.Remove(oldName);
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            if (status == InputStatus.Add)
            {
                add();
            }
            if (status == InputStatus.Rename)
            {
                rename();
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

        private void Form_input_Load(object sender, EventArgs e)
        {

        }
    }
}
