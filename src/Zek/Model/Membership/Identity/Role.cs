using Microsoft.EntityFrameworkCore;
using System;
using Zek.Data.Entity;

namespace Zek.Model.Membership.Identity
{
    public class Role : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<int>//, UserRole, Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>>
    {
        public Role()
        {
        }

        public Role(string roleName, string description = null) : this()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Name = roleName;
            Description = description;
        }


        public string Description { get; set; }

        public int? CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

        public int? ModifierId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }


    public class RoleMap : RoleMap<Role>
    {
        public RoleMap(ModelBuilder builder) : base(builder)
        {
        }
    }
    public class RoleMap<TEntity> : EntityTypeMap<TEntity> where TEntity : Role
    {
        public RoleMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DD_Role", "Dictionary");
            Property(t => t.Id).ValueGeneratedOnAdd();
            Property(t => t.Description).HasMaxLength(255);
            HasIndex(t => t.CreatorId);
            Property(t => t.CreateDate).ForSqlServerHasColumnType("datetime2(0)").ForSqlServerHasDefaultValueSql("sysdatetime()");
            HasIndex(t => t.ModifierId);
            Property(t => t.ModifiedDate).ForSqlServerHasColumnType("datetime2(0)");
        }
    }
}
