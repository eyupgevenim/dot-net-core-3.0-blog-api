using Blog.Core.Domain.Posts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Mapping.Posts
{
    public class PostMap : EntityTypeConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> b)
        {
            b.ToTable(nameof(Post));
            b.HasKey(x => x.Id);

            b.Property(x => x.Title).HasMaxLength(250).IsRequired();
            b.Property(x => x.Body).IsRequired();
            b.Property(x => x.BodyOverview).HasMaxLength(500).IsRequired();
            b.Property(x => x.Tags).HasMaxLength(100).IsRequired();
            b.Property(x => x.MetaTitle).HasMaxLength(100);
            b.Property(x => x.MetaKeywords).HasMaxLength(50);
            b.Property(x => x.MetaDescription).HasMaxLength(150);

            b.Property(x => x.StartDateUtc).HasDefaultValueSql("GETUTCDATE()").IsRequired();
            b.Property(x => x.CreatedOnUtc).HasDefaultValueSql("GETUTCDATE()").IsRequired();
            b.Property(x => x.AllowComments).HasDefaultValue(true).IsRequired();
            b.Property(x => x.Deleted).HasDefaultValue(false).IsRequired();

            base.Configure(b);
        }
    }
}
