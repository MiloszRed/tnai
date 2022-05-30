using System.Data.Entity.ModelConfiguration;

using TNAI.Model.Entities;

namespace TNAI.Model.Configurations
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption
                                                  .Identity);
            HasKey(x => x.Id);

            Property(x => x.Title).HasMaxLength(50);
        }
    }
}
