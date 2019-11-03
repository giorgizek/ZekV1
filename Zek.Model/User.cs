using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Zek.Model
{
    public class User
    {
        //public static void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    var builder = modelBuilder.Entity<User>();

        //    builder.ToTable("T_User", "Membership");
        //    builder.HasKey(t => t.Id);
        //    builder.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //    builder.Property(t => t.UserName).IsRequired().HasMaxLength(256);
        //    builder.Property(t => t.PasswordHash).IsRequired().HasMaxLength(128);
        //    builder.Property(t => t.PasswordSalt).IsRequired().HasMaxLength(32);
        //    builder.Property(t => t.PasswordQuestion).HasMaxLength(256);
        //    builder.Property(t => t.PasswordAnswer).HasMaxLength(128);
        //    builder.Property(t => t.Email).HasMaxLength(256).IsRequired();
        //    builder.Property(t => t.ExpiryDate).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.AllowedIp).HasMaxLength(255).IsUnicode(false);
        //    builder.Property(t => t.LastLoginIp).HasMaxLength(255).IsUnicode(false);
        //    builder.Property(t => t.RegIp).HasMaxLength(255).IsUnicode(false).IsRequired();
        //    builder.Property(t => t.LastLoginDate).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.LastPasswordChangedDate).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.LastLockoutDate).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.LastActivityDate).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.FailedPasswordAttemptWindowStart).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.FailedPasswordAnswerAttemptWindowStart).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.RestrictedDateFrom).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.RestrictedDateTo).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.CreateDate).HasPrecision(0).HasColumnType("datetime2");
        //    builder.Property(t => t.ModifiedDate).HasPrecision(0).HasColumnType("datetime2");




        //    //როცა არ გვინდა დამატებითი კლასის გაკეთება და გვინდა 2 ველიანი ცხრილის შექმნა მაშინ ეს იდეალური ვარიანტია
        //    //builder.HasMany(r => r.Roles).WithMany(t => t.Users).Map(ur =>
        //    //{
        //    //    ur.MapLeftKey("UserId");
        //    //    ur.MapRightKey("RoleId");
        //    //    ur.ToTable("T_UserRole", "Membership");
        //    //});


        //}

        //public User()
        //{
            //Created = new List<User>();
            //Roles = new HashSet<Role>();
            //Roles_Creator = new HashSet<Role>();
            //Roles_Modifier = new HashSet<Role>();
        //}

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        //[RequiredEx]
        //[StringLength(256)]
        public string UserName { get; set; }

        //[RequiredEx]
        //[StringLength(128)]
        public string PasswordHash { get; set; }

        //[RequiredEx]
        //[StringLength(32)]
        public string PasswordSalt { get; set; }

        public byte PasswordFormat { get; set; }

        //[StringLength(256)]
        public string PasswordQuestion { get; set; }

        //[StringLength(128)]
        public string PasswordAnswer { get; set; }

        //[RequiredEx]
        //[StringLength(256)]
        public string Email { get; set; }

        public bool IsEmailAllowed { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime ExpiryDate { get; set; }

        //[StringLength(255)]
        public string AllowedIp { get; set; }

        //[StringLength(255)]
        public string LastLoginIp { get; set; }

        //[RequiredEx]
        //[StringLength(255)]
        public string RegIp { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? LastLoginDate { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? LastPasswordChangedDate { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? LastLockoutDate { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? LastActivityDate { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? FailedPasswordAttemptWindowStart { get; set; }

        public int FailedPasswordAnswerAttemptCount { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? FailedPasswordAnswerAttemptWindowStart { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? RestrictedDateFrom { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? RestrictedDateTo { get; set; }

        public bool IsActive { get; set; }

        //[ForeignKey("Creator")]
        public int? CreatorId { get; set; }


        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        ////[ForeignKey("Modifier")]
        public int? ModifierId { get; set; }

        //[DateTimeColumn]
        //[DateTimeDisplayFormat]
        //[DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        //public Guid RowGuid { get; set; }




        //public virtual User Creator { get; set; }
        //public virtual User Modifier { get; set; }

        //public virtual ICollection<User> Created { get; set; }

        //public virtual ICollection<Role> Roles { get; set; }

        //public virtual ICollection<Role> Roles_Creator { get; set; }
        //public virtual ICollection<Role> Roles_Modifier { get; set; }
    }

    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            //Property(r => r.ModifierId).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Membership.T_UserRole_ModifierId")));

            ToTable("T_User", "Membership");
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Membership.T_UserRole_UserName") { IsUnique = true }));
            Property(t => t.PasswordHash).IsRequired().HasMaxLength(128);
            Property(t => t.PasswordSalt).IsRequired().HasMaxLength(32);
            Property(t => t.PasswordQuestion).HasMaxLength(256);
            Property(t => t.PasswordAnswer).HasMaxLength(128);
            Property(t => t.Email).HasMaxLength(256).IsRequired().HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AK_Membership.T_UserRole_Email") { IsUnique = true })); ;
            Property(t => t.ExpiryDate).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.AllowedIp).HasMaxLength(255).IsUnicode(false);
            Property(t => t.LastLoginIp).HasMaxLength(255).IsUnicode(false);
            Property(t => t.RegIp).HasMaxLength(255).IsUnicode(false).IsRequired();
            Property(t => t.LastLoginDate).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.LastPasswordChangedDate).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.LastLockoutDate).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.LastActivityDate).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.FailedPasswordAttemptWindowStart).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.FailedPasswordAnswerAttemptWindowStart).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.RestrictedDateFrom).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.RestrictedDateTo).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.CreateDate).HasPrecision(0).HasColumnType("datetime2");
            Property(t => t.ModifiedDate).HasPrecision(0).HasColumnType("datetime2");

            //როცა არ გვინდა დამატებითი კლასის გაკეთება და გვინდა 2 ველიანი ცხრილის შექმნა მაშინ ეს იდეალური ვარიანტია
            //builder.HasMany(r => r.Roles).WithMany(t => t.Users).Map(ur =>
            //{
            //    ur.MapLeftKey("UserId");
            //    ur.MapRightKey("RoleId");
            //    ur.ToTable("T_UserRole", "Membership");
            //});
        }
    }
}
