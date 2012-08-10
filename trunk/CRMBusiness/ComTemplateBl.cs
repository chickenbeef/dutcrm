using System;
using System.Collections.Generic;
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
            using (_crm = new CRMEntities(_uri))
            {
                return _crm.ComTemplates.ToList();
            }
        }

        public List<ComTemplate> GetTemplate(int id)
        {
            using (_crm = new CRMEntities(_uri))
            {
                return _crm.ComTemplates.Where(x => x.CT_ID == id).ToList();
            }
        }

        public void AddTemplate(string name, string paragraph)
        {
            using (_crm = new CRMEntities(_uri))
            {
                var ct = new ComTemplate { Name = name, Paragraph = paragraph };
                _crm.AddToComTemplates(ct);
                _crm.SaveChanges();
            }
        }

        public void DeleteTemplate(short Id)
        {
            using (_crm = new CRMEntities(_uri))
            {
                var ct = _crm.ComTemplates.SingleOrDefault(id => id.CT_ID == Id);

                if (ct != null)
                {
                    _crm.DeleteObject(ct);
                    _crm.SaveChanges();
                }
            }
        }

        public void UpdateTemplate(short ID, string name, string paragraph)
        {
            using (_crm = new CRMEntities(_uri))
            {
                var ct = _crm.ComTemplates.SingleOrDefault(id => id.CT_ID == ID);

                if (ct != null)
                {
                    ct.Name = name;
                    ct.Paragraph = paragraph;
                    _crm.SaveChanges();
                }
            }
        }
    }
}
