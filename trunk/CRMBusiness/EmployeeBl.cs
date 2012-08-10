using System;
using System.Collections.Generic;
using System.Linq;
using CRMBusiness.CRM;

namespace CRMBusiness
{
    public class EmployeeBl
    {
        private CRMEntities _crm;
        private readonly Uri _uri = new Uri("http://localhost:1677/CRMService.svc");

        public List<vEmployee> GetAllEmployees()
        {
            using (_crm = new CRMEntities(_uri))
            {
                return _crm.vEmployees.ToList();
            }


        }

        public List<vEmployee> GetEmployee(string username)
        {
            using (_crm = new CRMEntities(_uri))
            {
                return _crm.vEmployees.Where(x => x.Name.Contains(username)).ToList();
            }
        }

        public void AddEmployee(string username, string surname, string telephone, string cell, string fax, DateTime datecreated, DateTime datemodified, string userid)
        {
            using (_crm = new CRMEntities(_uri))
            {

                var emp = new Employee
                {
                    Name = username,
                    Surname = surname,
                    Telephone = telephone,
                    Cell = cell,
                    Fax = fax,
                    DateCreated = datecreated,
                    DateModified = datemodified,
                    UserId = userid
                };
                _crm.AddToEmployees(emp);
                _crm.SaveChanges();
            }
        }

        public void UpdateEmployee(string username, string surname, string telephone, string cell, string fax, DateTime datecreated, DateTime datemodified, string userid)
        {
            using (_crm = new CRMEntities(_uri))
            {

                var e = _crm.Employees.SingleOrDefault(emp => emp.Name == username);

                if (e != null)
                {
                    e.Name = username;
                    e.Surname = surname;
                    e.Telephone = telephone;
                    e.Cell = cell;
                    e.Fax = fax;
                    e.DateCreated = datecreated;
                    e.DateModified = datemodified;
                    e.UserId = userid;

                    _crm.SaveChanges();
                }
            }

        }
    }
}
