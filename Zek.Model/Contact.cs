using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class Contact
    {

        public int Id { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
        public string Fax3 { get; set; }

        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }

        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

        public string Url  { get; set; }


        //public int CreatorId { get; set; }
        //public virtual User Creator { get; set; }
        //public DateTime CreateDate { get; set; }


        //public int? ModifierId { get; set; }
        //public virtual User Modifier { get; set; }
        //public DateTime? ModifidDate { get; set; } 
    }


    public class ContactEntityConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactEntityConfiguration()
        {
            ToTable("T_Contact", "Contact");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Phone1).IsUnicode(false).HasMaxLength(25);
            Property(t => t.Phone2).IsUnicode(false).HasMaxLength(25);
            Property(t => t.Phone3).IsUnicode(false).HasMaxLength(25);

            Property(t => t.Fax1).IsUnicode(false).HasMaxLength(25);
            Property(t => t.Fax2).IsUnicode(false).HasMaxLength(25);
            Property(t => t.Fax3).IsUnicode(false).HasMaxLength(25);

            Property(t => t.Mobile1).IsUnicode(false).HasMaxLength(25);
            Property(t => t.Mobile2).IsUnicode(false).HasMaxLength(25);
            Property(t => t.Mobile3).IsUnicode(false).HasMaxLength(25);

            Property(t => t.Email1).IsUnicode(false).HasMaxLength(256);
            Property(t => t.Email2).IsUnicode(false).HasMaxLength(256);
            Property(t => t.Email3).IsUnicode(false).HasMaxLength(256);
            
            Property(t => t.Url).IsUnicode(true).HasMaxLength(1024);

            //HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
            //Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Contact.T_Contact_CreatorId")));
            //Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            //HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
            //Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Contact.T_Contact_ModifierId")));
            //Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }
}
