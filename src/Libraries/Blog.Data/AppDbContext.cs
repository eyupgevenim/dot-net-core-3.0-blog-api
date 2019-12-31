using Blog.Core;
using Blog.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Blog.Data
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dynamically load all entity and query type configurations
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type =>(type.BaseType?.IsGenericType ?? false) 
                && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

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
        public bool EnsureCreated()
        {
            return base.Database.EnsureCreated();
        }

        /// <summary>
        /// Get Connection String
        /// </summary>
        public string ConnectionString => base.Database.GetDbConnection().ConnectionString;
    }
}
