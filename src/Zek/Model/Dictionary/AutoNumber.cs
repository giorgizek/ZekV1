using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Dictionary
{
    public class AutoNumber
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    public class AutoNumberMap : EntityTypeMap<AutoNumber>
    {
        public AutoNumberMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DD_AutoNumber", "Config");
            Property(t => t.Name).HasMaxLength(400);
            HasKey(t => t.Name);
        }
    }
}
