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
    
    public partial class Get_BankAccounts_Result
    {
        public long BankAccountsId { get; set; }
        public string BankName { get; set; }
        public short BankAccountTypeId { get; set; }
        public string AccountTypeDesc { get; set; }
        public string AccountNum { get; set; }
        public decimal OpeningBalance { get; set; }
        public string AccountHolder1Name { get; set; }
        public string AccountHolder2Name { get; set; }
        public string AccountHolder3Name { get; set; }
        public string SwiftCode { get; set; }
        public string RountingNum { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
