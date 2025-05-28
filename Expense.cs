namespace ExpenseTrackerMAUI;

public class Expense
{
    public int Id { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }

    public string DisplayInfo => $"{ExpenseDate:dd/MM/yyyy} - {Category} - €{Amount:F2} - {Description}";
}
