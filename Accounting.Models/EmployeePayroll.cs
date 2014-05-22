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
    
    public partial class EmployeePayroll
    {
        public long EmployeePayrollId { get; set; }
        public long UserEmployeeId { get; set; }
        public int BusinessID { get; set; }
        public System.DateTime Date { get; set; }
        public short RegularHours { get; set; }
        public Nullable<short> OvertimeHours { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedByUserId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual BusinessRegistration BusinessRegistration { get; set; }
        public virtual UserEmployee UserEmployee { get; set; }
    }
}
