using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Zek.Model.Base;
using Zek.Model.Contact;
using Zek.Model.Dictionary;

namespace Zek.Model.Accounting
{
    public class Order : OrderBase
    {
        public Person.Person Customer { get; set; }


        public Address BillingAddress { get; set; }

        public Address ShippingAddress { get; set; }

        public List<OrderItem> Items { get; set; }
    }

    public class OrderBase : BaseModel<int>
    {
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }


        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal FreeShipping { get; set; }
        public decimal TotalBeforeTax { get; set; }
        public decimal Tax { get; set; }

        public decimal GrandTotal { get; set; }
        public ISO4217 CurrencyId { get; set; }


        /// <summary>
        /// Your billing address is where you receive your bank statement for the credit card.
        /// </summary>
        public int BillingAddressId { get; set; }

        /// <summary>
        /// The shipping address is where you want your items delivered
        /// </summary>
        public int ShippingAddressId { get; set; }


        public TransactionStatus StatusId { get; set; }
        //public OrderStatus OrderStatus { get; set; }


        public string Comment { get; set; }



        //todo Order public List<Transaction> Transactions { get; set; }
    }

    public class OrderMap : BaseModelMap<Order, int>
    {
        public OrderMap(ModelBuilder builder) : base(builder)
        {
            ToTable("T_Order", "Accounting");
            Property(o => o.OrderNumber).HasMaxLength(25).IsRequired();
            HasIndex(o => o.OrderNumber).IsUnique();

            Property(o => o.Date).ForSqlServerHasColumnType("date").ForSqlServerHasDefaultValueSql("getdate()");
            HasIndex(o => o.Date);

            HasOne(o => o.Customer).WithMany().HasForeignKey(t => t.CustomerId).OnDelete(DeleteBehavior.Restrict);

            Property(o => o.Subtotal).ForSqlServerHasColumnType("money");
            Property(o => o.Shipping).ForSqlServerHasColumnType("money").HasDefaultValue(0m);
            Property(o => o.FreeShipping).ForSqlServerHasColumnType("money").HasDefaultValue(0m);
            Property(o => o.TotalBeforeTax).HasComputedColumnSql("[Subtotal]+[Shipping]-[FreeShipping]");
            Property(o => o.Tax).ForSqlServerHasColumnType("money").HasDefaultValue(0m);
            Property(o => o.GrandTotal).HasComputedColumnSql("[Subtotal]+[Shipping]-[FreeShipping]+[Tax]");

            HasOne(o => o.BillingAddress).WithMany().HasForeignKey(o => o.BillingAddressId).OnDelete(DeleteBehavior.Restrict);
            HasOne(o => o.ShippingAddress).WithMany().HasForeignKey(o => o.ShippingAddressId).OnDelete(DeleteBehavior.Restrict);
            //HasOne(o => o.OrderStatus).WithMany().HasForeignKey(o => o.StatusId).OnDelete(DeleteBehavior.Restrict);


            Property(t => t.Comment).HasMaxLength(1000);
        }
    }
}
