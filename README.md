🏦 NorthBankinApplication
NorthBankinApplication is a feature-rich banking application that allows users to perform various actions based on their roles. Administrators have the privilege to edit, add, and delete users, as well as update customer information. Cashiers can handle account-related tasks like deposits, withdrawals, transfers, and creating new accounts. Additionally, the application includes a transaction checker that saves potentially fraudulent transactions to a file if they meet certain criteria.

Features
👩‍💼 Admin Capabilities
✏️ Edit user profiles
➕ Add new users
❌ Delete users
🔄 Update customer information
💼 Cashier Capabilities
💰 Make deposits to customer accounts
💸 Perform withdrawals
🔀 Initiate fund transfers between accounts
🏦 Create new bank accounts
🚫 Transaction Checker
🔍 Monitors transactions for potential fraud
📝 Saves suspicious transactions to a report file
Installation and Configuration
Before running the NorthBankinApplication, you need to make sure you have the required dependencies and configurations set up:

🔗 Clone the repository:

bash
Copy code
git clone <repository-url>
cd NorthBankinApplication
⚙️ Configure the application settings:

Review the appsettings.json and configure the database connection string, JWT settings, and other application-specific settings.
📦 Install necessary packages:

bash
Copy code
dotnet restore
📥 Apply database migrations:

bash
Copy code
dotnet ef database update
▶️ Run the application:

bash
Copy code
dotnet run
Usage
🔐 Authenticating Users
To access the application's features, users need to authenticate. The authentication is handled via the /api/auth/login endpoint. Administrators and cashiers will receive a JSON Web Token (JWT) upon successful login.

🌐 API Endpoints
AuthController
POST /api/auth/login: Authenticate users and receive a JWT token for access.
BankController
GET /api/bank/me/{id}: Retrieve customer information by providing their ID.
GET /api/bank/account/{id}/{limit}/{offset}: Retrieve account transactions with pagination.
Development and Customization
If you want to customize or extend the application, you can do so by modifying the source code. The application uses ASP.NET Core and Entity Framework for backend development. Feel free to explore and make changes as needed.

Transaction Checker Report
The transaction checker saves potentially fraudulent transactions to a file at the following location:

mathematica
Copy code
Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)/rapport.txt
License
This project is licensed under the MIT License. See the LICENSE file for details.

Contributors
Your Name - Project Lead
Acknowledgments
Special thanks to Your Acknowledgment for their contributions and support.
