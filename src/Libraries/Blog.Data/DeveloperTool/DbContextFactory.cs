using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog.Data.DeveloperTool
{
    /// <summary>
    /// for migrations scripts:
    ///_> dotnet ef migrations add Initial
    ///_> dotnet ef database update
    ///_>#dotnet ef database update Initial
    /// </summary>
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            //Database connection string
            var connectionString = "";
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Blog.Data"));
            return new AppDbContext(builder.Options);
        }
    }
}
