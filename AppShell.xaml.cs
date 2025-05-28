namespace ExpenseTrackerMAUI;

public partial class AppShell : Shell
{
    public static int LoggedInUserId { get; private set; }
    public static string LoggedInUsername { get; private set; }

    public AppShell(int userId, string username)
    {
        InitializeComponent();

        LoggedInUserId = userId;
        LoggedInUsername = username;

      
        CurrentItem = new ShellContent
        {
            Content = new MainPage(username)
        };
    }
}
