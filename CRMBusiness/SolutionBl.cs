﻿using System;
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
        public bool AddSolution(string description, DateTime datecreated, int probid, int empid)
        {
            if (description.Equals(""))
            {
                return false;
            }
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
            return true;
        }

        //Update a specific solution
        public bool UpdateSolution(int solid, string description, DateTime datemodified, int probid, int empid)
        {
            _crm = new CRMEntities(_uri);
            var s = _crm.Solutions.Where(sol => sol.SOL_ID == solid).ToList()[0];
            if (s == null)
            {
                return false;
            }
            s.Description = description;
            s.DateModified = datemodified;
            s.PROB_ID = probid;
            s.EMP_ID = empid;
            _crm.UpdateObject(s);
            _crm.SaveChanges();
            return true;
        }

        //Get solutions to a specific problem
        public List<Solution> GetSolutions(int probid)
        {
            _crm = new CRMEntities(_uri);
            return _crm.Solutions.Where(s => s.PROB_ID == probid).ToList();
        }
    }
}
