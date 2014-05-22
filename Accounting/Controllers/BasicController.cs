using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StratusAccounting.Models;

namespace StratusAccounting.Controllers
{
    [Authorize]
    public class BasicController : Controller
    {
        private int _userId;
        private int _businessId;
        public int UserId
        {
            get
            {
                _userId = Convert.ToInt32(HttpRuntime.Cache["UserId"]); //db.Users.First<User>(user => user.Email.Equals(User.Identity.Name));
                //_userId = logedUser.UserId;

                return _userId;
            }
        }
        public int BusinessId
        {
            get
            {
                if (_userId == 0)
                {
                    _userId = Convert.ToInt32(HttpRuntime.Cache["UserId"]);
                }
                _businessId = Convert.ToInt32(HttpRuntime.Cache["BusinessId"]);//db.BusinessRegistrations.Find(this._userId).BusinessID; //1;
                return _businessId;
            }
        }
    }
}
