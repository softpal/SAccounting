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
    
    public partial class PurchaseInvoice
    {
        public PurchaseInvoice()
        {
            this.Expenses = new HashSet<Expens>();
            this.PurchaseInvoiceDetails = new HashSet<PurchaseInvoiceDetail>();
            this.PurchaseInvoicePayments = new HashSet<PurchaseInvoicePayment>();
        }
    
        public long PurchaseInvoiceId { get; set; }
        public int BusinessID { get; set; }
        public string InvoiceTitle { get; set; }
        public long UserVendorId { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<long> PurchaseOrderId { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public short InvoiceStatusId { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedByUserId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<short> DueTermId { get; set; }
    
        public virtual Mst_InvoiceDueTerm Mst_InvoiceDueTerm { get; set; }
        public virtual Mst_Invoicestatus Mst_Invoicestatus { get; set; }
        public virtual BusinessRegistration BusinessRegistration { get; set; }
        public virtual ICollection<Expens> Expenses { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual ICollection<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; }
        public virtual ICollection<PurchaseInvoicePayment> PurchaseInvoicePayments { get; set; }
    }
}
