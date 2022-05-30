using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using TNAI.Model.Entities;

namespace TNAI.Model.Configurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Text).HasMaxLength(300);
            HasRequired(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId);
        }
    }
}
