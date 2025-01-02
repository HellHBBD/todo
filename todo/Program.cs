namespace todo;

static class Program
{
    /* global variable to store login user data */
    public static Dictionary<string, User> userList = new Dictionary<string, User>();
    public static User currentuser = new User("");
    public static Form mainForm = new Form_login();

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        /* exit entire program if all form is disposed */
        while (!mainForm.IsDisposed)
        {
            Application.Run(mainForm);
        }
    }
}
