﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheckEnrollCard
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CARDEntities : DbContext
    {
        public CARDEntities()
            : base("name=CARDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CardRegisted> CardRegisteds { get; set; }
    
        public virtual ObjectResult<isExistCard_Result> isExistCard(string cardNum)
        {
            var cardNumParameter = cardNum != null ?
                new ObjectParameter("CardNum", cardNum) :
                new ObjectParameter("CardNum", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<isExistCard_Result>("isExistCard", cardNumParameter);
        }
    
        public virtual int RegisterCard(string cardNum, string cardType, string cardStat, string expDate)
        {
            var cardNumParameter = cardNum != null ?
                new ObjectParameter("CardNum", cardNum) :
                new ObjectParameter("CardNum", typeof(string));
    
            var cardTypeParameter = cardType != null ?
                new ObjectParameter("CardType", cardType) :
                new ObjectParameter("CardType", typeof(string));
    
            var cardStatParameter = cardStat != null ?
                new ObjectParameter("CardStat", cardStat) :
                new ObjectParameter("CardStat", typeof(string));
    
            var expDateParameter = expDate != null ?
                new ObjectParameter("ExpDate", expDate) :
                new ObjectParameter("ExpDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegisterCard", cardNumParameter, cardTypeParameter, cardStatParameter, expDateParameter);
        }
    }
}
