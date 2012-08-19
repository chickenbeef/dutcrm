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
        public void AddBranch(int cpyId, string daddress, int routingId, string brhname, string paddress1, string paddress2, string brhsubrb, string brhcity, string postaladdress1, string postaladdress2, string postcode, string brhtel, string brhfax, string brhemail, DateTime datecreated, DateTime datemodified, Char intkey)
        {
            using (_crm = new CRMEntities(_uri))
            {
                var objb = new Branch
                {
                    CpyId = cpyId;
                    Daddress = daddress;
                    RoutingId = routingId;
                    Brhname = brhname;
                    Paddress1 = paddress1;
                    Paddress2 = paddress2;
                    Brhsubrb = brhsubrb;
                    Brhcity = brhcity;
                    Postaladdress1 = postaladdress1;
                    Postaladdress2 = postaladdress2;
                    Postcode = postcode;
                    Brhtel = brhtel;
                    Brhfax = brhfax;
                    Brhemail = brhemail;
                    Datecreated = datecreated;
                    Datemodified = datemodified;
                    Intkey = intkey;
                };
                
                _crm.AddToBranches(objb);
				_crm.SaveChanges();

            }
        }

        public void UpdateBranches(int brhId, string daddress, string brhname, string paddress1, string paddress2, string brhsubrb, string brhcity, string postaladdress1, string postaladdress2, string postcode, string brhtel, string brhfax, string brhemail, DateTime datemodified)
        {
            using (_crm = new CRMEntities(_uri))
            {
                var objb = _crm.Branches.SingleOrDefault(x => x.BRH_ID == brhId);
                if (objb == null) return;
                objb.Brhid = brhId;
                objb.Daddress = daddress;
                objb.Brhname = brhname;
                objb.Paddress1 = paddress1;
                objb.Paddress2 = paddress2;
                objb.Brhsubrb = brhsubrb;
                objb.Brhcity = brhcity;
                objb.Postaladdress1 = postaladdress1;
                objb.Postaladdress2 = postaladdress2;
                objb.Postcode = postcode;
                objb.Brhtel = brhtel;
                objb.Brhfax = brhfax;
                objb.Brhemail = brhemail;
                objb.Datemodified = datemodified;
            }
        }

        public List<Branch> GetAllBranches()
        {
            using (_crm = new CRMEntities(_uri))
            {
                return _crm.Branches.ToList();
            }
        }

        public Branch GetBranch(int brhid)
        {
            using (_crm = new CRMEntities(_uri))
            {
                return _crm.Branches.SingleOrDefault(x => x.BRH_ID == brhid);
            }
        }
        #endregion
    }
}
