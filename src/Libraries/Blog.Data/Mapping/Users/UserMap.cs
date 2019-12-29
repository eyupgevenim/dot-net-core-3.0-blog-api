using Blog.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapping.Users
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> b)
        {
            b.ToTable(nameof(User));
            b.HasKey(x => x.Id);

            b.Property(x => x.FirstName).HasMaxLength(30);
            b.Property(x => x.LastName).HasMaxLength(30);
            b.Property(x => x.UserName).HasMaxLength(30).IsRequired();
            b.Property(x => x.Email).HasMaxLength(50).IsRequired();
            b.Property(x => x.HashedPassword).HasMaxLength(50).IsRequired();
            b.Property(x => x.Salt).HasMaxLength(50).IsRequired();

            b.Property(x => x.Active).HasDefaultValue(true).IsRequired();
            b.Property(x => x.CreatedOnUtc).HasDefaultValueSql("GETUTCDATE()").IsRequired();

            base.Configure(b);
        }
    }
}
