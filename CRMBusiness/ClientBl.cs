using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ClientBl
    {
        
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");


        //methods
        public void AddClient(string name, string surname, DateTime? dob, string tel, string cell, string fax, DateTime? datecreated, DateTime? datemodified,int branchid, string userid,string username)
        {
            using (_crm = new CRMEntities( _uri))
            {
                var objC = new ClientBl
                               {
                                   
                               }
            }
               



        }


    }
}
