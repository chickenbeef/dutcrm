using System;
using CRMBusiness.CRM; 


namespace CRMBusiness
{
	public class SaleBl
	{

		private CRMEntities _crm;
		private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

		public void SaveSale(DateTime datecreated, int cid, int eid)
		{
		    _crm = new CRMEntities(_uri);
			var sale = new Sale
			{
				DateCreated = datecreated,
				CLIENT_ID = cid,
				EMP_ID = eid
			};

			_crm.AddToSales(sale);
			_crm.SaveChanges();				
		}
	}
}
