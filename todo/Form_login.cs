using System.Text;
using Newtonsoft.Json;

namespace todo;

public partial class Form_login : Form
{
    const int margin = 12;
    const int padding = 6;
    void updateListBox()
    {
        listBox.Items.Clear();
        foreach (var kvp in Program.userList)
        {
            listBox.Items.Add(kvp.Key);
        }
    }

    public Form_login()
    {
        InitializeComponent();
        try
        {
            string jsonString = File.ReadAllText("data.json", Encoding.UTF8);
            Program.userList = JsonConvert.DeserializeObject<Dictionary<string, User>>(jsonString) ?? new Dictionary<string, User>();
        }
        catch
        {
            Program.userList = new Dictionary<string, User>();
        }
        updateListBox();
    }

    private void button_add_Click(object sender, EventArgs e)
    {
        Form_input form = new Form_input();
        form.ShowDialog();
        updateListBox();
    }

    private void button_remove_Click(object sender, EventArgs e)
    {
        if (listBox.SelectedItem == null)
        {
            return;
        }
        if (MessageBox.Show("使用者以及資料將永久刪除，是否確定刪除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            return;
        }
        Program.userList.Remove((string)listBox.SelectedItem);
        updateListBox();
    }

    private void button_exit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Form_login_FormClosing(object sender, FormClosingEventArgs e)
    {
        string jsonString = JsonConvert.SerializeObject(Program.userList);
        File.WriteAllText("data.json", jsonString, Encoding.UTF8);
    }

    private void button_rename_Click(object sender, EventArgs e)
    {
        if (listBox.SelectedItem == null)
        {
            return;
        }
        Form_input form = new Form_input((string)listBox.SelectedItem);
        form.ShowDialog();
        updateListBox();
    }

    private void button_open_Click(object sender, EventArgs e)
    {
        if (listBox.SelectedItem == null)
        {
            return;
        }
        string? userName = listBox.SelectedItem.ToString();
        if (userName != null)
        {
            /* change mainForm to Form_home and exit Form_login */
            Program.currentuser = Program.userList[userName];
            Program.mainForm = new Form_home(Program.currentuser);
            Close();
        }
    }

    private void listBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.O)
        {
            button_open_Click(sender, e);
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.A)
        {
            button_add_Click(sender, e);
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.R)
        {
            button_rename_Click(sender, e);
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.D)
        {
            button_remove_Click(sender, e);
            e.SuppressKeyPress = true;
        }
        else if (e.KeyCode == Keys.Q)
        {
            button_exit_Click(sender, e);
            e.SuppressKeyPress = true;
        }
    }

    private void Form_login_Resize(object sender, EventArgs e)
    {
        listBox.Width = Width - 2 * margin - padding - 100 - 20;
        listBox.Height = Height - 2 * margin - 40;

        int x = listBox.Width + margin + padding;
        int y = margin;
        button_open.Left = x;
        button_open.Top = y;

        y += (padding + 30);
        button_add.Left = x;
        button_add.Top = y;

        y += (padding + 30);
        button_rename.Left = x;
        button_rename.Top = y;

        y += (padding + 30);
        button_remove.Left = x;
        button_remove.Top = y;

        y += (padding + 30);
        button_exit.Left = x;
        button_exit.Top = y;
    }
}
