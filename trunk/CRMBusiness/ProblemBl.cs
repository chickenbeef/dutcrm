using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ProblemBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);

        //Add a new problem to the database(problem table)
        public bool AddProblem(string description, DateTime datecreated, int empid)
        {
            if (description.Equals("") || empid.Equals(0)) return false;
            _crm = new CRMEntities(_uri);
            var p = new Problem
            {
                Description = description,
                DateCreated = datecreated,
                EMP_ID = empid
            };
            _crm.AddToProblems(p);
            _crm.SaveChanges();
            return true;
        }

        //Get a problem for a specific problem ID
        public Problem GetProblem(int probid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Problems.Where(p => p.PROB_ID == probid).ToList()[0];
        }

        //Get problems that match a phrase or keywords(Description)
        public  List<Problem> GetProblems(string description)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Problems.Where(p => p.Description.Contains(description)).ToList();          
        }

        //Get id of last problem
        public int GetLastId()
        {
            return new CRMEntities(_uri).Problems.ToList().Max(p => p.PROB_ID);
        }
    }
}
