using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
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
        public  List<CRMData.Problem> GetProblems(string description)
        {
            var words = description.Split(' ');
            var noiseWords = "About After All Also An And another Any Are As At Be because been before being between both But By came Can come could Did Do does each else For from Get Got Had Has have He Her here Him Himself His How to if the of in i is that it on you this with or was so if out not a".Split(' ');
            var keyWords = new List<string>();
            keyWords.AddRange(words);
            foreach (var word in words)
            {
                foreach (var noiseWord in noiseWords)
                {
                    if (word.Equals(noiseWord, StringComparison.InvariantCultureIgnoreCase))
                    {
                        keyWords.Remove(word);
                    }
                }
            }
            var sb = new StringBuilder();
            sb.Append("%");
            foreach (var keyWord in keyWords)
            {
                sb.Append(keyWord + "%");
            }
            var command = sb.ToString();
            var crm = new CRMData.CRMEntities(ConfigurationManager.ConnectionStrings["CRMEntities"].ToString());
            return
                crm.Problems.Where(x => SqlFunctions.PatIndex(command, x.Description) > 0).
                    ToList();
        }

        //Get id of last problem
        public int GetLastId()
        {
            return new CRMEntities(_uri).Problems.ToList().Max(p => p.PROB_ID);
        }
    }
}
