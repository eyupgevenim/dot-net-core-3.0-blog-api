using Blog.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapping
{
    public class EntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> b)
        {
            this.PostConfigure(b);
        }

        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }
}
