using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class NoteBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);

        //Adds a new note to database
        public bool AddNote(string description, DateTime datecreated, int cprid, int empid)
        {
            if(description.Equals("") || cprid.Equals(0) || empid.Equals(0)) return false;
            _crm = new CRMEntities(_uri);
            var n = new Note
            {
                Description = description,
                DateCreated = datecreated,
                CPR_ID = cprid,
                EMP_ID = empid
            };
            _crm.AddToNotes(n);
            _crm.SaveChanges();
            return true;
        }

        //Get all notes for a specific problem logged(CPR_ID)
        public List<Note> GetNotes(int cprid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Notes.Where(n => n.CPR_ID == cprid).ToList();        
        } 
    }
}
