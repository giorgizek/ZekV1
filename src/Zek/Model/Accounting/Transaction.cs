using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Model.Base;
using Zek.Model.Dictionary;
using Zek.Model.Membership.Identity;

namespace Zek.Model.Accounting
{
    public class BankTransaction<TUser> : Transaction<TUser>
        where TUser : User
    {
        /// <summary>
        /// If amount is recieved then IsCredit=true. If amount is send to other account then IsCredit=false
        /// </summary>
        public bool IsCredit { get; set; }

        //public decimal IncomingAmount { get; set; }
        //public byte IncomingCurrencyId { get; set; }
        //public Currency IncomingCurrency { get; set; }
        //public decimal ExchangeRate { get; set; }
    }
    public class Transaction<TUser> : TransactionBase<TUser>
        where TUser : User
    {
        public Account Account { get; set; }
        public TransactionType TransactionType { get; set; }
        public Person.Person Person { get; set; }
    }
    public class TransactionBase<TUser> : BaseModel<int, TUser>
        where TUser : User
    {
        public int AccountId { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public ISO4217 CurrencyId { get; set; }

        public PaymentMethod PaymentMethodId { get; set; }

        public byte TypeId { get; set; }

        //public byte StatusId { get; set; }
        public TransactionStatus StatusId { get; set; }

        public int? PersonId { get; set; }

        public string Description { get; set; }
    }


    public class BankTransactionMap<TUser> : TransactionMap<BankTransaction<TUser>, TUser>
        where TUser : User
    {
        public BankTransactionMap(ModelBuilder builder) : base(builder)
        {
            HasIndex(t => t.IsCredit);
        }
    }

    public class TransactionMap<TUser> : TransactionMap<Transaction<TUser>, TUser>
        where TUser : User
    {
        public TransactionMap(ModelBuilder builder) : base(builder)
        {
        }
    }
    public class TransactionMap<TEntity, TUser> : TransactionBaseMap<TEntity, TUser>
        where TEntity : Transaction<TUser>
        where TUser : User
    {
        public TransactionMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            HasOne(t => t.TransactionType).WithMany().HasForeignKey(t => t.TypeId).OnDelete(DeleteBehavior.Restrict);
            HasOne(t => t.Person).WithMany().HasForeignKey(t => t.PersonId).OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class TransactionBaseMap<TEntity, TUser> : BaseModelMap<TEntity, int, TUser>
        where TEntity : TransactionBase<TUser>
        where TUser : User
    {
        public TransactionBaseMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            ToTable("T_Transaction", "Accounting");
            Property(t => t.Date).ForSqlServerHasColumnType("datetime2(0)").ForSqlServerHasDefaultValueSql("sysdatetime()");
            HasIndex(t => t.Date);

            HasIndex(t => t.StatusId);
            //HasIndex(t => t.IsCredit);

            Property(t => t.Description).HasMaxLength(400);
        }
    }
}
