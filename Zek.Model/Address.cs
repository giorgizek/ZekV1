using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Spatial;

namespace Zek.Model
{
    public class Address
    {
        public int Id { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string PostalCode { get; set; }

        public DbGeography Location { get; set; }


        //public int CreatorId { get; set; }
        //public virtual User Creator { get; set; }
        //public DateTime CreateDate { get; set; }


        //public int? ModifierId { get; set; }
        //public virtual User Modifier { get; set; }
        //public DateTime? ModifidDate { get; set; }
    }


    public class AddressEntityConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressEntityConfiguration()
        {
            ToTable("T_Address", "Contact");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            HasRequired(t => t.City).WithMany().HasForeignKey(t => t.CityId).WillCascadeOnDelete(false); 
            Property(t => t.CityId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Contact.T_Address_CityId")));


            Property(t => t.Street).IsUnicode(true).HasMaxLength(400).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Contact.T_Address_Street")));
            Property(t => t.HouseNumber).IsUnicode(true).HasMaxLength(25);
            Property(t => t.PostalCode).IsUnicode(true).HasMaxLength(25);



            //HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
            //Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Contact.T_Address_CreatorId")));
            //Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            //HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
            //Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Contact.T_Address_ModifierId")));
            //Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }
}
