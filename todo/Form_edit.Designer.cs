namespace todo
{
    partial class Form_edit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label_name = new Label();
            textBox_input = new TextBox();
            button_confirm = new Button();
            button_cancel = new Button();
            label_date = new Label();
            dateTimePicker_task = new DateTimePicker();
            label1 = new Label();
            comboBox_imortamt = new ComboBox();
            button_descrip = new Button();
            button_remove = new Button();
            button_prev = new Button();
            button_next = new Button();
            SuspendLayout();
            // 
            // label_name
            // 
            label_name.AutoSize = true;
            label_name.Location = new Point(15, 11);
            label_name.Margin = new Padding(4, 0, 4, 0);
            label_name.Name = "label_name";
            label_name.Size = new Size(50, 23);
            label_name.TabIndex = 0;
            label_name.Text = "名稱:";
            // 
            // textBox_input
            // 
            textBox_input.Location = new Point(15, 38);
            textBox_input.Margin = new Padding(4);
            textBox_input.Name = "textBox_input";
            textBox_input.Size = new Size(559, 30);
            textBox_input.TabIndex = 1;
            textBox_input.KeyDown += textBox_input_KeyDown;
            // 
            // button_confirm
            // 
            button_confirm.Location = new Point(336, 199);
            button_confirm.Margin = new Padding(4);
            button_confirm.Name = "button_confirm";
            button_confirm.Size = new Size(115, 35);
            button_confirm.TabIndex = 2;
            button_confirm.Text = "確認";
            button_confirm.UseVisualStyleBackColor = true;
            button_confirm.Click += button_confirm_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(459, 198);
            button_cancel.Margin = new Padding(4);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(115, 35);
            button_cancel.TabIndex = 3;
            button_cancel.Text = "取消";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // label_date
            // 
            label_date.AutoSize = true;
            label_date.Location = new Point(15, 82);
            label_date.Margin = new Padding(4, 0, 4, 0);
            label_date.Name = "label_date";
            label_date.Size = new Size(50, 23);
            label_date.TabIndex = 4;
            label_date.Text = "期限:";
            // 
            // dateTimePicker_task
            // 
            dateTimePicker_task.CustomFormat = "yyyy-MM-dd";
            dateTimePicker_task.Format = DateTimePickerFormat.Custom;
            dateTimePicker_task.Location = new Point(114, 76);
            dateTimePicker_task.Margin = new Padding(4);
            dateTimePicker_task.Name = "dateTimePicker_task";
            dateTimePicker_task.Size = new Size(305, 30);
            dateTimePicker_task.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 117);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(86, 23);
            label1.TabIndex = 6;
            label1.Text = "重要程度:";
            // 
            // comboBox_imortamt
            // 
            comboBox_imortamt.FormattingEnabled = true;
            comboBox_imortamt.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            comboBox_imortamt.Location = new Point(114, 114);
            comboBox_imortamt.Margin = new Padding(4);
            comboBox_imortamt.Name = "comboBox_imortamt";
            comboBox_imortamt.Size = new Size(184, 31);
            comboBox_imortamt.TabIndex = 7;
            // 
            // button_descrip
            // 
            button_descrip.Location = new Point(15, 199);
            button_descrip.Margin = new Padding(4);
            button_descrip.Name = "button_descrip";
            button_descrip.Size = new Size(115, 35);
            button_descrip.TabIndex = 8;
            button_descrip.Text = "詳述";
            button_descrip.UseVisualStyleBackColor = true;
            button_descrip.Click += button_descrip_Click;
            // 
            // button_remove
            // 
            button_remove.Location = new Point(217, 198);
            button_remove.Name = "button_remove";
            button_remove.Size = new Size(112, 34);
            button_remove.TabIndex = 9;
            button_remove.Text = "刪除任務";
            button_remove.UseVisualStyleBackColor = true;
            button_remove.Click += button_remove_Click;
            // 
            // button_prev
            // 
            button_prev.Location = new Point(15, 155);
            button_prev.Name = "button_prev";
            button_prev.Size = new Size(112, 34);
            button_prev.TabIndex = 10;
            button_prev.Text = "prev";
            button_prev.UseVisualStyleBackColor = true;
            // 
            // button_next
            // 
            button_next.Location = new Point(133, 155);
            button_next.Name = "button_next";
            button_next.Size = new Size(112, 34);
            button_next.TabIndex = 11;
            button_next.Text = "next";
            button_next.UseVisualStyleBackColor = true;
            // 
            // Form_edit
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(589, 247);
            Controls.Add(button_next);
            Controls.Add(button_prev);
            Controls.Add(button_remove);
            Controls.Add(button_descrip);
            Controls.Add(comboBox_imortamt);
            Controls.Add(label1);
            Controls.Add(dateTimePicker_task);
            Controls.Add(label_date);
            Controls.Add(button_cancel);
            Controls.Add(button_confirm);
            Controls.Add(textBox_input);
            Controls.Add(label_name);
            Margin = new Padding(4);
            Name = "Form_edit";
            Text = "Form_edit";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_name;
        private TextBox textBox_input;
        private Button button_confirm;
        private Button button_cancel;
        private Label label_date;
        private DateTimePicker dateTimePicker_task;
        private Label label1;
        private ComboBox comboBox_imortamt;
        private Button button_descrip;
        private Button button_remove;
        private Button button_prev;
        private Button button_next;
    }
}