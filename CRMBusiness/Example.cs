using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class Example
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        public List<ComTemplate> GetAllComTemplates()
        {
            _crm = new CRMEntities(_uri);
            return _crm.ComTemplates.ToList();
        }
    }
}
