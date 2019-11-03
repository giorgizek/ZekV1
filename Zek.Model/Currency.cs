using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class Currency
    {
        public Currency()
        {
            Translates = new HashSet<CurrencyTranslate>();
        }

        public byte Id { get; set; }

        public string Code { get; set; }
        public string Symbol { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<CurrencyTranslate> Translates { get; set; }


        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreateDate { get; set; }


        public int? ModifierId { get; set; }
        public virtual User Modifier { get; set; }
        public DateTime? ModifidDate { get; set; }
    }



    public class CurrencyEntityConfiguration : EntityTypeConfiguration<Currency>
    {
        public CurrencyEntityConfiguration()
        {
            ToTable("DD_Currency", "Dictionary");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Code).IsUnicode(false).HasMaxLength(10).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Dictionary.DD_Currency_Code") { IsUnique = true }));
            Property(t => t.Symbol).HasMaxLength(10).IsRequired();

            Property(t => t.IsDeleted).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Currency_IsDeleted")));

            HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
            Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Currency_CreatorId")));
            Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
            Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Currency_ModifierId")));
            Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }




    public class CurrencyTranslate
    {
        public byte CurrencyId { get; set; }
        public byte CultureId { get; set; }

        public string Text { get; set; }
        public string MinorUnit { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Culture Culture { get; set; }
    }

    public class CurrencyTranslateEntityConfiguration : EntityTypeConfiguration<CurrencyTranslate>
    {
        public CurrencyTranslateEntityConfiguration()
        {
            ToTable("DT_Currency", "Translate");
            HasKey(t => new { t.CurrencyId, t.CultureId });

            Property(t => t.CurrencyId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_Currency_CurrencyId")));
            HasRequired(t => t.Currency).WithMany(t => t.Translates).HasForeignKey(t => t.CurrencyId).WillCascadeOnDelete(true);

            Property(t => t.CultureId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_Currency_CultureId")));
            HasRequired(t => t.Culture).WithMany().HasForeignKey(t => t.CultureId).WillCascadeOnDelete(false);

            Property(t => t.Text).IsRequired().HasMaxLength(400);
            Property(t => t.MinorUnit).IsRequired().HasMaxLength(400);
        }
    }
}
