using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Zek.Model.Membership.Identity
{
    public class IdentityDbContext : IdentityDbContext<User, Role>
    {
        public IdentityDbContext()
        {
        }

        public IdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }


    public class IdentityDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, int>//, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> 
        where TUser : User
        where TRole : Role
    {
        public IdentityDbContext()
        {
        }

        public IdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ReSharper disable ObjectCreationAsStatement
            new UserMap<TUser>(builder);
            new RoleMap<TRole>(builder);
            // ReSharper restore ObjectCreationAsStatement

            //new UserRoleMap(builder);
            builder.Entity<IdentityUserRole<int>>().ToTable("T_UserRole", "Membership");
            //new UserLoginMap(builder);
            builder.Entity<IdentityUserLogin<int>>().ToTable("T_UserLogin", "Membership");
            //new UserClaimMap(builder);
            builder.Entity<IdentityUserClaim<int>>().ToTable("T_UserClaim", "Membership");
            //new RoleClaimMap(builder);
            builder.Entity<IdentityRoleClaim<int>>().ToTable("T_RoleClaim", "Membership");
            //new UserTokenMap(builder);
            builder.Entity<IdentityUserToken<int>>().ToTable("T_UserToken", "Membership");
        }
    }

    //IdentityDbContext<ApplicationUser, ApplicationRole, long>
}