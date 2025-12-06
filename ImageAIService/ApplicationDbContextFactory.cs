using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ImageAIService.Data;

namespace ImageAIService
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            const string ConnectionStringKey = "ConnectionStrings__DefaultConnection";

            var connectionString = Environment.GetEnvironmentVariable(ConnectionStringKey);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException($"Environment variable '{ConnectionStringKey}' not found. Ensure it's set in your environment or when running dotnet ef commands.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}