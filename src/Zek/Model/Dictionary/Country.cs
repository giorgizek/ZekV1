using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Data.Entity;
using Zek.Model.Membership.Identity;
using Zek.Model.Base;

namespace Zek.Model.Dictionary
{
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }

        public List<City> Cities { get; set; }
        public List<CountryTranslate> Translates { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public DateTime CreateDate { get; set; }

        public int? ModifierId { get; set; }
        public User Modifier { get; set; }
        public DateTime? ModifidDate { get; set; }
    }


    public class CountryMap : EntityTypeMap<Country>
    {
        public CountryMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DD_Country", "Dictionary");
            HasKey(t => t.Id);
            Property(t => t.Id).ValueGeneratedNever();

            Property(t => t.Code).HasMaxLength(10).IsRequired();
            HasIndex(t => t.Code).IsUnique();

            Property(t => t.IsDeleted).HasDefaultValue(false);

            HasIndex(t => t.IsDeleted);

            HasOne(t => t.Creator).WithMany().HasForeignKey(t => t.CreatorId).OnDelete(DeleteBehavior.Restrict);

            Property(t => t.CreateDate).IsRequired().ForSqlServerHasColumnType("datetime2(0)").ForSqlServerHasDefaultValueSql("sysdatetime()");

            HasOne(t => t.Modifier).WithMany().HasForeignKey(t => t.ModifierId).OnDelete(DeleteBehavior.Restrict);

            Property(t => t.ModifidDate).ForSqlServerHasColumnType("datetime2(0)");
        }
    }


    public class CountryTranslate : TranslateModel<Country, int>
    {
        //public short CountryId { get; set; }
        //public Country Country { get; set; }
    }

    public class CountryTranslateMap : TranslateModelMap<CountryTranslate, Country, int>
    {
        public CountryTranslateMap(ModelBuilder builder) : base(builder)
        {
            ToTable("DT_Country", "Translate");
            //HasKey(t => new { t.CountryId, t.CultureId });

            //HasIndex(t => t.CountryId);
            //HasOne(t => t.Country).WithMany().HasForeignKey(t => t.CountryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}