using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Mapping
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}
