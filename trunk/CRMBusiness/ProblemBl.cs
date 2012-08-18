using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ProblemBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        //Add a new problem to the database(problem table)
        public void AddProblem(string description, DateTime datecreated, int empid)
        {
            _crm = new CRMEntities(_uri);
            var p = new Problem
            {
                Description = description,
                DateCreated = datecreated,
                EMP_ID = empid
            };
            _crm.AddToProblems(p);
            _crm.SaveChanges();         
        }

        //Get a problem for a specific problem ID
        public Problem GetProblem(int probid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Problems.SingleOrDefault(p => p.PROB_ID == probid);      
        }

        //Get problems that match a phrase or keywords(Description)
        public  List<Problem> GetProblems(string description)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Problems.Where(p => p.Description.Contains(description)).ToList();             
        } 
    }
}
