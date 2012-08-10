using System;
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
           using(_crm= new CRMEntities(_uri))
           {
               var objESc = _crm.Escalations.SingleOrDefault(x=> x.Priority == p);
               if (objESc != null)
               {
                   objESc.Priority = p;
                   objESc.Duration = (short) d;
               }

               _crm.SaveChanges();
           }
       }

       public Escalation GetConfigInfo(string p)
       {
           using(_crm = new CRMEntities(_uri))
           {
               return _crm.Escalations.SingleOrDefault(x=> x.Priority == p);
           }
       }
      #endregion
    }
}
