using Microsoft.EntityFrameworkCore;
using System;
using Zek.Data.Entity;

namespace Zek.Model.Membership.Identity
{
    public class User : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUser<int>//, UserClaim, UserRole, UserLogin>
    {
        public User()
        {
            RowId = Guid.NewGuid();
        }
        public User(string userName) : this()
        {
            UserName = userName;
        }


        //public async Task<System.Security.Claims.ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNetCore.Identity.UserManager<User> manager)
        //{
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    return userIdentity;
        //}


        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public string FullName { get; set; }




        public int? CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

        public int? ModifierId { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Guid RowId { get; set; }



    }



    public class UserMap : UserMap<User>
    {
        public UserMap(ModelBuilder builder) : base(builder)
        {
        }
    }

    public class UserMap<TEntity> : EntityTypeMap<TEntity>
        where TEntity : User
    {
        public UserMap(ModelBuilder builder) : base(builder)
        {
            ToTable("T_User", "Membership");
            Property(t => t.Id).ValueGeneratedOnAdd();

            //Property(t => t.FirstName).HasMaxLength(100).IsRequired();
            //Property(t => t.LastName).HasMaxLength(150).IsRequired();

            //Property(t => t.FullName).HasMaxLength(255).IsRequired();
            //Property(t => t.FullName).HasComputedColumnSql("rtrim(ltrim(rtrim([LastName]) + ' ' + ltrim([FirstName])))");
            //HasIndex(t => t.FullName);


            HasIndex(t => t.CreatorId);
            //HasOne(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId);

            Property(t => t.CreateDate).ForSqlServerHasColumnType("datetime2(0)").ForSqlServerHasDefaultValueSql("sysdatetime()");

            HasIndex(t => t.ModifierId);
            //HasOne(t => t.Modifier).WithMany(/*u => u.Roles_Modifier*//*).HasForeignKey(t => t.ModifierId);

            Property(t => t.ModifiedDate).ForSqlServerHasColumnType("datetime2(0)");

            Property(t => t.RowId).ForSqlServerHasDefaultValueSql("newid()");
            HasIndex(t => t.RowId).IsUnique();
        }
    }
}