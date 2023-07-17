using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupaLibrary.Services;
using TransactionChecker;
using WebbApp.Models;

var services = new ServiceCollection();

// Register the dependencies
services.AddTransient<ICheckerService, CheckerService>(); // Replace with your implementation
services.AddTransient<Checker>();

// Configure the database context and connection string
services.AddDbContext<BankAppDataContext>(options =>
    options.UseSqlServer("Server=localhost;Database=BankAppData;Trusted_Connection=True;MultipleActiveResultSets=true"));

// Build the service provider
using (var serviceProvider = services.BuildServiceProvider())
{
     // Resolve the required services
    var checker = serviceProvider.GetRequiredService<Checker>();

    // Call the Check() method
    checker.Check();
}
