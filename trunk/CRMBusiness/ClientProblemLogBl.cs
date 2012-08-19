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

        public void AddClientProblem(bool cprSolved, DateTime dCreated, DateTime dSolved, bool comTel, Int32 cId, Int32 eId,
                         Int32 pId, Int32 sId, string p)
        {
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
                                    Priority = p
                                };


            _crm.AddToClientProblemsLogs(objCpl);
            _crm.SaveChanges();           
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
            _crm = new CRMEntities(_uri);
            return _crm.vClientProblemsLogs.Where(x => x.CPR_ID == cprId).ToList()[0];
        }

        public List<vClientProblemsLog> GetClientProblem(string nme, string desc)
        {
            _crm = new CRMEntities(_uri);
            return _crm.vClientProblemsLogs.Where(x => x.ClientName == nme && x.ProblemDescription.Contains(desc)).ToList();
        }

        public void UpdateClientProblem(int cprId, bool cprSolved, DateTime dCreated, DateTime dSolved, bool comTel,
                                        Int32 cId, Int32 eId, Int32 pId, Int32 sId, string p)
        {
            _crm = new CRMEntities(_uri);
            var objCpl = _crm.ClientProblemsLogs.Where(x => x.CPR_ID == cprId).ToList()[0];
            if (objCpl == null) return;
            objCpl.Solved = cprSolved;
            objCpl.DateCreated = dCreated;
            objCpl.DateSolved = dSolved;
            objCpl.ComTypeTel = comTel;
            objCpl.CLIENT_ID = cId;
            objCpl.EMP_ID = eId;
            objCpl.PROB_ID = pId;
            objCpl.SOL_ID = sId;
            objCpl.Priority = p;

            _crm.UpdateObject(objCpl);
            _crm.SaveChanges();
        }

        #endregion
    }
}
