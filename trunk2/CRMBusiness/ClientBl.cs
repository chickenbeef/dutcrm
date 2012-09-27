using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ClientBl
    {
        
        private CRMEntities _crm    ;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);


        //methods
        //save
        public bool AddClient(string name, string surname, DateTime dob, string tel, string cell, string fax, DateTime dateofbirth, DateTime datecreated, int branchid, Guid userid)
        {
            if (name.Equals("") || surname.Equals("") || dateofbirth.Equals("") || branchid.Equals(0)) return false;
            _crm = new CRMEntities(_uri);
  
            var objc = new Client
            {
                Name = name,
                Surname = surname,
                DateOfBirth = dob,
                Telephone = tel,
                Cell = cell,
                Fax = fax,
                DateCreated = datecreated,
                BRH_ID = branchid,
                UserId = userid,
            };

            _crm.AddToClients(objc);
            _crm.SaveChanges();
            return true;
        }


        //update client

        public bool UpdateClient(int cid, string name, string surname, DateTime dob, string tel, string cell, string fax, DateTime datemodified)
        {
            _crm = new CRMEntities(_uri);
            
            var objc = _crm.Clients.Where(x => x.CLIENT_ID == cid).ToList()[0];
            if (objc == null) return false;
            objc.Name = name;
            objc.Surname = surname;
            objc.DateOfBirth = dob;
            objc.Telephone = tel;
            objc.Cell = cell;
            objc.Fax = fax;
            objc.DateModified = datemodified;
         
            _crm.UpdateObject(objc);
            _crm.SaveChanges();
            return true;
        }

        //get client by name
        public List<vClient> GetClientName(string  cName)
        {
            _crm = new CRMEntities(_uri);          
            return _crm.vClients.Where(x => x.Name == cName).ToList();            
        }

        //get client by username
        public List<vClient> GetClientUserName(string cUserName)
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClients.Where(x => x.UserName == cUserName).ToList();
        }

        //get client by client id
        public vClient GetClientByClientId(int cid)
        {
            try
            {
                _crm = new CRMEntities(_uri);
                return _crm.vClients.Where(x => x.CLIENT_ID == cid).ToList()[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        //get all clients
        public List<vClient> GetAllClients()
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClients.ToList();
        }
    }
}
