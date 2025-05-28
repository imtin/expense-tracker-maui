namespace ExpenseTrackerMAUI;

public partial class MainPage : ContentPage
{
    private readonly DatabaseHelper db = new DatabaseHelper();
    private Expense selectedExpense;
    private string username;

    public MainPage(string currentUsername)
    {
        InitializeComponent();
        username = currentUsername;
        welcomeLabel.Text = $"Welcome, {username}!";
        datePicker.Date = DateTime.Today;
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(amountEntry.Text) || categoryPicker.SelectedIndex == -1)
        {
            DisplayAlert("Input Error", "Please enter an amount and select a category.", "OK");
            return;
        }

        Expense newExpense = new Expense
        {
            ExpenseDate = datePicker.Date,
            Category = categoryPicker.SelectedItem.ToString(),
            Amount = decimal.Parse(amountEntry.Text),
            Description = descriptionEntry.Text ?? string.Empty,
            UserId = AppShell.LoggedInUserId
        };

        if (db.InsertExpense(newExpense))
        {
            ClearInputs();
            DisplayAlert("Success", "Expense added.", "OK");
            OnViewAllClicked(null, null);
        }
    }

    private void OnViewAllClicked(object sender, EventArgs e)
    {
        var expenses = db.GetExpensesByUser(AppShell.LoggedInUserId);
        expenseListView.ItemsSource = expenses;
        totalLabel.Text = $"Total Spent: €{expenses.Sum(e => e.Amount):F2}";
    }

    private void OnExpenseSelected(object sender, SelectionChangedEventArgs e)
    {
        selectedExpense = e.CurrentSelection.FirstOrDefault() as Expense;
        if (selectedExpense != null)
        {
            datePicker.Date = selectedExpense.ExpenseDate;
            categoryPicker.SelectedItem = selectedExpense.Category;
            amountEntry.Text = selectedExpense.Amount.ToString("F2");
            descriptionEntry.Text = selectedExpense.Description;
        }
    }

    private void OnUpdateClicked(object sender, EventArgs e)
    {
        if (selectedExpense == null)
        {
            DisplayAlert("Select Item", "Please select an expense to update.", "OK");
            return;
        }

        selectedExpense.ExpenseDate = datePicker.Date;
        selectedExpense.Category = categoryPicker.SelectedItem.ToString();
        selectedExpense.Amount = decimal.Parse(amountEntry.Text);
        selectedExpense.Description = descriptionEntry.Text;

        if (db.UpdateExpense(selectedExpense))
        {
            ClearInputs();
            selectedExpense = null;
            OnViewAllClicked(null, null);
        }
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (selectedExpense == null)
        {
            DisplayAlert("Select Item", "Please select an expense to delete.", "OK");
            return;
        }

        if (db.DeleteExpense(selectedExpense.Id))
        {
            ClearInputs();
            selectedExpense = null;
            OnViewAllClicked(null, null);
        }
    }

    private void OnGenerateReportClicked(object sender, EventArgs e)
    {
        var expenses = db.GetExpensesByUser(AppShell.LoggedInUserId);
        ReportGenerator.Generate(expenses, username);

    }

    private void ClearInputs()
    {
        datePicker.Date = DateTime.Today;
        categoryPicker.SelectedIndex = -1;
        amountEntry.Text = string.Empty;
        descriptionEntry.Text = string.Empty;
    }
}
