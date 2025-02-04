﻿namespace todo;

partial class Form_login
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        listBox = new ListBox();
        button_open = new Button();
        button_add = new Button();
        button_remove = new Button();
        button_rename = new Button();
        button_exit = new Button();
        SuspendLayout();
        // 
        // listBox
        // 
        listBox.FormattingEnabled = true;
        listBox.ItemHeight = 19;
        listBox.Location = new Point(12, 12);
        listBox.Name = "listBox";
        listBox.Size = new Size(300, 346);
        listBox.TabIndex = 0;
        listBox.KeyDown += listBox_KeyDown;
        // 
        // button_open
        // 
        button_open.Location = new Point(318, 12);
        button_open.Name = "button_open";
        button_open.Size = new Size(100, 30);
        button_open.TabIndex = 1;
        button_open.Text = "開啟(O)";
        button_open.UseVisualStyleBackColor = true;
        button_open.Click += button_open_Click;
        // 
        // button_add
        // 
        button_add.Location = new Point(318, 48);
        button_add.Name = "button_add";
        button_add.Size = new Size(100, 30);
        button_add.TabIndex = 2;
        button_add.Text = "新增(A)";
        button_add.UseVisualStyleBackColor = true;
        button_add.Click += button_add_Click;
        // 
        // button_remove
        // 
        button_remove.Location = new Point(318, 120);
        button_remove.Name = "button_remove";
        button_remove.Size = new Size(100, 30);
        button_remove.TabIndex = 3;
        button_remove.Text = "刪除(D)";
        button_remove.UseVisualStyleBackColor = true;
        button_remove.Click += button_remove_Click;
        // 
        // button_rename
        // 
        button_rename.Location = new Point(318, 84);
        button_rename.Name = "button_rename";
        button_rename.Size = new Size(100, 30);
        button_rename.TabIndex = 4;
        button_rename.Text = "重新命名(R)";
        button_rename.UseVisualStyleBackColor = true;
        button_rename.Click += button_rename_Click;
        // 
        // button_exit
        // 
        button_exit.Location = new Point(318, 156);
        button_exit.Name = "button_exit";
        button_exit.Size = new Size(100, 30);
        button_exit.TabIndex = 5;
        button_exit.Text = "離開(Q)";
        button_exit.UseVisualStyleBackColor = true;
        button_exit.Click += button_exit_Click;
        // 
        // Form_login
        // 
        AutoScaleDimensions = new SizeF(9F, 19F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(430, 374);
        Controls.Add(button_exit);
        Controls.Add(button_rename);
        Controls.Add(button_remove);
        Controls.Add(button_add);
        Controls.Add(button_open);
        Controls.Add(listBox);
        MinimumSize = new Size(448, 421);
        Name = "Form_login";
        Text = "選擇使用者";
        FormClosing += Form_login_FormClosing;
        Resize += Form_login_Resize;
        ResumeLayout(false);
    }

    #endregion

    private ListBox listBox;
    private Button button_open;
    private Button button_add;
    private Button button_remove;
    private Button button_rename;
    private Button button_exit;
}
