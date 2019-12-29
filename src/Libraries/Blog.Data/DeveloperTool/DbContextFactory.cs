using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog.Data.DeveloperTool
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            //Databese connection string
            var connectionString = "Data Source=DPP0082;Initial Catalog=blog_api_db;Integrated Security=True";
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Blog.Data"));
            return new AppDbContext(builder.Options);
        }
    }
}
