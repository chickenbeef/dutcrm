using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ComTemplateBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        public List<ComTemplate> GetAllTemplates()
        {
            _crm = new CRMEntities(_uri);
            return _crm.ComTemplates.ToList();
        }

        public ComTemplate GetTemplate(int id)
        {
            _crm = new CRMEntities(_uri);
            return _crm.ComTemplates.Where(x => x.CT_ID == id).ToList()[0];
        }

        public bool AddTemplate(string name, string paragraph)
        {
            if (name.Equals("") || paragraph.Equals("")) return false;
            _crm = new CRMEntities(_uri);
            var ct = new ComTemplate { Name = name, Paragraph = paragraph };
            _crm.AddToComTemplates(ct);
            _crm.SaveChanges();
            return true;
        }

        public bool DeleteTemplate(short ctId)
        {
            _crm = new CRMEntities(_uri);
            var ct = _crm.ComTemplates.Where(id => id.CT_ID == ctId).ToList()[0];

            if (ct == null) return false;
            _crm.DeleteObject(ct);
            _crm.SaveChanges();
            return true;
        }

        public bool UpdateTemplate(short ctId, string name, string paragraph)
        {
            _crm = new CRMEntities(_uri);
            var ct = _crm.ComTemplates.SingleOrDefault(id => id.CT_ID == ctId);
            if (ct == null) return false;
            ct.Name = name;
            ct.Paragraph = paragraph;
            _crm.UpdateObject(ct);
            _crm.SaveChanges();
            return true;
        }
    }
}
