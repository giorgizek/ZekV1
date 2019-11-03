using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class Culture
    {
        public byte Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class CultureEntityConfiguration : EntityTypeConfiguration<Culture>
    {
        public CultureEntityConfiguration()
        {
            ToTable("DD_Culture", "Dictionary");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Code).HasMaxLength(10).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Dictionary.DD_Culture_Code") { IsUnique = true }));
            Property(t => t.Name).IsUnicode(true).HasMaxLength(200).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Dictionary.DD_Culture_Name") { IsUnique = true }));
        }
    }
}
