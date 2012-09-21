using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ComTemplateBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);

        public List<ComTemplate> GetAllTemplates(int catid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.ComTemplates.Where(ct => ct.CAT_ID == catid).ToList();
        }

        public bool AddTemplate(string name, string paragraph, int catid)
        {
            if (name.Equals("") || paragraph.Equals("")) return false;
            _crm = new CRMEntities(_uri);
            var ct = new ComTemplate { Name = name, Paragraph = paragraph , CAT_ID = catid};
            _crm.AddToComTemplates(ct);
            _crm.SaveChanges();
            return true;
        }

        public bool DeleteTemplate(int ctId)
        {
            _crm = new CRMEntities(_uri);
            var ct = _crm.ComTemplates.Where(id => id.CT_ID == ctId).ToList()[0];

            if (ct == null) return false;
            _crm.DeleteObject(ct);
            _crm.SaveChanges();
            return true;
        }

        public bool UpdateTemplate(int ctId, string name, string paragraph, int catid)
        {
            _crm = new CRMEntities(_uri);
            var ct = _crm.ComTemplates.SingleOrDefault(id => id.CT_ID == ctId);
            if (ct == null) return false;
            ct.Name = name;
            ct.Paragraph = paragraph;
            ct.CAT_ID = catid;
            _crm.UpdateObject(ct);
            _crm.SaveChanges();
            return true;
        }

        public ComTemplate GetTemplateById(int id)
        {
            try
            {
                return new CRMEntities(_uri).ComTemplates.Where(x => x.CT_ID == id).ToList()[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
