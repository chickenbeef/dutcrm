using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class CategoriesBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);
 
        public List<Category> GetAllCategories()
        {
            return new CRMEntities(_uri).Categories.ToList();
        }

        public void DeleteCategory(int catid)
        {
            _crm = new CRMEntities(_uri);
            var category = _crm.Categories.Where(c => c.CAT_ID == catid);
            _crm.DeleteObject(category);
            _crm.SaveChanges();
        }

        public void AddCategory(string name)
        {
            _crm = new CRMEntities(_uri);
            var category = new Category {Name = name};
            _crm.AddToCategories(category);
            _crm.SaveChanges();
        }
    }
}
