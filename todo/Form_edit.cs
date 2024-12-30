﻿using System;
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
        EditStatus status;
        private Form_home formhome;

        /* add new mission */
        public Form_edit(Form_home home)
        {
            InitializeComponent();
            status = EditStatus.Add;
            label_name.Text = "任務名稱：";
            oldTask = textBox_input.Text = "";
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
            formhome.AddCheckBox(taskName);

        }
        void modify()
        {
            if (string.IsNullOrEmpty(textBox_input.Text) || textBox_input.Text == oldTask)
            {
                return;
            }
            if (Program.currentuser.taskList.ContainsKey(textBox_input.Text))
            {
                MessageBox.Show("任務已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            /* modify task name */
            Task taskToUpdate = Program.currentuser.taskList[oldTask];
            Program.currentuser.taskList.Remove(oldTask);
            taskToUpdate.name = textBox_input.Text;
            taskToUpdate.TaskCheckBox.Text = textBox_input.Text;
            Program.currentuser.taskList[textBox_input.Text] = taskToUpdate;
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
                /* update checkBox.Text */
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
