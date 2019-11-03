using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class Menu
    {
        public Menu()
        {
            Children = new HashSet<Menu>();
            Translates = new HashSet<MenuTranslate>();
        }


        public int Id { get; set; }

        public int? ParentId { get; set; }

        public virtual Menu Parent { get; set; }
        public virtual ICollection<Menu> Children { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string Url { get; set; }

        public virtual ICollection<MenuTranslate> Translates { get; set; }


        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public DateTime CreateDate { get; set; }


        public int? ModifierId { get; set; }

        public virtual User Modifier { get; set; }

        public DateTime? ModifidDate { get; set; }
    }


    public class MenuEntityConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuEntityConfiguration()
        {
            ToTable("DD_Menu", "Dictionary");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasOptional(t => t.Parent).WithMany(t => t.Children).HasForeignKey(t => t.ParentId).WillCascadeOnDelete(false);
            Property(t => t.ParentId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Menu_ParentID")));

            Property(t => t.ControllerName).IsUnicode(true).HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Menu_ControllerName_ActionName") { Order = 1 }));
            Property(t => t.ActionName).IsUnicode(true).HasMaxLength(200).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Menu_ControllerName_ActionName") { Order = 2 }));
            Property(t => t.Url).IsUnicode(true).HasMaxLength(1024);

            HasRequired(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).WillCascadeOnDelete(false);
            Property(t => t.CreatorId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Menu_CreatorId")));
            Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).WillCascadeOnDelete(false);
            Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Menu_ModifierId")));
            Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }





    public class MenuTranslate
    {
        public int MenuId { get; set; }
        public byte CultureId { get; set; }

        public string Text { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual Culture Culture { get; set; }
    }

    public class MenuTranslateEntityConfiguration : EntityTypeConfiguration<MenuTranslate>
    {
        public MenuTranslateEntityConfiguration()
        {
            ToTable("DT_Menu", "Translate");
            HasKey(t => new { t.MenuId, t.CultureId });

            Property(t => t.MenuId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_Menu_MenuId")));
            HasRequired(t => t.Menu).WithMany(t => t.Translates).HasForeignKey(t => t.MenuId).WillCascadeOnDelete(true);

            Property(t => t.CultureId).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_Menu_CultureId")));
            HasRequired(t => t.Culture).WithMany().HasForeignKey(t => t.CultureId).WillCascadeOnDelete(false);

            Property(t => t.Text).IsRequired().HasMaxLength(400).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Translate.DT_Menu_Text")));

        }
    }
}
