using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class SolutionBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        //Add a new solution to database
        public void AddSolution(string description, DateTime datecreated, int probid, int empid)
        {
            _crm = new CRMEntities(_uri);
            var s = new Solution
            {
                Description = description,
                DateCreated = datecreated,
                PROB_ID = probid,
                EMP_ID = empid
            };
            _crm.AddToSolutions(s);
            _crm.SaveChanges();
        }

        //Update a specific solution
        public void UpdateSolution(int solid, string description, DateTime datemodified, int probid, int empid)
        {
            _crm = new CRMEntities(_uri);
            var s = _crm.Solutions.SingleOrDefault(sol => sol.SOL_ID == solid);
            if (s == null) return;
            s.Description = description;
            s.DateModified = datemodified;
            s.PROB_ID = probid;
            s.EMP_ID = empid;
            _crm.SaveChanges();
        }

        //Get solutions to a specific problem
        public List<Solution> GetSolutions(int probid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Solutions.Where(s => s.PROB_ID == probid).ToList();
        }
    }
}
