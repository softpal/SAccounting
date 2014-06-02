using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratusAccounting.BAL
{
    public class BusinessUsersBasicView
    {
        public int SNo { get; set; }
        public int BusinessId { get; set; }
        public string Business { get; set; }
        public string AdminEmail { get; set; }
        public DateTime RegisterdOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int? Licenses { get; set; }
        public string Status { get; set; }
        public int BusinessStatusId { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
