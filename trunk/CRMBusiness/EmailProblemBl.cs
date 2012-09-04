using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness .CRM;
namespace CRMBusiness
{
    public class EmailProblemBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        //METHODS

        //SAVE
        public bool AddEmailProblem(string description, DateTime datecreated, int clientid, int employeeid, string mailid)
        {
            if(description.Equals("") || clientid.Equals(0)) return false;
            _crm = new CRMEntities(_uri);
            var objep = new EmailProblem
                            {
                                Description = description,
                                DateCreated = datecreated,
                                CLIENT_ID = clientid,
                                EMP_ID = employeeid,
                                Mail_ID = mailid
                            };
            _crm.AddToEmailProblems(objep);
            _crm.SaveChanges();
            return true;
        }

        //UPDATE
        public bool UpdateEmailProblem(int epid, bool attended, int empid)
        {
            _crm = new CRMEntities(_uri);
            var objep = _crm.EmailProblems.Where(x => x.EP_ID == epid).ToList()[0];
            if (objep == null) return false;
            objep.Attended = attended;
            objep.EMP_ID = empid;
            _crm.UpdateObject(objep);
            _crm.SaveChanges();
            return true;
        }


        //get email problems
        public List<EmailProblem> GetAllEmailProblems()
        {
            _crm = new CRMEntities(_uri);
            return _crm.EmailProblems.ToList();
        }

        public List<EmailProblem> GetAllUnAttendedEmailProblems()
        {
            _crm = new CRMEntities(_uri);
            return _crm.EmailProblems.Where(x => x.Attended == false).ToList();
        }
    }
}

