using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class ChartBL
    {
        public DateTime DateV { get; set; }
        public int ValueV { get; set; }
        public int ValueZ { get; set; }



        public List<ChartBL> Getsolvedoncreate()
        {
            #region method to get and calculate prob count
            var dates = new List<DateTime>();
            var datvals = new List<int>();
            var countprobs = 0;
            //var intialdate = new DateTime(2012, 09, 15);


            var cprl = new ClientProblemLogBl().GetAllClientProblems();

            var solvedoOnCreate = new List<vClientProblemsLog>();
            var unSolvedOnCreate = new List<vClientProblemsLog>();
            var currentDate = DateTime.Now.Date;
            var startDate = DateTime.Now.AddDays(-7);

            //get tickets for last 7 days
            foreach (var ticket in cprl)
            {
                if (ticket.SolvedOnCreate && (ticket.DateCreated.Date >= startDate && ticket.DateCreated.Date <= currentDate.Date))
                {
                    solvedoOnCreate.Add(ticket);
                }
                else if ((!ticket.SolvedOnCreate) && (ticket.DateCreated.Date >= startDate && ticket.DateCreated.Date <= currentDate.Date))
                {
                    unSolvedOnCreate.Add(ticket);
                }
            }


            //calulate tickets for last 7 days
            var cDate = DateTime.Now.AddDays(+1);
            var objLC = new List<ChartBL>();

            for (int i = 0; i < 7; i++)
            {
                cDate = cDate.AddDays(-1);
                var counter = 0;
                var counter2 = 0;
                foreach (var soc in solvedoOnCreate)
                {
                    if (soc.DateCreated.Date.ToShortDateString() == cDate.ToShortDateString())
                    {
                        counter++;
                    }
                }

                foreach (var usoc in unSolvedOnCreate)
                {
                    if (usoc.DateCreated.Date.ToShortDateString() == cDate.ToShortDateString())
                    {
                        counter2++;
                    }
                }


                objLC.Add(new ChartBL { DateV = cDate, ValueV = counter,ValueZ = counter2});
            }

            return objLC;

            #endregion
        }


    }
}
