using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CRMBusiness.CRM;


namespace CRMBusiness
{

    public class ClientProblemLogBl
    {
        #region variables

        private CRMEntities _crm;
        private readonly Uri _uri = new Uri(ConfigurationManager.AppSettings["WCFUri"]);

        #endregion

        #region methods

        public bool AddClientProblem(bool cprSolved, DateTime dCreated, DateTime? dSolved, bool comTel,
            Int32 cId, Int32 eId, Int32 pId, Int32? sId, string p)
        {
            if (cId.Equals(0) || eId.Equals(0) || pId.Equals(0) || p.Equals("")) return false;
            _crm = new CRMEntities(_uri);
            var objCpl = new ClientProblemsLog
                                {
                                    Solved = cprSolved,
                                    DateCreated = dCreated,
                                    DateSolved = dSolved,
                                    ComTypeTel = comTel,
                                    CLIENT_ID = cId,
                                    EMP_ID = eId,
                                    PROB_ID = pId,
                                    SOL_ID = sId,
                                    Priority = p,
                                    SolvedOnCreate = cprSolved
                                };

            _crm.AddToClientProblemsLogs(objCpl);
            _crm.SaveChanges();
            return true;
        }

        public List<vClientProblemsLog> GetAllClientProblems()
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClientProblemsLogs.ToList();
        }

        public List<vClientProblemsLog> GetUnsolvedProblems()
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClientProblemsLogs.Where(x=> x.Solved == false).ToList();           
        }

        public vClientProblemsLog GetClientProblem(int cprId)
        {
            try
            {
                _crm = new CRMEntities(_uri);
                return _crm.vClientProblemsLogs.Where(x => x.CPR_ID == cprId).ToList()[0];
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public vClientProblemsLog GetSolvedClientProblemById(int cprId)
        {
            try
            {
                _crm = new CRMEntities(_uri);
                return _crm.vClientProblemsLogs.Where(x => x.CPR_ID == cprId && x.Solved).ToList()[0];
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<vClientProblemsLog> GetClientProblemByUsername(string username)
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClientProblemsLogs.Where(x => x.UserName.Contains(username) && x.Solved).ToList();
        }

        public List<vClientProblemsLog> GetClientProblemByDescription(string description)
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClientProblemsLogs.Where(x => x.ProblemDescription.Contains(description) && x.Solved).ToList();
        }

        public List<vClientProblemsLog> GetClientProblemByClientName(string name)
        {
            return new CRMEntities(_uri).vClientProblemsLogs.Where(c => c.ClientName.Contains(name) && c.Solved).ToList();
        } 

        public bool UpdateClientProblem(int cprId, bool cprSolved, DateTime? dSolved,
                                        Int32 eId, Int32? sId)
        {
            _crm = new CRMEntities(_uri);
            var objCpl = _crm.ClientProblemsLogs.Where(x => x.CPR_ID == cprId).ToList()[0];
            if (objCpl == null) return false;
            objCpl.Solved = cprSolved;
            objCpl.DateSolved = dSolved;
            objCpl.EMP_ID = eId;
            objCpl.SOL_ID = sId;
            _crm.UpdateObject(objCpl);
            _crm.SaveChanges();
            return true;
        }

        public int GetLastCprId()
        {
            return new CRMEntities(_uri).ClientProblemsLogs.ToList().Max(cpl => cpl.CPR_ID);
        }

        public List<vClientProblemsLog> GetClientProblemsByProbId(int probid)
        {
            return new CRMEntities(_uri).vClientProblemsLogs.Where(x => x.PROB_ID == probid).ToList();
        }

        #endregion
    }
}
