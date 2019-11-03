using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class Person
    {
        public int Id { get; set; }
        public bool IsLegal { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string FullNameEn { get; set; }

        public string PersonalNumber { get; set; }
        public string Passport { get; set; }

        public Gender GenderId { get; set; }
        public DateTime? BirthDate { get; set; }

        public short? ResidentId { get; set; }
        public Country Resident { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }


        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreateDate { get; set; }


        public int? ModifierId { get; set; }
        public virtual User Modifier { get; set; }
        public DateTime? ModifidDate { get; set; }
    }
    public class PersonEntityConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonEntityConfiguration()
        {
            ToTable("T_Person", "Person");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.FirstName).IsUnicode(true).HasMaxLength(100).IsRequired();
            Property(t => t.LastName).IsUnicode(true).HasMaxLength(150).IsRequired();
            Property(t => t.FullName).IsUnicode(true).HasMaxLength(255).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Person.T_Person_FullName")));

            Property(t => t.FirstNameEn).IsUnicode(true).HasMaxLength(100).IsRequired();
            Property(t => t.LastNameEn).IsUnicode(true).HasMaxLength(150).IsRequired();
            Property(t => t.FullNameEn).IsUnicode(true).HasMaxLength(255).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Person.T_Person_FullNameEn")));

            Property(t => t.PersonalNumber).IsUnicode(true).HasMaxLength(50).IsRequired();
            Property(t => t.Passport).IsUnicode(true).HasMaxLength(50).IsRequired();

            Property(t => t.BirthDate).HasPrecision(0).HasColumnType("date");

            HasOptional(t => t.Resident).WithMany().HasForeignKey(t => t.ResidentId).WillCascadeOnDelete(false);
            Property(t => t.ResidentId).IsOptional().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Person.T_Person_ResidentId")));

            HasRequired(t => t.Address).WithMany().HasForeignKey(t => t.AddressId).WillCascadeOnDelete(false);
            Property(t => t.AddressId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Person.T_Person_AddressId")));

            HasRequired(t => t.Contact).WithMany().HasForeignKey(t => t.ContactId).WillCascadeOnDelete(false);
            Property(t => t.ContactId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Person.T_Person_ContactId")));


            HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
            Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Person.T_Person_CreatorId")));
            Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
            Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Person.T_Person_ModifierId")));
            Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }



}
