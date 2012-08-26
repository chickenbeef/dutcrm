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
       public bool UpdateConfig(string priority, int duration)
       {
           _crm = new CRMEntities(_uri);
            var objESc = _crm.Escalations.Where(x=> x.Priority == priority).ToList()[0];
           if (objESc == null) return false;
           objESc.Duration = (short) duration;
           _crm.UpdateObject(objESc);
           _crm.SaveChanges();
           return true;
       }

       public List<Escalation> GetConfigInfo()
       {
           _crm = new CRMEntities(_uri);
           return _crm.Escalations.ToList();
       }
      #endregion
    }
}
