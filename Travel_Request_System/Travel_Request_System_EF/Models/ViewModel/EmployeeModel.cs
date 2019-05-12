namespace Travel_Request_System_EF.Models.ViewModel
{
    public class FullEmployeeDetail
    {
        public string EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PositionCode { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string DepartmentHead { get; set; }
        public string Costcenter { get; set; }
        public string CostCenterHead { get; set; }
        public string PassportID { get; set; }
        public string PassportexpireDate { get; set; }
        public string QatarID { get; set; }
        public string QIDEDate { get; set; }
        public string Location { get; set; }
        public string Section { get; set; }
        public string ContactDetails { get; set; }
    }

    public class EmailPersonDetails
    {
        public string EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class MiniEmployeeDetail
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string HireDate { get; set; }
        public string Designation { get; set; }
        public string BusinessUnit { get; set; }
        public string Photo { get; set; }
    }

    public class QatarDetails
    {
        public string EMPLOYEEID { get; set; }
        public string EMPLOYEECODE { get; set; }
        public string FULLNAME { get; set; }
        public string HIREDATE { get; set; }
        public string QID { get; set; }
        public string QIDEDate { get; set; }
        public string QIdfile { get; set; }
        public string Mobile { get; set; }
    }

    public class PassportDetails
    {
        public string EMPLOYEEID { get; set; }
        public string EMPLOYEECODE { get; set; }
        public string FULLNAME { get; set; }
        public string HIREDATE { get; set; }
        public string PassportID { get; set; }
        public string PassportexpireDate { get; set; }
        public string Passportissuedate { get; set; }
        public string Passportfile { get; set; }
    }
}