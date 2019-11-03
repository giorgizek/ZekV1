using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Translates = new HashSet<CountryTranslate>();
        }

        public short Id { get; set; }

        public string Code { get; set; }

        public bool IsDeleted { get; set; }



        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<CountryTranslate> Translates { get; set; }


        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreateDate { get; set; }


        public int? ModifierId { get; set; }
        public virtual User Modifier { get; set; }
        public DateTime? ModifidDate { get; set; }
    }


    public class CountryEntityConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryEntityConfiguration()
        {
            ToTable("DD_Country", "Dictionary");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Code).IsUnicode(false).HasMaxLength(10).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Dictionary.DD_Country_Code") { IsUnique = true }));


            Property(t => t.IsDeleted).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Country_IsDeleted")));

            HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
            Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Country_CreatorId")));
            Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
            Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Country_ModifierId")));
            Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }




    public class CountryTranslate
    {
        public short CountryId { get; set; }
        public byte CultureId { get; set; }

        public string Text { get; set; }

        public virtual Country Country { get; set; }

        public virtual Culture Culture { get; set; }
    }

    public class CountryTranslateEntityConfiguration : EntityTypeConfiguration<CountryTranslate>
    {
        public CountryTranslateEntityConfiguration()
        {
            ToTable("DT_Country", "Translate");
            HasKey(t => new { t.CountryId, t.CultureId });

            Property(t => t.CountryId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_Country_CountryId")));
            HasRequired(t => t.Country).WithMany(t => t.Translates).HasForeignKey(t => t.CountryId).WillCascadeOnDelete(true);

            Property(t => t.CultureId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_Country_CultureId")));
            HasRequired(t => t.Culture).WithMany().HasForeignKey(t => t.CultureId).WillCascadeOnDelete(false);

            Property(t => t.Text).IsRequired().HasMaxLength(400).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_Country_Text")));
        }
    }
}
