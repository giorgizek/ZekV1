﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zek.Test
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InsuranceDBEntities : DbContext
    {
        public InsuranceDBEntities()
            : base("name=InsuranceDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_PolicyVersion> T_PolicyVersion { get; set; }
    
        public virtual ObjectResult<SP_Policy_FinInfo_GetByXml_Result> SP_Policy_FinInfo_GetByXml(string xml, Nullable<System.DateTime> date)
        {
            var xmlParameter = xml != null ?
                new ObjectParameter("xml", xml) :
                new ObjectParameter("xml", typeof(string));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Policy_FinInfo_GetByXml_Result>("SP_Policy_FinInfo_GetByXml", xmlParameter, dateParameter);
        }
    }
}