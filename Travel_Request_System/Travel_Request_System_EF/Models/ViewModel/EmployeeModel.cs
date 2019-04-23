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
        public string FullName;
        public string PositionCode;
        public string Designation;
        public string Department;
        public string DepartmentHead;
        public string Costcenter;
        public string CostCenterHead;
        public string PassportID;
        public string PassportexpireDate;
        public string QatarID;
        public string QIDEDate;
        public string Location;
        public string Section;
        public string ContactDetails;
    }

    public class EmailPersonDetails
    {
        public string EmployeeID;
        public string EmployeeCode;
        public string FullName;
        public string Email;
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