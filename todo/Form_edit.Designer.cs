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
            SuspendLayout();
            // 
            // label_name
            // 
            label_name.AutoSize = true;
            label_name.Location = new Point(12, 9);
            label_name.Name = "label_name";
            label_name.Size = new Size(42, 19);
            label_name.TabIndex = 0;
            label_name.Text = "名稱:";
            // 
            // textBox_input
            // 
            textBox_input.Location = new Point(12, 31);
            textBox_input.Name = "textBox_input";
            textBox_input.Size = new Size(458, 27);
            textBox_input.TabIndex = 1;
            textBox_input.KeyDown += textBox_input_KeyDown;
            // 
            // button_confirm
            // 
            button_confirm.Location = new Point(276, 139);
            button_confirm.Name = "button_confirm";
            button_confirm.Size = new Size(94, 29);
            button_confirm.TabIndex = 2;
            button_confirm.Text = "確認";
            button_confirm.UseVisualStyleBackColor = true;
            button_confirm.Click += button_confirm_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(376, 139);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(94, 29);
            button_cancel.TabIndex = 3;
            button_cancel.Text = "取消";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // label_date
            // 
            label_date.AutoSize = true;
            label_date.Location = new Point(12, 76);
            label_date.Name = "label_date";
            label_date.Size = new Size(42, 19);
            label_date.TabIndex = 4;
            label_date.Text = "期限:";
            // 
            // dateTimePicker_task
            // 
            dateTimePicker_task.CustomFormat = "yyyy-MM-dd";
            dateTimePicker_task.Format = DateTimePickerFormat.Custom;
            dateTimePicker_task.Location = new Point(93, 70);
            dateTimePicker_task.Name = "dateTimePicker_task";
            dateTimePicker_task.Size = new Size(250, 27);
            dateTimePicker_task.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 114);
            label1.Name = "label1";
            label1.Size = new Size(72, 19);
            label1.TabIndex = 6;
            label1.Text = "重要程度:";
            // 
            // comboBox_imortamt
            // 
            comboBox_imortamt.FormattingEnabled = true;
            comboBox_imortamt.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            comboBox_imortamt.Location = new Point(93, 106);
            comboBox_imortamt.Name = "comboBox_imortamt";
            comboBox_imortamt.Size = new Size(151, 27);
            comboBox_imortamt.TabIndex = 7;
            // 
            // button_descrip
            // 
            button_descrip.Location = new Point(12, 139);
            button_descrip.Name = "button_descrip";
            button_descrip.Size = new Size(94, 29);
            button_descrip.TabIndex = 8;
            button_descrip.Text = "詳述";
            button_descrip.UseVisualStyleBackColor = true;
            button_descrip.Click += button_descrip_Click;
            // 
            // Form_edit
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 180);
            Controls.Add(button_descrip);
            Controls.Add(comboBox_imortamt);
            Controls.Add(label1);
            Controls.Add(dateTimePicker_task);
            Controls.Add(label_date);
            Controls.Add(button_cancel);
            Controls.Add(button_confirm);
            Controls.Add(textBox_input);
            Controls.Add(label_name);
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
    }
}