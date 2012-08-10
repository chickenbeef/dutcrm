using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
	public class CompanyBl
	{

		private CRMEntities _crm;
		private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");



		public List<Company> GetAllCompanies()
		{

			using (_crm = new CRMEntities(_uri))
			{
				return _crm.Companies.ToList();
			}		

		}






		public Company GetCompany(int cpyid)
		{

			using (_crm = new CRMEntities(_uri))
			{

				return _crm.Companies.SingleOrDefault(co => co.CPY_ID == cpyid);

			}

		
		}





		public void AddCompany(string name, int type, int regtype, int regcomptype, string regno, string audfirmname, string audfirmqual, string audfirmaudit, bool vatregist, string vatregno, DateTime datecreated, DateTime datemodified  )
		{

			using (_crm = new CRMEntities(_uri))
			{


				var comp = new Company
				{
					Name = name,
					Type = type,
					RegisterType = regtype,
					RegisterCompanyType = regcomptype,
					RegisterNo = regno,
					AuditorFirmName = audfirmname,
					AuditorFirmQualification = audfirmqual,
					AuditorFirmAuditor = audfirmaudit,
					VATRegistered = vatregist,
					VATRegNo = vatregno,
					CreatedDate = datecreated,
					ModifiedDate = datemodified,
				};


				_crm.AddToCompanies(comp);
				_crm.SaveChanges();
				
			}

		}


	}
}
