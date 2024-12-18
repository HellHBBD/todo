using System.Text;
using Newtonsoft.Json;

namespace todo;

public partial class Form_login : Form
{
    Dictionary<string, User> userList = Program.userList;
    void updateListBox()
    {
        /* update listBox content from userList */
        listBox.Items.Clear();
        foreach (var kvp in userList)
        {
            listBox.Items.Add(kvp.Key);
        }
    }

    public Form_login()
    {
        InitializeComponent();
        /* read data from file or create new object */
        try
        {
            string jsonString = File.ReadAllText("data.json", Encoding.UTF8);
            userList = JsonConvert.DeserializeObject<Dictionary<string, User>>(jsonString) ?? new Dictionary<string, User>();
        }
        catch
        {
            userList = new Dictionary<string, User>();
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
        userList.Remove((string)listBox.SelectedItem);
        updateListBox();
    }

    private void button_exit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Form_login_FormClosing(object sender, FormClosingEventArgs e)
    {
        string jsonString = JsonConvert.SerializeObject(userList);
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
        /* change mainForm to Form_home and exit Form_login */
        Program.mainForm = new Form_home((string)listBox.SelectedItem);
        Close();
    }
}
