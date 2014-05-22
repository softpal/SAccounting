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
    
    public partial class VendorTaxDetail
    {
        public long UserVendorId { get; set; }
        public short TaxTypesId { get; set; }
        public string TaxNumber { get; set; }
        public Nullable<decimal> TaxValue { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedByUserId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual UserVendor UserVendor { get; set; }
        public virtual Mst_TaxTypes Mst_TaxTypes { get; set; }
    }
}
