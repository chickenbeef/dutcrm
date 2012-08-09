using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class NoteBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        //Adds a new note to database
        public void AddNote(string description, DateTime datecreated, int cprid, int empid)
        {
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
        }

        //Get all notes for a specific problem logged(CPR_ID)
        public List<Note> GetNotes(int cprid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Notes.Where(n => n.CPR_ID == cprid).ToList();
        } 
    }
}
