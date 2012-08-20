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
        public void AddEmailProblem(string description, DateTime datcreated, bool attended, int clientid, int employeeid)
        {

            _crm = new CRMEntities(_uri);

            var objep = new EmailProblem
                            {
                                Description = description,
                                DateCreated = datcreated,
                                Attended = attended,
                                CLIENT_ID = clientid,
                                EMP_ID = employeeid
                            };

            _crm.AddToEmailProblems(objep);
            _crm.SaveChanges();
          }

        //UPDATE
        public void UpdateEmailProbelm(int epid,string description, bool attended, int empid)
        {
            _crm = new CRMEntities(_uri);

            var objep = _crm.EmailProblems.Where(x => x.EP_ID == epid).ToList()[0];
            if (objep == null) return;
            objep.Description = description;
            objep.Attended = attended;
            objep.EMP_ID = empid;

            _crm.UpdateObject(objep);
            _crm.SaveChanges();
            
        }


        //get email problems
        public List<EmailProblem> GetAllEmailProblems()
        {
            _crm = new CRMEntities(_uri);
            return _crm.EmailProblems.ToList();
        }

    }
}

