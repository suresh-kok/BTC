using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel_Request_System_EF.Models.ViewModel
{
    public class FullEmployeeDetail
    {
        public string EmployeeID;
        public string EmployeeCode;
        public string Name;
        public string HireDate;
        public string TerminationDate;
        public string EntityTypeId;
        public string Description;
        public string EntityId;
        public string Costcenter;
        public string CompanyCode;
        public string Email;
        public string nvDept;
        public string nvDeptCode;
        public string Photo;
        public string nvDesignation;
    }

    public class MiniEmployeeDetail
    {
        public string EmployeeCode;
        public string Name;
        public string HireDate;
        public string Designation;
        public string BusinessUnit;
        public string Photo;
    }

    public class QatarDetails
    {
        public string EMPLOYEEID;
        public string EMPLOYEECODE;
        public string FULLNAME;
        public string HIREDATE;
        public string QID;
        public string QIDEDate;
        public string QIdfile;
        public string Mobile;
    }

    public class PassportDetails
    {
        public string EMPLOYEEID;
        public string EMPLOYEECODE;
        public string FULLNAME;
        public string HIREDATE;
        public string PassportID;
        public string PassportexpireDate;
        public string Passportissuedate;
        public string Passportfile;
    }
}