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
    
    public partial class UserEmployee
    {
        public UserEmployee()
        {
            this.EmployeeBankDetails = new HashSet<EmployeeBankDetail>();
            this.EmployeeFamilyDetails = new HashSet<EmployeeFamilyDetail>();
            this.EmployeeLeaveBillingDetails = new HashSet<EmployeeLeaveBillingDetail>();
            this.EmployeeLeaves = new HashSet<EmployeeLeaf>();
            this.EmployeePaymentTypes = new HashSet<EmployeePaymentType>();
            this.EmployeePayrolls = new HashSet<EmployeePayroll>();
            this.EmployeesTimesheets = new HashSet<EmployeesTimesheet>();
            this.EmployeeAssets = new HashSet<EmployeeAsset>();
            this.Payments = new HashSet<Payment>();
        }
    
        public long UserEmployeeId { get; set; }
        public int BusinessID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string DOB { get; set; }
        public string SSN { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string AlternateEmail { get; set; }
        public string Mobile { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<short> CountryId { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string HomeFax { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedByUserId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CountyId { get; set; }
    
        public virtual ICollection<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public virtual ICollection<EmployeeFamilyDetail> EmployeeFamilyDetails { get; set; }
        public virtual ICollection<EmployeeLeaveBillingDetail> EmployeeLeaveBillingDetails { get; set; }
        public virtual ICollection<EmployeeLeaf> EmployeeLeaves { get; set; }
        public virtual ICollection<EmployeePaymentType> EmployeePaymentTypes { get; set; }
        public virtual ICollection<EmployeePayroll> EmployeePayrolls { get; set; }
        public virtual ICollection<EmployeesTimesheet> EmployeesTimesheets { get; set; }
        public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual BusinessRegistration BusinessRegistration { get; set; }
        public virtual Mst_Cities Mst_Cities { get; set; }
        public virtual Mst_Counties Mst_Counties { get; set; }
        public virtual Mst_Countries Mst_Countries { get; set; }
        public virtual Mst_States Mst_States { get; set; }
    }
}
