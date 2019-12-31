using Blog.Core;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public interface IDbContext
    {
        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Ensures that the database for the context exists. If it exists, no action is
        /// taken. If it does not exist then the database and all its schema are created.
        /// If the database exists, then no effort is made to ensure it is compatible with
        /// the model for this context.
        /// Note that this API does not use migrations to create the database. In addition,
        /// the database that is created cannot be later updated using migrations. If you
        /// are targeting a relational database and using migrations, you can use the DbContext.Database.Migrate()
        /// method to ensure the database is created and all migrations are applied.
        /// </summary>
        bool EnsureCreated();

        /// <summary>
        /// Get Connection String
        /// </summary>
        string ConnectionString { get; }
    }
}
