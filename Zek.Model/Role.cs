using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class Role
    {
        //public static void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    var builder = modelBuilder.Entity<Role>();
        //    builder.ToTable("DD_Role", "Dictionary");
        //    builder.HasKey(t => t.Id);
        //    builder.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //    builder.Property(t => t.Name).IsUnicode(true).HasMaxLength(255).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Dictionary.DD_Role_Name") { IsUnique = true }));
            
        //    builder.HasOptional(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId);
        //    //builder.HasOptional(t => t.Creator).WithMany(u => u.Roles_Creator).HasForeignKey(t => t.CreatorId);
        //    builder.Property(t => t.CreatorId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Role_CreatorId")));
        //    builder.Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


        //    builder.HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId);
        //    //builder.HasOptional(t => t.Modifier).WithMany(u => u.Roles_Modifier).HasForeignKey(t => t.ModifierId);
        //    builder.Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Role_ModifierId")));
        //    builder.Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        //}


        //public Role()
        //{
            //Users = new HashSet<User>();
        //}

        public int Id { get; set; }

        public string Name { get; set; }


        public int? CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public DateTime CreateDate { get; set; }


        public int? ModifierId { get; set; }

        public virtual User Modifier { get; set; }

        public DateTime? ModifidDate { get; set; }

        //public virtual ICollection<User> Users { get; set; }
    }

    public class RoleEntityConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleEntityConfiguration()
        {
            ToTable("DD_Role", "Dictionary");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsUnicode(true).HasMaxLength(255).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Dictionary.DD_Role_Name") { IsUnique = true }));

            HasOptional(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId);
            //builder.HasOptional(t => t.Creator).WithMany(u => u.Roles_Creator).HasForeignKey(t => t.CreatorId);
            Property(t => t.CreatorId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Role_CreatorId")));
            Property(t => t.CreateDate).IsRequired().HasPrecision(0).HasColumnType("datetime2");


            HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId);
            //builder.HasOptional(t => t.Modifier).WithMany(u => u.Roles_Modifier).HasForeignKey(t => t.ModifierId);
            Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Dictionary.DD_Role_ModifierId")));
            Property(t => t.ModifidDate).HasPrecision(0).HasColumnType("datetime2");
        }
    }
}
