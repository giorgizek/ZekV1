using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class City
    {
        public City()
        {
            Translates = new HashSet<CityTranslate>();
        }


        public int Id { get; set; }

        public short CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<CityTranslate> Translates { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreateDate { get; set; }


        public int? ModifierId { get; set; }
        public virtual User Modifier { get; set; }
        public DateTime? ModifidDate { get; set; }
    }


    public class CityEntityConfiguration : EntityTypeConfiguration<City>
    {
        public CityEntityConfiguration()
        {
            ToTable("DD_City", "Dictionary");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(t => t.Country).WithMany(t => t.Cities).HasForeignKey(t => t.CountryId).WillCascadeOnDelete(false);
            Property(t => t.CountryId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_City_CountryId")));

            Property(t => t.IsDeleted).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_City_IsDeleted")));


            HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
            Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_City_CreatorId")));
            Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
            Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_City_ModifierId")));
            Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }




    public class CityTranslate
    {
        public int CityId { get; set; }
        public byte CultureId { get; set; }

        public string Text { get; set; }

        public virtual City City { get; set; }

        public virtual Culture Culture { get; set; }
    }

    public class CityTranslateEntityConfiguration : EntityTypeConfiguration<CityTranslate>
    {
        public CityTranslateEntityConfiguration()
        {
            ToTable("DT_City", "Translate");
            HasKey(t => new { t.CityId, t.CultureId });

            Property(t => t.CityId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_City_CityId")));
            HasRequired(t => t.City).WithMany(t => t.Translates).HasForeignKey(t => t.CityId).WillCascadeOnDelete(true);

            Property(t => t.CultureId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_City_CultureId")));
            HasRequired(t => t.Culture).WithMany().HasForeignKey(t => t.CultureId).WillCascadeOnDelete(false);

            Property(t => t.Text).IsRequired().HasMaxLength(400).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_City_Text")));
        }
    }
}