using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Data.Entity;
using Zek.Model.Dictionary;

namespace Zek.Model.Contact
{
    public class Address
    {
        public int Id { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string PostalCode { get; set; }

        //todo public DbGeography Location { get; set; }
    }


    public class AddressMap : EntityTypeMap<Address>
    {
        public AddressMap(ModelBuilder builder) : base(builder)
        {
            ToTable("T_Address", "Contact");
            HasKey(t => t.Id);
            Property(t => t.Id).ValueGeneratedOnAdd();


            HasIndex(t => t.CityId);
            HasOne(t => t.City).WithMany().HasForeignKey(t => t.CityId).OnDelete(DeleteBehavior.Restrict);



            Property(t => t.Street).HasMaxLength(400).IsRequired();
            HasIndex(t => t.Street);
            Property(t => t.HouseNumber).HasMaxLength(25);
            Property(t => t.PostalCode).HasMaxLength(25);
        }
    }
}
