using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Data.Entity;
using Zek.Model.Membership.Identity;

namespace Zek.Model.Accounting
{
    public class TransactionEcomm<TUser>
        where TUser : User
    {
        public int TransactionId { get; set; }
        public Transaction<TUser> Transaction { get; set; }

        public int EcommId { get; set; }
        public Ecomm Ecomm { get; set; }
    }

    public class TransactionEcommMap<TUser> : EntityTypeMap<TransactionEcomm<TUser>>
        where TUser : User
    {
        public TransactionEcommMap(ModelBuilder builder) : base(builder)
        {
            ToTable("T_TransactionEcomm", "Accounting");
            HasKey(t => t.TransactionId);

            HasOne(t => t.Transaction).WithOne().HasForeignKey<TransactionEcomm<TUser>>(t => t.TransactionId).OnDelete(DeleteBehavior.Restrict);

            HasOne(t => t.Ecomm).WithOne().HasForeignKey<TransactionEcomm<TUser>>(t => t.EcommId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
