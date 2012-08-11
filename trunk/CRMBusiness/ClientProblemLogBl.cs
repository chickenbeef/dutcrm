using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;


namespace CRMBusiness
{

    public class ClientProblemLogBl
    {
        #region variables

        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        #endregion

        #region methods

        public void Save(bool cprSolved, DateTime dCreated, DateTime dSolved, bool comTel, Int32 cId, Int32 eId,
                         Int32 pId, Int32 sId, string p)
        {
            using (_crm = new CRMEntities(_uri))
            {
                var objCPL = new ClientProblemsLog
                                 {
                                     Solved = cprSolved,
                                     DateCreated = dCreated,
                                     DateSolved = dSolved,
                                     ComTypeTel = comTel,
                                     CLIENT_ID = cId,
                                     EMP_ID = eId,
                                     PROB_ID = pId,
                                     SOL_ID = sId,
                                     Priority = p
                                 };


                _crm.AddToClientProblemsLogs(objCPL);
                _crm.SaveChanges();
            }
        }

        public List<vClientProblemsLog> GetAllClientProblems()
        {
            using (_crm = new CRMEntities(_uri))
            {
                return _crm.vClientProblemsLogs.ToList();
            }
        }

        public List<vClientProblemsLog> GetUnsolvedProblems()
        {
            
            using (_crm = new CRMEntities(_uri))
            {
                
                    return _crm.vClientProblemsLogs.Where(x=> x.Solved == false).ToList();
                
            }
            
        }

        public vClientProblemsLog GetClientProblem(int cprId)
        {
            using (_crm = new CRMEntities(_uri))
            {
                return _crm.vClientProblemsLogs.SingleOrDefault(x => x.CPR_ID == cprId);
            }
        }

        public List<vClientProblemsLog> GetClientProblem(string nme, string desc)
        {
            using (_crm = new CRMEntities(_uri))
            {
                return
                    _crm.vClientProblemsLogs.Where(x => x.ClientName == nme && x.ProblemDescription.Contains(desc)).
                        ToList();
            }
        }

        public void UpdateClientProblem(int cprId, bool cprSolved, DateTime dCreated, DateTime dSolved, bool comTel,
                                        Int32 cId, Int32 eId, Int32 pId, Int32 sId, string p)
        {
            using (_crm = new CRMEntities(_uri))
            {
                var objCPL = _crm.ClientProblemsLogs.SingleOrDefault(x => x.CPR_ID == cprId);
                if (objCPL == null) return;
                objCPL.Solved = cprSolved;
                objCPL.DateCreated = dCreated;
                objCPL.DateSolved = dSolved;
                objCPL.ComTypeTel = comTel;
                objCPL.CLIENT_ID = cId;
                objCPL.EMP_ID = eId;
                objCPL.PROB_ID = pId;
                objCPL.SOL_ID = sId;
                objCPL.Priority = p;

                _crm.SaveChanges();
            }
        }

        #endregion
    }
}
