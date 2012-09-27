using System;
using System.Configuration;
using CRMBusiness.CRM; 


namespace CRMBusiness
{
	public class SaleBl
	{

		private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);

		public bool SaveSale(DateTime datecreated, int cid, int eid)
		{
            if (cid.Equals(0) || eid.Equals(0)) return false;
            _crm = new CRMEntities(_uri);
            var sale = new Sale
            {
                DateCreated = datecreated,
                CLIENT_ID = cid,
                EMP_ID = eid
            };
            _crm.AddToSales(sale);
            _crm.SaveChanges();
		    return true;
		}
	}
}
