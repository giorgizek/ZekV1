using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Model.Base;
using Zek.Model.Contact;
using Zek.Model.Dictionary;

namespace Zek.Model.Person
{
    public class Person : PersonBase
    {
        public PersonTitle Title { get; set; }
        public Country Resident { get; set; }
        public Address Address { get; set; }
        public Contact.Contact Contact { get; set; }
    }

    public class PersonBase : BaseModel<int>
    {
        public bool IsLegal { get; set; }

        public byte? TitleId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string FirstNameEn { get; set; }
        public string LastNameEn { get; set; }
        public string FullNameEn { get; set; }

        public string PersonalNumber { get; set; }
        public string Passport { get; set; }

        public Gender? GenderId { get; set; }
        public DateTime? BirthDate { get; set; }

        public int? ResidentId { get; set; }

        public int AddressId { get; set; }
        public int ContactId { get; set; }
    }

    public class PersonMap : PersonMap<Person>
    {
        public PersonMap(ModelBuilder builder) : base(builder)
        {
        }
    }

    public class PersonMap<TEntity> : PersonBaseMap<TEntity> where TEntity : Person
    {
        public PersonMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            HasOne(p => p.Title).WithMany().HasForeignKey(p => p.TitleId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Resident).WithMany().HasForeignKey(t => t.ResidentId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Address).WithMany().HasForeignKey(t => t.AddressId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Contact).WithMany().HasForeignKey(t => t.ContactId).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PersonBaseMap<TEntity> : BaseModelMap<TEntity, int> where TEntity : PersonBase
    {
        public PersonBaseMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            ToTable("T_Person", "Person");

            Property(t => t.FirstName).HasMaxLength(100).IsRequired();
            Property(t => t.LastName).HasMaxLength(150).IsRequired();

            //Property(t => t.FullName).HasMaxLength(255).IsRequired();
            Property(t => t.FullName).HasComputedColumnSql("rtrim(ltrim(rtrim([LastName]) + ' ' + ltrim([FirstName])))");
            HasIndex(t => t.FullName);


            Property(t => t.FirstNameEn).HasMaxLength(100).IsRequired();
            Property(t => t.LastNameEn).HasMaxLength(150).IsRequired();

            //Property(t => t.FullNameEn).HasMaxLength(255).IsRequired();
            HasIndex(t => t.FullNameEn);
            Property(t => t.FullNameEn).HasComputedColumnSql("rtrim(ltrim(rtrim([FirstNameEn]) + ' ' + ltrim([LastNameEn])))");

            Property(t => t.PersonalNumber).HasMaxLength(50).IsRequired();
            HasIndex(t => t.PersonalNumber);

            Property(t => t.Passport).HasMaxLength(50).IsRequired();
            HasIndex(t => t.Passport);

            Property(t => t.BirthDate).ForSqlServerHasColumnType("date");


        }
    }

}
