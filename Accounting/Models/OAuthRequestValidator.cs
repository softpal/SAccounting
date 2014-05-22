using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StratusAccounting.Models
{
    using Intuit.Ipp.Core;
    using Intuit.Ipp.DataService;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.Security;

    public class OAuthRequestValidator
    {
        string accessToken = "";
        string accessTokenSecret = "";
        string consumerKey = "";
        string consumerSecret = "";

        //OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessToken, accessTokenSecret, consumerKey, consumerSecret);
    }

    public static class ServiceContext
    {
        //string appToken = "";
        //string companyID = "";
        //ServiceContext context = new ServiceContext(appToken, companyID, IntuitServicesType.QBD, oauthValidator);

        //DataService service = new DataService(context);

        //Customer customer = new Customer();
        //Customer resultCustomer = service.Add(customer) as Customer;

        //Customer resultCustomer = service.Update(customer) as Customer;

        //Customer resultCustomer = service.FindById(customer) as Customer;

        //int startPosition = 1;
        //int maxResult = 10;
        //List<Customer> customers = service.FindAll(customer, startPosition, maxResult).ToList<Customer>();
    }

    public class Customer
    {
        //Customer customer = new Customer();
        ////Mandatory Fields
        //customer.GivenName = "Mary";
        //customer.Title = "Ms.";
        //customer.MiddleName = "Jayne";
        //customer.FamilyName = "Cooper";


    }

}