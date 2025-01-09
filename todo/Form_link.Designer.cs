namespace todo
{
    partial class Form_link
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
            listBox_prev = new ListBox();
            button_cancel = new Button();
            listBox_available = new ListBox();
            button_right = new Button();
            button_left = new Button();
            listBox_next = new ListBox();
            label_prev = new Label();
            label_next = new Label();
            label_available = new Label();
            SuspendLayout();
            // 
            // listBox_prev
            // 
            listBox_prev.FormattingEnabled = true;
            listBox_prev.Location = new Point(12, 35);
            listBox_prev.Name = "listBox_prev";
            listBox_prev.Size = new Size(230, 303);
            listBox_prev.TabIndex = 1;
            listBox_prev.SelectedIndexChanged += listBox_prev_SelectedIndexChanged;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(667, 452);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(112, 34);
            button_cancel.TabIndex = 3;
            button_cancel.Text = "完成";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // listBox_available
            // 
            listBox_available.FormattingEnabled = true;
            listBox_available.Location = new Point(284, 35);
            listBox_available.Name = "listBox_available";
            listBox_available.Size = new Size(230, 303);
            listBox_available.TabIndex = 4;
            listBox_available.SelectedIndexChanged += listBox_available_SelectedIndexChanged;
            // 
            // button_right
            // 
            button_right.Location = new Point(417, 369);
            button_right.Name = "button_right";
            button_right.Size = new Size(112, 34);
            button_right.TabIndex = 5;
            button_right.Text = ">";
            button_right.UseVisualStyleBackColor = true;
            button_right.Click += button_right_Click;
            // 
            // button_left
            // 
            button_left.Location = new Point(260, 369);
            button_left.Name = "button_left";
            button_left.Size = new Size(112, 34);
            button_left.TabIndex = 6;
            button_left.Text = "<";
            button_left.UseVisualStyleBackColor = true;
            button_left.Click += button_left_Click;
            // 
            // listBox_next
            // 
            listBox_next.FormattingEnabled = true;
            listBox_next.Location = new Point(552, 35);
            listBox_next.Name = "listBox_next";
            listBox_next.Size = new Size(230, 303);
            listBox_next.TabIndex = 7;
            listBox_next.SelectedIndexChanged += listBox_next_SelectedIndexChanged;
            // 
            // label_prev
            // 
            label_prev.AutoSize = true;
            label_prev.Location = new Point(82, 9);
            label_prev.Name = "label_prev";
            label_prev.Size = new Size(82, 23);
            label_prev.TabIndex = 8;
            label_prev.Text = "previous";
            // 
            // label_next
            // 
            label_next.AutoSize = true;
            label_next.Location = new Point(641, 9);
            label_next.Name = "label_next";
            label_next.Size = new Size(47, 23);
            label_next.TabIndex = 9;
            label_next.Text = "next";
            // 
            // label_available
            // 
            label_available.AutoSize = true;
            label_available.Location = new Point(367, 9);
            label_available.Name = "label_available";
            label_available.Size = new Size(64, 23);
            label_available.TabIndex = 10;
            label_available.Text = "未加入";
            // 
            // Form_link
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 498);
            Controls.Add(label_available);
            Controls.Add(label_next);
            Controls.Add(label_prev);
            Controls.Add(listBox_next);
            Controls.Add(button_left);
            Controls.Add(button_right);
            Controls.Add(listBox_available);
            Controls.Add(button_cancel);
            Controls.Add(listBox_prev);
            Name = "Form_link";
            Text = "Form_link";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox_prev;
        private Button button_cancel;
        private ListBox listBox_available;
        private Button button_right;
        private Button button_left;
        private ListBox listBox_next;
        private Label label_prev;
        private Label label_next;
        private Label label_available;
    }
}