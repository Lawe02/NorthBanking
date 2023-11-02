# NorthBankinApplication 🏦

**NorthBankinApplication** is a feature-rich banking application that empowers users to perform various actions based on their roles. Administrators wield the privilege to edit, add, and delete users, as well as update customer information. Cashiers are equipped to manage account-related tasks, including deposits, withdrawals, transfers, and creating new accounts. The application also boasts a transaction checker that diligently saves potentially fraudulent transactions to a file if they meet specific criteria.

## Features

### Admin Capabilities 👩‍💼

- ✏️ Edit user profiles
- ➕ Add new users
- ❌ Delete users
- 🔄 Update customer information

### Cashier Capabilities 💼

- 💰 Make deposits to customer accounts
- 💸 Perform withdrawals
- 🔀 Initiate fund transfers between accounts
- 🏦 Create new bank accounts

### Transaction Checker 🚫

- 🔍 Monitors transactions for potential fraud
- 📝 Saves suspicious transactions to a report file

## Installation and Configuration

Before getting started with NorthBankinApplication, ensure you have the required dependencies and configurations in place:

1. **Clone the Repository** 🔗

    ```bash
    git clone <repository-url>
    cd NorthBankingSystem
    ```

2. **Configure Application Settings** ⚙️

    - Review the `appsettings.json` file.
    - Configure the database connection string, JWT settings, and other application-specific settings.

3. **Install Necessary Packages** 📦

    ```bash
    dotnet restore
    ```

4. **Apply Database Migrations** 📥

    ```bash
    dotnet ef database update
    ```

5. **Run the Application** ▶️

    ```bash
    dotnet run
    ```

## Usage

**Authenticating Users** 🔐

Accessing the application's features requires user authentication. Authentication is managed via the `/api/auth/login` endpoint, where administrators and cashiers receive a JSON Web Token (JWT) upon successful login.

### API Endpoints 🌐

**AuthController**

- POST `/api/auth/login`: Authenticate users and receive a JWT token for access.

**BankController**

- GET `/api/bank/me/{id}`: Retrieve customer information by providing their ID.
- GET `/api/bank/account/{id}/{limit}/{offset}`: Retrieve account transactions with pagination.

## Development and Customization

Customizing or extending the application is made simple by modifying the source code. The application is built using ASP.NET Core and Entity Framework for backend development, offering the flexibility to explore and make changes as needed.

**Transaction Checker Report**

The transaction checker diligently saves potentially fraudulent transactions to a file at the following location:

```csharp
Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)/rapport.txt
## License

This project is licensed under the MIT License. Refer to the [LICENSE](link-to-license-file) file for detailed information.

## Contributors

- Your Name - Project Lead

## Acknowledgments

Special thanks to Your Acknowledgment for their contributions and support.

## Production Link

[**NorthBank **](https://northbank.azurewebsites.net/)

**Admin Credentials:**

- Username: `richard.chalk@systementor.se`
- Password: `Hejsan123#`

**Cashier Credentials:**

- Username: `richard.chalk@cashier.systementor.se`
- Password: `Hejsan123#`

