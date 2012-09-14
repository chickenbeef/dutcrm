using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
	public class CompanyBl
	{
		private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);

		public List<Company> GetAllCompanies()
		{
		    _crm = new CRMEntities(_uri);
		    return _crm.Companies.ToList();
		}

		public Company GetCompany(int cpyid)
		{
		    _crm = new CRMEntities(_uri);
		    return _crm.Companies.Where(co => co.CPY_ID == cpyid).ToList()[0];
		}

		public bool AddCompany(string name, int type, int regtype, int regcomptype, string regno, string audfirmname, string audfirmqual, string audfirmaudit, bool vatregist, string vatregno, DateTime datecreated)
		{
            if (name.Equals("") || type.Equals(0) || regtype.Equals(0) || regcomptype.Equals(0) || 
                regno.Equals(0) || vatregist.Equals(0) || vatregno.Equals(0)) return false;
		    _crm = new CRMEntities(_uri);
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
			};
			_crm.AddToCompanies(comp);
			_crm.SaveChanges();
		    return true;
		}
	}
}
