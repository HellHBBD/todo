namespace todo
{
    partial class Form_input
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
            textBox_input = new TextBox();
            button_confirm = new Button();
            button_cancel = new Button();
            label_prompt = new Label();
            SuspendLayout();
            // 
            // textBox_input
            // 
            textBox_input.Location = new Point(12, 31);
            textBox_input.Name = "textBox_input";
            textBox_input.Size = new Size(458, 27);
            textBox_input.TabIndex = 0;
            textBox_input.KeyDown += textBox_input_KeyDown;
            // 
            // button_confirm
            // 
            button_confirm.Location = new Point(276, 64);
            button_confirm.Name = "button_confirm";
            button_confirm.Size = new Size(94, 29);
            button_confirm.TabIndex = 1;
            button_confirm.Text = "確定";
            button_confirm.UseVisualStyleBackColor = true;
            button_confirm.Click += button_confirm_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(376, 64);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(94, 29);
            button_cancel.TabIndex = 2;
            button_cancel.Text = "取消";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // label_prompt
            // 
            label_prompt.AutoSize = true;
            label_prompt.Location = new Point(12, 9);
            label_prompt.Name = "label_prompt";
            label_prompt.Size = new Size(54, 19);
            label_prompt.TabIndex = 3;
            label_prompt.Text = "名稱：";
            // 
            // Form_input
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(482, 103);
            Controls.Add(label_prompt);
            Controls.Add(button_cancel);
            Controls.Add(button_confirm);
            Controls.Add(textBox_input);
            Name = "Form_input";
            Text = "Form_input";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_input;
        private Button button_confirm;
        private Button button_cancel;
        private Label label_prompt;
    }
}