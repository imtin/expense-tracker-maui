using MySql.Data.MySqlClient;

namespace ExpenseTrackerMAUI;

public class DatabaseHelper
{
    private readonly string connectionString = "server=localhost;user=root;password=kikoklaun;database=expense_tracker_db;";

    //Validate login credentials
    public int ValidateUser(string username, string password)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        string query = "SELECT id FROM users WHERE username = @username AND password = @password";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);
        var result = cmd.ExecuteScalar();
        return result != null ? Convert.ToInt32(result) : -1;
    }

    //Check if a user exists
    public bool DoesUserExist(string username)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        string query = "SELECT COUNT(*) FROM users WHERE username = @username";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@username", username);
        int count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0;
    }

    //Register a new user
    public bool RegisterUser(string username, string password)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
        using var checkCmd = new MySqlCommand(checkQuery, conn);
        checkCmd.Parameters.AddWithValue("@username", username);
        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
        if (count > 0) return false;

        string insertQuery = "INSERT INTO users (username, password) VALUES (@username, @password)";
        using var insertCmd = new MySqlCommand(insertQuery, conn);
        insertCmd.Parameters.AddWithValue("@username", username);
        insertCmd.Parameters.AddWithValue("@password", password);
        return insertCmd.ExecuteNonQuery() > 0;
    }

    //Insert a new expense
    public bool InsertExpense(Expense expense)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        string query = "INSERT INTO expenses (expense_date, category, amount, description, user_id) VALUES (@date, @cat, @amt, @desc, @uid)";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@date", expense.ExpenseDate);
        cmd.Parameters.AddWithValue("@cat", expense.Category);
        cmd.Parameters.AddWithValue("@amt", expense.Amount);
        cmd.Parameters.AddWithValue("@desc", expense.Description);
        cmd.Parameters.AddWithValue("@uid", expense.UserId);
        return cmd.ExecuteNonQuery() > 0;
    }

    //Get all expenses for a specific user
    public List<Expense> GetExpensesByUser(int userId)
    {
        var list = new List<Expense>();
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        string query = "SELECT * FROM expenses WHERE user_id = @uid";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@uid", userId);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Expense
            {
                Id = reader.GetInt32("id"),
                ExpenseDate = reader.GetDateTime("expense_date"),
                Category = reader.GetString("category"),
                Amount = reader.GetDecimal("amount"),
                Description = reader.GetString("description"),
                UserId = reader.GetInt32("user_id")
            });
        }
        return list;
    }

    //Update an existing expense
    public bool UpdateExpense(Expense expense)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        string query = "UPDATE expenses SET expense_date=@date, category=@cat, amount=@amt, description=@desc WHERE id=@id";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@date", expense.ExpenseDate);
        cmd.Parameters.AddWithValue("@cat", expense.Category);
        cmd.Parameters.AddWithValue("@amt", expense.Amount);
        cmd.Parameters.AddWithValue("@desc", expense.Description);
        cmd.Parameters.AddWithValue("@id", expense.Id);
        return cmd.ExecuteNonQuery() > 0;
    }

    //Delete an expense
    public bool DeleteExpense(int id)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();

        string query = "DELETE FROM expenses WHERE id=@id";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }
}
