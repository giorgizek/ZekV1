using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Base
{
    public class PocoModel<TId>
    {
        public TId Id { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

        public int? ModifierId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class PocoModelMap<TEntity, TId> : EntityTypeMap<TEntity>
        where TEntity : PocoModel<TId>
    {
        public PocoModelMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder)
        {
            HasKey(t => t.Id);
            if (valueGeneratedOnAdd.GetValueOrDefault(true))
                Property(t => t.Id).ValueGeneratedOnAdd();
            else
                Property(t => t.Id).ValueGeneratedNever();

            Property(t => t.IsDeleted).HasDefaultValue(false);
            HasIndex(t => t.IsDeleted);

            HasIndex(t => t.CreatorId);
            Property(t => t.CreateDate).ForSqlServerHasColumnType("datetime2(0)").ForSqlServerHasDefaultValueSql("sysdatetime()");

            HasIndex(t => t.ModifierId);
            Property(t => t.ModifiedDate).ForSqlServerHasColumnType("datetime2(0)");
        }
    }
}