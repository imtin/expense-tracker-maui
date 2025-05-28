# Personal Expense Tracker and Analyzer

A user-friendly .NET MAUI desktop application for managing and analyzing personal expenses.

---

## Features

* User registration and login
* Expense logging with date, category, amount, and description
* View, edit, and delete expenses
* Automatic report generation (PDF format)
* Data stored in a local MySQL database

---

## Technologies Used

* **.NET MAUI** (.NET 8)
* **C#**
* **MySQL** (via MySQL Workbench)
* **QuestPDF** for report generation

---

## Prerequisites

Make sure the following are installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* [Visual Studio 2022+](https://visualstudio.microsoft.com/) with MAUI workload
* [MySQL Server](https://dev.mysql.com/downloads/mysql/) and [MySQL Workbench](https://dev.mysql.com/downloads/workbench/)

---

## Setup Instructions

### 1. Clone the Repository

```bash
git https://github.com/imtin/expense-tracker-maui.git
cd expense-tracker-maui
```

### 2. Set Up the Database

* Open **MySQL Workbench**
* Run the file `create_schema.sql` to create the database and tables:

  * This creates a schema called `expense_tracker_db` with `users` and `expenses` tables.

### 3. Configure Database Connection

* Open `DatabaseHelper.cs`
* Adjust the connection string if needed:

```csharp
private readonly string connectionString = "server=localhost;user=root;password=YOUR_PASSWORD;database=expense_tracker_db;";
```

### 4. Build and Run the App

* Open `ExpenseTrackerMAUI.sln` in Visual Studio
* Set `ExpenseTrackerMAUI` as the startup project
* Click **Run** or press `F5`

---

## Screenshots

Screenshots of login, adding expenses, report generation, etc., can be found in the project report or appendix.

---

## Project Structure

```
├── App.xaml / App.xaml.cs            → App lifecycle
├── AppShell.xaml / AppShell.xaml.cs → Navigation and shell
├── DatabaseHelper.cs                → Database access
├── Expense.cs                       → Expense model
├── LoginPage.xaml/.cs               → Login UI + logic
├── MainPage.xaml/.cs                → Main dashboard UI + logic
├── RegisterPage.xaml/.cs            → Register UI + logic
├── ReportGenerator.cs               → PDF generation
├── create_schema.sql                → MySQL schema setup
```

---

## License

This project is part of an academic submission for ATU Donegal. For educational use only.

---

## Author

**Ivan Martin**

