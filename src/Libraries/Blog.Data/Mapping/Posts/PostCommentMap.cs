using Blog.Core.Domain.Posts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Mapping.Posts
{
    public class PostCommentMap : EntityTypeConfiguration<PostComment>
    {
        public override void Configure(EntityTypeBuilder<PostComment> b)
        {
            b.ToTable(nameof(PostComment));
            b.HasKey(x => x.Id);

            b.Property(x => x.CommentText).HasMaxLength(500).IsRequired();
            b.Property(x => x.IsApproved).HasDefaultValue(true).IsRequired();

            b.HasOne(x => x.Post)
                .WithMany(x => x.PostComments)
                .HasForeignKey(x => x.PostId);

            base.Configure(b);
        }
    }
}
