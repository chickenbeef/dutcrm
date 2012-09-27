using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ConfigurationSettingsBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);

        public List<ConfigurationSetting> GetAllSettings()
        {
            _crm = new CRMEntities(_uri);
            return _crm.ConfigurationSettings.ToList();
        }

        public bool UpdateSetting(string setting, string value)
        {
            _crm = new CRMEntities(_uri);

            var objc = _crm.ConfigurationSettings.Where(x => x.Setting == setting).ToList()[0];
            if (objc == null) return false;
            objc.Value = value;
            _crm.UpdateObject(objc);
            _crm.SaveChanges();
            return true;
        }
    }
}
