using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class BranchesBl
    {
        #region Variables
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");
        #endregion

        #region Methods
        public bool AddBranch(int cpyid, string docexaddr, int routingId, string name, string physaddr,
            string physaddr2, string subrb, string city, string postaladdr1, string postaladdr2, string postalcode,
            string tel, string fax, string email, DateTime datecreated, string intkey)
        {
            if (cpyid.Equals(0) || name.Equals("") || physaddr.Equals("") || tel.Equals("") ||
                fax.Equals("") || email.Equals("")) return false;

            _crm = new CRMEntities(_uri);
            var objb = new Branch
            {
                CPY_ID = cpyid,
                DocexAddress = docexaddr,
                RoutingID = routingId,
                Name = name,
                PhysicalAddress1 = physaddr,
                PhysicalAddress2 = physaddr2,
                Suburb = subrb,
                City = city,
                PostalAddress1 = postaladdr1,
                PostalAddress2 = postaladdr2,
                PostalCode = postalcode,
                Tel = tel,
                Fax = fax,
                Email = email,
                DateCreated = datecreated,
                IntegrationKey = intkey
            };

            _crm.AddToBranches(objb);
            _crm.SaveChanges();
            return true;
        }

        public bool UpdateBranches(int brhId, int cpyid, string docexaddr, int routingId, string name, string physaddr, string physaddr2, string subrb, string city, string postaladdr1, string postaladdr2, string postalcode, string tel, string fax, string email, DateTime datemodified, string intkey)
        {
            var objb = _crm.Branches.Where(x => x.BRH_ID == brhId).ToList()[0];
            if (objb == null) return false;
            objb.CPY_ID = cpyid;
            objb.DocexAddress = docexaddr;
            objb.RoutingID = routingId;
            objb.Name = name;
            objb.PhysicalAddress1 = physaddr;
            objb.PhysicalAddress2 = physaddr2;
            objb.Suburb = subrb;
            objb.City = city;
            objb.PostalAddress1 = postaladdr1;
            objb.PostalAddress2 = postaladdr2;
            objb.PostalCode = postalcode;
            objb.Tel = tel;
            objb.Fax = fax;
            objb.Email = email;
            objb.DateCreated = datemodified;
            objb.IntegrationKey = intkey;
            return true;
        }

        public List<Branch> GetAllBranches()
        {
            return _crm.Branches.ToList();
        }

        public Branch GetBranch(int brhid)
        {
            return _crm.Branches.SingleOrDefault(x => x.BRH_ID == brhid);
        }
        #endregion
    }
}
