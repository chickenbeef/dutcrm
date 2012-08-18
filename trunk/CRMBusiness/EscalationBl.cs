using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    
    public class EscalationBl
    {
       #region variables
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");
       #endregion

       #region methods
       public void UpdateConfig(string p, int d)
       {
           _crm = new CRMEntities(_uri);
            var objESc = _crm.Escalations.SingleOrDefault(x=> x.Priority == p);
            if (objESc != null)
            {
                objESc.Duration = (short) d;
            }

            _crm.SaveChanges();
       }

       public List<Escalation> GetConfigInfo()
       {
           _crm = new CRMEntities(_uri);
           return _crm.Escalations.ToList();
       }
      #endregion
    }
}
