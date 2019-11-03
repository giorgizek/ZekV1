using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zek.Data.Entity
{
    //public interface IEntityTypeConfiguration<TEntity> where TEntity : class
    //{
    //void Map(EntityTypeBuilder<TEntity> builder);
    //}

    public class EntityTypeMap<TEntity> where TEntity : class
    {
        public EntityTypeMap(ModelBuilder builder)
        {
            EntityTypeBuilder = builder.Entity<TEntity>();
            //InternalOnModelCreating();

            
        }



        public EntityTypeBuilder<TEntity> EntityTypeBuilder { get; }

        public EntityTypeBuilder<TEntity> ToEntityTable(bool autoSchema = false)
        {
            if (!autoSchema)
                return EntityTypeBuilder.ToTable($"T_{typeof(TEntity).Name}");

            var type = typeof(TEntity);

            var index = type.Namespace.LastIndexOf('.');
            var schema = index != -1
                ? type.Namespace.Substring(index + 1)
                : type.Namespace;

            return EntityTypeBuilder.ToTable($"T_{type.Name}", schema);
        }
        public EntityTypeBuilder<TEntity> ToEntityTable(string schema) => EntityTypeBuilder.ToTable($"T_{typeof(TEntity).Name}", schema);
        public EntityTypeBuilder<TEntity> ToDictionaryTable() => ToDictionaryTable("Dictionary");
        public EntityTypeBuilder<TEntity> ToDictionaryTable(string schema) => EntityTypeBuilder.ToTable($"DD_{typeof(TEntity).Name}", schema);
        public EntityTypeBuilder<TEntity> ToTranslateTable() => ToTranslateTable("Translate");
        public EntityTypeBuilder<TEntity> ToTranslateTable(string schema)
        {
            var name = typeof(TEntity).Name;
            if (name.EndsWith("Translate", StringComparison.CurrentCultureIgnoreCase))
            {
                name = name.Substring(0, name.Length - 9);
            }
            return EntityTypeBuilder.ToTable($"DT_{name}", schema);
        }

        public EntityTypeBuilder<TEntity> ToTable(string name) => EntityTypeBuilder.ToTable(name);
        public EntityTypeBuilder<TEntity> ToTable(string name, string schema) => EntityTypeBuilder.ToTable(name, schema);

        
        public PropertyBuilder<TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression) => EntityTypeBuilder.Property(propertyExpression);

        public KeyBuilder HasKey(Expression<Func<TEntity, object>> keyExpression) => EntityTypeBuilder.HasKey(keyExpression);
        public IndexBuilder HasIndex(Expression<Func<TEntity, object>> indexExpression) => EntityTypeBuilder.HasIndex(indexExpression);

        public CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> collection = null) where TRelatedEntity : class => EntityTypeBuilder.HasMany(collection);
        public ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>> reference = null) where TRelatedEntity : class => EntityTypeBuilder.HasOne(reference);

        //private void InternalOnModelCreating()
        //{
        //    OnModelCreating(EntityTypeBuilder);
        //}
        //protected virtual void OnModelCreating(EntityTypeBuilder<TEntity> builder)
        //{

        //}
    }
}
