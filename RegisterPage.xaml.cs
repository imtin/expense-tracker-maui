namespace ExpenseTrackerMAUI;

public partial class RegisterPage : ContentPage
{
    private DatabaseHelper db = new DatabaseHelper();

    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text?.Trim();
        string password = passwordEntry.Text;
        string confirm = confirmPasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            messageLabel.Text = "Please fill in all fields.";
            return;
        }

        if (password != confirm)
        {
            messageLabel.Text = "Passwords do not match.";
            return;
        }

        bool success = db.RegisterUser(username, password);

        if (success)
        {
            await DisplayAlert("Success", "Account created. You can now log in.", "OK");
            await Navigation.PopAsync(); // return to LoginPage
        }
        else
        {
            messageLabel.Text = "Username already exists.";
        }
    }
}
