using System.Data.Entity.ModelConfiguration;

using TNAI.Model.Entities;

namespace TNAI.Model.Configurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption
                                                  .Identity);
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50);
        }
    }
}
