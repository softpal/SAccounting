using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StratusAccounting.Models;
using BusinessRegistration = StratusAccounting.Models.BusinessRegistration;

namespace StratusAccounting.Helper
{
    public class CustomMetadataValidationProvider : DataAnnotationsModelValidatorProvider
    {
        private ModelMetadata _metadata;
        private IEnumerable<Attribute> _attributes;

        public IEnumerable<Attribute> Phone
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Phone number is Required" }, new RegularExpressionAttribute(@"^\(?([+.][0-9]{2}[0-9]{10})$") { ErrorMessage = "Enter valid Phone number with country code." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> Title
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Title is Required" }, new RegularExpressionAttribute(@"^([a-zA-Z]+\s)*[a-zA-Z]{1,100}$") { ErrorMessage = "Enter valid Title." }};
                return _attributes;
            }
        }

        public IEnumerable<Attribute> UniqueId
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Unique is Required" }, new RegularExpressionAttribute(@"^[A-Za-z0-9]{0,100}$") { ErrorMessage = "Enter valid Unique." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> Description
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0,300){ErrorMessage="You cann't enter more the 300 chars."} };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> AssetDispositionName
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> AssetName
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." }, new RegularExpressionAttribute(@"^([a-zA-Z]+\s)*[a-zA-Z]{1,50}$") { ErrorMessage = "Enter valid Name." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> PreferenceField
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0, 200) { ErrorMessage = "You cann't enter more the 200 chars." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> PreferenceValue
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> AddressLine1
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> AddressLine2
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> PreferenceValuesId
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "PreferenceValuesId is Required" } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> Licences
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Licences is Required" } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> Fax
        {
            get
            {
                _attributes = new List<Attribute> { new RegularExpressionAttribute(@"^\(?([+.][0-9]{2}[0-9]{10})$") { ErrorMessage = "Enter valid fax no." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> Email
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Email is Required" }, new RegularExpressionAttribute(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}") { ErrorMessage = "Please enter correct Email" }, new DataTypeAttribute(DataType.EmailAddress) };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> Decimals
        {
            get
            {
                _attributes = new List<Attribute>
                {
                    new DisplayFormatAttribute() {ApplyFormatInEditMode = true, DataFormatString = "{0:F2}"}

                };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> CustomerFirstName
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Customer First Name is Required" }, new RegularExpressionAttribute(@"^([a-zA-Z]+\s)*[a-zA-Z]{1,120}$") { ErrorMessage = "Enter valid Name." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> CompanyName
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Company Name is Required" }, new RegularExpressionAttribute(@"^([a-zA-Z]+\s)*[a-zA-Z]{1,120}$") { ErrorMessage = "Enter valid Name." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> CustomerLastName
        {
            get
            {
                _attributes = new List<Attribute> { new RegularExpressionAttribute(@"^([a-zA-Z]+\s)*[a-zA-Z]{1,120}$") { ErrorMessage = "Enter valid Name." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> VendorFirstName
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Customer First Name is Required" }, new RegularExpressionAttribute(@"^([a-zA-Z]+\s)*[a-zA-Z]{1,120}$") { ErrorMessage = "Enter valid Name." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> VendorLastName
        {
            get
            {
                _attributes = new List<Attribute> { new RegularExpressionAttribute(@"^([a-zA-Z]+\s)*[a-zA-Z]{1,120}$") { ErrorMessage = "Enter valid Name." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> Website
        {
            get
            {
                _attributes = new List<Attribute> { new RegularExpressionAttribute(@"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?$") { ErrorMessage = "Enter valid Website." } };
                return _attributes;
            }
        }

        //public IEnumerable<Attribute> CreditCardToken
        //{
        //    get
        //    {
        //        _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Enter valid CreditCardToken." }, new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
        //        return _attributes;
        //    }
        //}

        //public IEnumerable<Attribute> CreditCardToken
        //{
        //    get
        //    {
        //        _attributes = new List<Attribute> { new RegularExpressionAttribute(@"^\(?([+.][0-9]{2}[0-9]{100})$") { ErrorMessage = "Enter valid Website." } };
        //        return _attributes;
        //    }
        //}

        public IEnumerable<Attribute> TaxNumber
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> TaxTypesId
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute{ ErrorMessage = "TaxTypeId is Required." } };
                return _attributes;
            }
        }

        //public IEnumerable<Attribute> AccountNum
        //{
        //    get
        //    {
        //        _attributes = new List<Attribute> { new RegularExpressionAttribute("@^\w{1,17}$"){ErrorMessage="Enter valid Account Num."}, new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
        //        return _attributes;
        //    }
        //}

        public IEnumerable<Attribute> SwiftCode
        {
            get
            {
                _attributes = new List<Attribute> { new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> InvoiceTitle
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Invoice Title is Required" }, new RegularExpressionAttribute(@"^([a-zA-Z]+\s)*[a-zA-Z]{1,120}$") { ErrorMessage = "Enter valid Title." } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> PONumber
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "PONumber is Required" } };
                return _attributes;
            }
        }

        public IEnumerable<Attribute> Term
        {
            get
            {
                _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Term is Required" } };
                return _attributes;
            }
        }

        //public IEnumerable<Attribute> PreferenceFieldsId
        //{
        //    get
        //    {
        //        _attributes = new List<Attribute> { new RangeAttribute(0, 50) { ErrorMessage = "You cann't enter more the 50 chars." } };
        //        return _attributes;
        //    }
        //}

        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            _metadata = metadata;
            _attributes = attributes;
            //var user = context.HttpContext.User;

            if (metadata.ModelType == typeof(User) || metadata.ContainerType == typeof(User))
            {
                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "UserFirstName")
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "First Name is Required" }, new RegularExpressionAttribute(@"^[a-zA-Z''-'\s]{1,40}$") { ErrorMessage = "Numbers and special characters are not allowed in the name." } };

                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "UserLastName")
                    _attributes = new List<Attribute> { new RegularExpressionAttribute(@"^[a-zA-Z''-'\s]{1,40}$") { ErrorMessage = "Numbers and special characters are not allowed in the name." } };

                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "Email")
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Email is Required" }, new RegularExpressionAttribute(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}") { ErrorMessage = "Please enter correct Email" }, new DataTypeAttribute(DataType.EmailAddress) };

                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "UserPassword")
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Please enter password" }, new DataTypeAttribute(DataType.Password) };

                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "ConfirmPassword")
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Please enter confirm password" }, new System.ComponentModel.DataAnnotations.CompareAttribute("UserPassword") { ErrorMessage = "Passwords do not match" }, new DataTypeAttribute(DataType.Password) };
            }

            if (metadata.ModelType == typeof(BusinessRegistration) ||
                metadata.ContainerType == typeof(BusinessRegistration))
            {
                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "BusinessName")
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Business Name is Required" }, new RegularExpressionAttribute(@"^[a-zA-Z''-'\s]{1,40}$") { ErrorMessage = "Numbers and special characters are not allowed in the name." } };

                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "Phone")
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Phone number is Required" }, new RegularExpressionAttribute(@"^\(?([+.][0-9]{2}[0-9]{10})$") { ErrorMessage = "Enter valid Phone number with country code." } };

                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "Fax")
                    _attributes = new List<Attribute> { new RegularExpressionAttribute(@"^\(?([+.][0-9]{2}[0-9]{10})$") { ErrorMessage = "Enter valid fax no." } };

                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "CountryId")
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Please select country." } };
                //AddressLine1
                if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "AddressLine1")
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Address Field is required." } };
            }
            //if (metadata.ModelType == typeof(UserBankAccount) ||
            //    metadata.ContainerType == typeof(UserBankAccount))
            //{
            //    if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "BankName")
            //        _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "BankName is Required" }, new RegularExpressionAttribute(@"^[a-zA-Z''-'\s]{1,40}$") { ErrorMessage = "Numbers and special characters are not allowed in the name." } };
            //    if (!string.IsNullOrWhiteSpace(metadata.PropertyName) && metadata.PropertyName == "BankAccountTypeId")
            //        _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "BankAccount Type is Required" } };
            //}

            if (metadata.ModelType == typeof(UMst_BusinessItems) ||
                metadata.ContainerType == typeof(UMst_BusinessItems))
            {
                BusinessItems();
            }

            if (metadata.ModelType == typeof(UserBankAccount) || metadata.ContainerType == typeof(UserBankAccount))
            {
                BankAccounts();
            }

            if (metadata.ModelType == typeof(VendorTaxDetail) || metadata.ContainerType == typeof(VendorTaxDetail))
            {
                VendorTaxDetails();
            }

            if (metadata.ModelType == typeof(UserCustomer) || metadata.ContainerType == typeof(UserCustomer))
            {
                UserCustomer();
            }

            if (metadata.ModelType == typeof(Mst_PreferenceValues) || metadata.ContainerType == typeof(Mst_PreferenceValues))
            {
                Preferences();
            }

            if (metadata.ModelType == typeof(PurchaseInvoice) || metadata.ContainerType == typeof(PurchaseInvoice))
            {
                Invoices();
            }

            return base.GetValidators(metadata, context, _attributes);
        }

        private void BusinessItems()
        {
            switch (_metadata.PropertyName)
            {
                case "Title":
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Title is Required" }, new RegularExpressionAttribute(@"^[a-zA-Z''-'\s]{1,40}$") { ErrorMessage = "Numbers and special characters are not allowed in the name." } };
                    break;
                case "UniqueId":
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Unique# is Required" } };
                    break;
                case "UserAccountsId":
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Category is Required" } };
                    break;
                case "CostPrice":
                case "DiscountPrice":
                case "PreferredPrice":
                    _attributes = new List<Attribute> { new RegularExpressionAttribute(@"^(\d*\.\d{1,2}|\d+)$") { ErrorMessage = "Enter valid input like 0.00." } };
                    break;
                case "OpeningBalance":
                    _attributes = Decimals;
                    break;
            }
        }

        private void BankAccounts()
        {
            switch (_metadata.PropertyName)
            {
                case "BankName":
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "BankName Name is Required" }, new RegularExpressionAttribute(@"^[a-zA-Z''-'\s]{1,40}$") { ErrorMessage = "Numbers and special characters are not allowed in the name." } };
                    break;
                case "BankAccountTypeId":
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Account Type Id is Required" } };
                    break;
                case "OpeningBalance":
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "OpeningBalance is Required" }, new RegularExpressionAttribute(@"^(\d*\.\d{1,2}|\d+)$") { ErrorMessage = "Enter valid input like 0.00." } };
                    break;
                case "AccountNum":
                    _attributes = new List<Attribute> { new RequiredAttribute { ErrorMessage = "Account Number is Required" }, new RegularExpressionAttribute(@"^\w{1,17}$") { ErrorMessage = "Special charecters are allowed." } };
                    break;
                case "Phone":
                    _attributes = Phone;
                    break;
                case "Fax":
                    _attributes = Fax;
                    break;
            }
        }

        private void VendorTaxDetails()
        {
            switch (_metadata.PropertyName)
            {
                case "TaxNumber":
                    _attributes = TaxNumber;
                    break;
                case "TaxTypesId":
                    _attributes = TaxTypesId;
                    break;
            }
        }

        private void UserCustomer()
        {
            switch (_metadata.PropertyName)
            {
                case "CompanyName":
                    _attributes = CompanyName;
                    break;
                case "CustomerFirstName":
                    _attributes = CustomerFirstName;
                    break;
                case "CustomerLastName":
                    _attributes = CustomerLastName;
                    break;
                case "Website":
                    _attributes = Website;
                    break;
                //case "CreditCardToken":
                //    _attributes = CreditCardToken;
                //    break;
            }
        }

        private void Preferences()
        {
            switch (_metadata.PropertyName)
            {
                case "PreferenceValue":
                    _attributes = PreferenceValue;
                    break;
                //case "PreferenceFieldsId":
                //    _attributes = PreferenceFieldsId;
                //    break;
            }
        }

        private void Invoices()
        {
            switch (_metadata.PropertyName)
            {
                case "InvoiceTitle":
                    _attributes = InvoiceTitle;
                    break;
                case "PONumber":
                    _attributes = PONumber;
                    break;
                case "Term":
                    _attributes = Term;
                    break;
            }
        }

    }
}