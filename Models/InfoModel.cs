using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Q1WebApp.Models
{
    public class InfoModel
    {
        public int InfoId { get; set; }
        public int PersonId { get; set; }
        public string TelNo { get; set; }
        public string CellNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressCode { get; set; }
        public string PostalAddress1 { get; set; }
        public string PostalAddress2 { get; set; }
        public string PostalCode { get; set; }

        public InfoModel() { }

        public InfoModel(int infoId, int personId, string telNo, string cellNo, string addressLine1, string addressLine2, string addressLine3, string addressCode, string postalAddress1, string postalAddress2, string postalCode)
        {
            InfoId = infoId;
            PersonId = personId;
            TelNo = telNo;
            CellNo = cellNo;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            AddressCode = addressCode;
            PostalAddress1 = postalAddress1;
            PostalAddress2 = postalAddress2;
            PostalCode = postalCode;
        }
    }
}