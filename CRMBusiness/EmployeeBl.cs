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
            _crm = new CRMEntities(_uri);
            return _crm.vEmployees.ToList();
        }

        public List<vEmployee> GetEmployee(string username)
        {
            _crm = new CRMEntities(_uri);
            return _crm.vEmployees.Where(x => x.Name.Contains(username)).ToList();
        }

        public bool AddEmployee(string name, string surname, string telephone, string cell, string fax, DateTime datecreated, string userid)
        {
            if (name.Equals("") || surname.Equals("")) return false;
            _crm = new CRMEntities(_uri);
            var emp = new Employee
            {
                Name = name,
                Surname = surname,
                Telephone = telephone,
                Cell = cell,
                Fax = fax,
                DateCreated = datecreated,
                UserId = userid
            };
            _crm.AddToEmployees(emp);
            _crm.SaveChanges();
            return true;
        }

        public bool UpdateEmployee(int empid, string name, string surname, string telephone, string cell, string fax, DateTime datemodified)
        {
            _crm = new CRMEntities(_uri);
            var emp = _crm.Employees.Where(e => e.EMP_ID == empid).ToList()[0];

            if (emp.Equals(null)) return false;
            emp.Name = name;
            emp.Surname = surname;
            emp.Telephone = telephone;
            emp.Cell = cell;
            emp.Fax = fax;
            emp.DateModified = datemodified;

            _crm.UpdateObject(emp);
            _crm.SaveChanges();
            return true;
        }
    }
}
