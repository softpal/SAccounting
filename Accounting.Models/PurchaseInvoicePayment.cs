//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Accounting.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseInvoicePayment
    {
        public PurchaseInvoicePayment()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public long PurchaseInvoicePaymentId { get; set; }
        public Nullable<long> PurchaseInvoiceId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<short> PaymentTypesId { get; set; }
        public string InstrumentNum { get; set; }
        public Nullable<short> PaymentStatusId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedByUserId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<short> PaidToTypeId { get; set; }
        public Nullable<long> ItemId { get; set; }
    
        public virtual Mst_PaymentStatus Mst_PaymentStatus { get; set; }
        public virtual Mst_PaymentTypes Mst_PaymentTypes { get; set; }
        public virtual Mst_UserType Mst_UserType { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }
        public virtual UMst_UserAccounts UMst_UserAccounts { get; set; }
    }
}
