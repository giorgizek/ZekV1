using System;
using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;
using Zek.Model.Dictionary;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Zek.Model.Accounting
{
    public class Account : BaseModel<int>
    {
        public int PersonId { get; set; }
        public Person.Person Person { get; set; }

        public string FriendlyName { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }



        public short TypeId { get; set; }
        public AccountType AccountType { get; set; }

        public decimal Balance { get; set; }
        //public decimal BalanceInGel { get; }

        //bool CanBePrimary



        //paymentOperationTypes ["4.31.01.06-01", "4.31.01.11-01", "4.31.01.16-01", "4.31.01.17-01", "4.31.03.02-01", "4.31.03.06-01",…]

        public string Iban { get; set; }



        public ISO4217 CurrencyId { get; set; }


        public bool IsPrimary { get; set; }
        public bool IsHidden { get; set; }
        public bool IsClosed { get; set; }
    }

    public class AccountMap : BaseModelMap<Account, int>
    {
        public AccountMap(ModelBuilder builder) : base(builder)
        {
            ToTable("T_Account", "Accounting");

            Property(t => t.Balance).ForSqlServerHasColumnType("money").HasDefaultValue(0m);
            Property(t => t.Iban).HasMaxLength(34);
            HasIndex(t => t.Iban);

            Property(t => t.FriendlyName).HasMaxLength(50);

            HasOne(t => t.AccountType).WithMany().HasForeignKey(t => t.TypeId).OnDelete(DeleteBehavior.Restrict);

            Property(a => a.OpenDate).ForSqlServerHasColumnType("date").ForSqlServerHasDefaultValueSql("getdate()");
            Property(a => a.CloseDate).ForSqlServerHasColumnType("date");

            Property(a => a.IsPrimary).HasDefaultValue(false);

            Property(a => a.IsHidden).HasDefaultValue(false);

            Property(t => t.IsClosed).HasComputedColumnSql("CAST(CASE WHEN [CloseDate] IS NOT NULL THEN (1) ELSE (0) END AS BIT)");
            HasIndex(t => t.IsClosed);
        }
    }
}
