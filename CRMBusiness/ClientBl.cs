using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ClientBl
    {
        
        private CRMEntities _crm    ;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");


        //methods
        //save
        public void AddClient(string name, string surname, DateTime dob, string tel, string cell, string fax, DateTime datecreated, DateTime datemodified,int branchid, string userid)
        {
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
                DateModified = datemodified,
                BRH_ID = branchid,
                UserId = userid,
            };

            _crm.AddToClients(objc);
            _crm.SaveChanges();
            
       }


        //update client

        public void UpdateClient(int cid, string name, string surname, DateTime dob, string tel, string cell, string fax, DateTime datemodified)
        {
            _crm = new CRMEntities(_uri);
            
            var objc = _crm.Clients.Where(x => x.CLIENT_ID == cid).ToList()[0];
            if (objc == null) return;
            objc.Name = name;
            objc.Surname = surname;
            objc.DateOfBirth = dob;
            objc.Telephone = tel;
            objc.Cell = cell;
            objc.Fax = fax;
            objc.DateModified = datemodified;
         
            _crm.UpdateObject(objc);
            _crm.SaveChanges();
        }

        //get client by name
        public vClient GetClientName(string  cName)
        {
            _crm = new CRMEntities(_uri);          
            return _crm.vClients.Where(x => x.Name == cName).ToList()[0];            
        }

        //get client by username
        public vClient GetClientUserName(string cUserName)
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClients.Where(x => x.UserName == cUserName).ToList()[0];
        }


        //get all clients
        public List<vClient> GetAllClients()
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClients.ToList();
        }
    }
}
