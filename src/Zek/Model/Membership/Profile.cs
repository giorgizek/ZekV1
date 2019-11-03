using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Data.Entity;
using Zek.Model.Membership.Identity;

namespace Zek.Model.Membership
{
    public class Profile
    {
        public int Id { get; set; }
        public User User { get; set; }


        public int PersonId { get; set; }
        public Person.Person Person { get; set; }

        //public int AddressId { get; set; }
        //public Address Address { get; set; }

        //public int ContactId { get; set; }
        //public Contact.Contact Contact { get; set; }
    }

    public class ProfileMap : EntityTypeMap<Profile>
    {
        public ProfileMap(ModelBuilder builder) : base(builder)
        {
            ToTable("T_Profile", "Membership");
            HasKey(t => t.Id);

            HasOne(t => t.User).WithOne().HasForeignKey<Profile>(t => t.Id).OnDelete(DeleteBehavior.Cascade);
            HasOne(t => t.Person).WithMany().HasForeignKey(t => t.PersonId).OnDelete(DeleteBehavior.Restrict);
            //HasOne(t => t.Address).WithMany().HasForeignKey(t => t.AddressId).OnDelete(DeleteBehavior.Restrict);
            //HasOne(t => t.Contact).WithMany().HasForeignKey(t => t.ContactId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
