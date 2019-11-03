using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class UserRole
    {
        //public static void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    var builder = modelBuilder.Entity<UserRole>();
        //    builder.ToTable("T_UserRole", "Membership");
        //    builder.HasKey(t => t.Id);
        //    builder.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        //    builder.HasRequired(t => t.User).WithMany().HasForeignKey(t => t.UserId);
        //    builder.Property(t => t.UserId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Membership.T_UserRole_UserId_RoleId") { IsUnique = true, Order = 1 }));
        //    builder.HasRequired(t => t.Role).WithMany().HasForeignKey(t => t.RoleId);
        //    builder.Property(t => t.RoleId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Membership.T_UserRole_UserId_RoleId") { IsUnique = true, Order = 2 }));

        //    builder.HasOptional(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId);
        //    builder.Property(t => t.CreatorId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Membership.T_UserRole_CreatorId")));
        //    builder.HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId);
        //    builder.Property(t => t.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Membership.T_UserRole_ModifierId")));
        //}

        
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }



        public int? CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public DateTime CreateDate { get; set; }


        public int? ModifierId { get; set; }

        public virtual User Modifier { get; set; }

    }

    public class UserRoleEntityConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleEntityConfiguration()
        {
            ToTable("T_UserRole", "Membership");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(t => t.User).WithMany().HasForeignKey(t => t.UserId);
            Property(t => t.UserId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Membership.T_UserRole_UserId_RoleId") { IsUnique = true, Order = 1 }));
            HasRequired(t => t.Role).WithMany().HasForeignKey(t => t.RoleId);
            Property(t => t.RoleId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Membership.T_UserRole_UserId_RoleId") { IsUnique = true, Order = 2 }));

            HasOptional(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId);
            Property(t => t.CreatorId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Membership.T_UserRole_CreatorId")));
            HasOptional(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId);
            Property(t => t.ModifierId).HasColumnAnnotation("Index",new IndexAnnotation(new IndexAttribute("IX_Membership.T_UserRole_ModifierId")));

        
        }
    }
}
