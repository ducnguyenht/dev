using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPPopulate.Nomenclature.Organization
{
    public class OrganizationEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
    }
}
