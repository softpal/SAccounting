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
    
    public partial class Asset
    {
        public Asset()
        {
            this.EmployeeAssets = new HashSet<EmployeeAsset>();
        }
    
        public long AssetsId { get; set; }
        public int BusinessID { get; set; }
        public Nullable<int> AssetTypeId { get; set; }
        public string AssetDescription { get; set; }
        public string LocatedAt { get; set; }
        public string PONumber { get; set; }
        public Nullable<long> UserVendorId { get; set; }
        public Nullable<System.DateTime> PurchasedDate { get; set; }
        public Nullable<decimal> InitialValue { get; set; }
        public Nullable<decimal> CurrentValue { get; set; }
        public Nullable<short> AssetDispositionId { get; set; }
        public string GLAccountCode { get; set; }
        public string DepreciationGLAccountCode { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedByUserId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Mst_AssetDisposition Mst_AssetDisposition { get; set; }
        public virtual BusinessRegistration BusinessRegistration { get; set; }
        public virtual BusinessRegistration BusinessRegistration1 { get; set; }
        public virtual UMst_AssetType UMst_AssetType { get; set; }
        public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; }
    }
}
