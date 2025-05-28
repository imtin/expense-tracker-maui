namespace ExpenseTrackerMAUI;

public partial class LoginPage : ContentPage
{
    private readonly DatabaseHelper db = new DatabaseHelper();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text?.Trim();
        string password = passwordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            messageLabel.Text = "Please enter both username and password.";
            return;
        }

        int userId = db.ValidateUser(username, password);
        if (userId > 0)
        {
            // Login successful: pass both userId and username
            Application.Current.MainPage = new AppShell(userId, username);
        }
        else
        {
            bool userExists = db.DoesUserExist(username);
            if (!userExists)
            {
                messageLabel.Text = "User not found. Please register.";
            }
            else
            {
                messageLabel.Text = "Incorrect password. Please try again.";
            }
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
