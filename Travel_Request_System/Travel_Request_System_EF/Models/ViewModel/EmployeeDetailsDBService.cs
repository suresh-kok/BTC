using System;
using System.Data.SqlClient;
using System.Linq;

namespace Travel_Request_System_EF.Models.ViewModel
{
    public class EmployeeDetailsDBService : IDisposable
    {
        string employeeCode = "";

        public EmployeeDetailsDBService(string employeeCode)
        {
            this.employeeCode = employeeCode;
        }

        //Cost Center Manager of the Employee
        public string EmployeeManager(string entityTypeID)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT " +
                                            "Emp.[FirstName], " +
                                            "EM.Description FROM " +
                                            "HRW_Employee Emp " +
                                            "INNER JOIN " +
                                            "ORG_EmpEntityLink Oel" +
                                            "ON " +
                                            "(Emp.EmployeeID = Oel.EmployeeId) " +
                                            "INNER JOIN " +
                                            "ORG_EntityMaster EM " +
                                            "ON " +
                                            "(EM.EntityId = Oel.EntityId) " +
                                            "WHERE " +
                                            "Emp.EmployeeCode = '" + employeeCode + "'" +
                                            "AND EM.EntityTypeID = '" + entityTypeID + "'";

                var sequenceQueryResult = db.Database.SqlQuery<string>(sequenceMaxQuery).FirstOrDefault();

                string EmployeeManagerName = string.Empty;

                if (sequenceQueryResult != null)
                {
                    EmployeeManagerName = sequenceQueryResult.ToString();
                }

                return EmployeeManagerName;
            }
        }

        public string EmployeeManagerDetails(string entityTypeID)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT " +
                                            "Emp.EmployeeCode, " +
                                            "EM.Description FROM " +
                                            "HRW_Employee Emp " +
                                            "INNER JOIN " +
                                            "ORG_EmpEntityLink Oel" +
                                            "ON " +
                                            "(Emp.EmployeeID = Oel.EmployeeId) " +
                                            "INNER JOIN " +
                                            "ORG_EntityMaster EM " +
                                            "ON " +
                                            "(EM.EntityId = Oel.EntityId) " +
                                            "WHERE " +
                                            "Emp.EmployeeCode = '" + employeeCode + "'" +
                                            "AND EM.EntityTypeID = '" + entityTypeID + "'";

                var sequenceQueryResult = db.Database.SqlQuery<string>(sequenceMaxQuery).FirstOrDefault();

                string EmployeeManagerName = string.Empty;

                if (sequenceQueryResult != null)
                {
                    EmployeeManagerName = sequenceQueryResult.ToString();
                }

                return EmployeeManagerName;
            }
        }

        public string DepartmentHeadDetails(string entityTypeID)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT " +
                                            "Emp.EmployeeCode, " +
                                            "EM.Description FROM " +
                                            "HRW_Employee Emp " +
                                            "INNER JOIN " +
                                            "ORG_EmpEntityLink Oel" +
                                            "ON " +
                                            "(Emp.EmployeeID = Oel.EmployeeId) " +
                                            "INNER JOIN " +
                                            "ORG_EntityMaster EM " +
                                            "ON " +
                                            "(EM.EntityId = Oel.EntityId) " +
                                            "WHERE " +
                                            "Emp.EmployeeCode = '" + employeeCode + "'" +
                                            "AND EM.EntityTypeID = '" + entityTypeID + "'";

                var sequenceQueryResult = db.Database.SqlQuery<string>(sequenceMaxQuery).FirstOrDefault();

                string EmployeeManagerName = string.Empty;

                if (sequenceQueryResult != null)
                {
                    EmployeeManagerName = sequenceQueryResult.ToString();
                }

                return EmployeeManagerName;
            }
        }

        //Employee Details
        public FullEmployeeDetail FullEmployeeDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT a.EmployeeID " +
            " , a.EmployeeCode " +
            " ,a.FullName " +
            " ,( " +
                " SELECT Value " +
                " FROM HRW_VEmpEntityValues " +
                    " WHERE EmployeeId IN( " +
                        " SELECT EmployeeId " +
                        " FROM HRW_Employee " +
                        " WHERE EmployeeCode = '@employeeCode' " +
                            " AND RecordType = 'EMP' " +
                        " ) " +
                    " AND EntityTypeId = '88' " +
                " ) AS Designation " +
            " ,( " +
                " SELECT Value " +
                " FROM HRW_VEmpEntityValues " +
                " WHERE EmployeeId IN(" +
                        " SELECT EmployeeId " +
                        " FROM HRW_Employee " +
                        " WHERE EmployeeCode = '@employeeCode' " +
                            " AND RecordType = 'EMP' " +
                        " ) " +
                    " AND EntityTypeId = '10' " +
                " ) AS Department " +
            " ,( " +
                " SELECT Emp.FirstName " +
                " FROM HRW_Employee Emp " +
                " INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                " INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                " WHERE Emp.EmployeeCode = '@employeeCode' " +
                    " AND EM.EntityTypeID = '96' " +
                " ) AS DepartmentHead " +
            " ,( " +
                " SELECT Value " +
                " FROM HRW_VEmpEntityValues " +
                " WHERE EmployeeId IN(" +
                        " SELECT EmployeeId " +
                        " FROM HRW_Employee " +
                        " WHERE EmployeeCode = '@employeeCode' " +
                           " AND RecordType = 'EMP' " +
                        " ) " +
                    " AND EntityTypeId = '10' " +
                " ) AS CostCenter " +
            " ,( " +
                " SELECT Emp.FirstName " +
                " FROM HRW_Employee Emp " +
                " INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                " INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                " WHERE Emp.EmployeeCode = '@employeeCode' " +
                    " AND EM.EntityTypeID = '101' " +
                " ) AS CostCenterHead " +
              " , PassportID " +
              " , PassportexpireDate " +
              " , Passportissuedate " +
              " , QID AS QatarID " +
               " , QIDEDate " +
               " , QIdfile " +
               " ,( " +
                " SELECT Value " +
                 " FROM HRW_VEmpEntityValues " +
                 " WHERE EmployeeId IN(" +
                         " SELECT EmployeeId " +
                         " FROM HRW_Employee " +
                         " WHERE EmployeeCode = '@employeeCode' " +
                             " AND RecordType = 'EMP' " +
                         " ) " +
                     " AND EntityTypeId = '84' " +
                 " ) AS Location " +
               " ,( " +
                " SELECT Value " +
                 " FROM HRW_VEmpEntityValues " +
                 " WHERE EmployeeId IN(" +
                         " SELECT EmployeeId " +
                         " FROM HRW_Employee " +
                         " WHERE EmployeeCode = '@employeeCode' " +
                             " AND RecordType = 'EMP' " +
                         " ) " +
                     " AND EntityTypeId = '85' " +
                 " ) AS Section " +
               " ,( " +
                " SELECT Value " +
                 " FROM HRW_VEmpEntityValues " +
                 " WHERE EmployeeId IN(" +
                         " SELECT EmployeeId " +
                         " FROM HRW_Employee " +
                         " WHERE EmployeeCode = '@employeeCode' " +
                             " AND RecordType = 'EMP' " +
                       " ) " +
                     " AND EntityTypeId = '93' " +
             " ) AS Contact " +
            " , cast(a.HireDate AS DATE) AS HireDate " +
            " , a.TerminationDate " +
            " ,a.EntityTypeId " +
            " ,c.Description " +
            " ,a.EntityId " +
            " ,b.Description AS Costcenter " +
            " ,b.EntityCode AS CompanyCode " +
            " ,isnull(( " +
                    " SELECT ParamValue " +
                    " FROM HRW_VEmployeeFields " +
                    " WHERE EmployeeId IN ( " +
                            " SELECT EmployeeId " +
                            " FROM HRW_Employee " +
                            " WHERE EmployeeCode = '@employeeCode' " +
                                " AND RecordType = 'EMP' " +
                          "   ) " +
                        " AND EntityTypeParamID = '15' " +
                    " ), '') AS Email " +
            " , isnull((" +
                    " SELECT Value " +
                    " FROM HRW_VEmpEntityValues " +
                    " WHERE EmployeeId IN(" +
                            " SELECT EmployeeId " +
                            " FROM HRW_Employee " +
                            " WHERE EmployeeCode = '@employeeCode' " +
                                " AND RecordType = 'EMP' " +
                          " ) " +
                        " AND EntityTypeId = '10' " +
                    " ), '') AS nvDept " +
            " , isnull((" +
                    " SELECT EntityCode " +
                    " FROM ORG_EntityMaster " +
                    " WHERE EntityId IN(" +
                            " SELECT EntityId " +
                            " FROM HRW_VEmpEntityValues " +
                            " WHERE EmployeeCode = '@employeeCode' " +
                                " AND RecordType = 'EMP' " +
                            " ) " +
                        " AND EntityTypeId = '10' " +
                    " ), '') AS nvDeptCode " +
            " , isnull((" +
                    " SELECT Value " +
                    " FROM HRW_VEmpEntityValues " +
                    " WHERE EmployeeId IN(" +
                            " SELECT EmployeeId " +
                            " FROM HRW_Employee " +
                            " WHERE EmployeeCode = '@employeeCode' " +
                                " AND RecordType = 'EMP' " +
                            " ) " +
                        " AND EntityTypeId = '88' " +
                    " ), '') AS nvDesignation " +
            " FROM HRW_VEmpEntityValues AS a " +
        " INNER JOIN ORG_EntityMaster AS b ON a.EntityId = b.EntityId " +
        " INNER JOIN ORG_EntityType AS c ON a.EntityTypeId = c.EntityTypeId " +
        " INNER JOIN QATARIDDetails AS d ON a.EmployeeCode = d.EMPLOYEECODE " +
        " INNER JOIN PassportDetails AS e ON a.EmployeeCode = e.EMPLOYEECODE " +
        " WHERE a.employeecode = '@employeeCode' " +
            " AND TerminationDate IS NULL " +
            " AND a.RecordType = 'EMP' ";

                var sequenceQueryResult = db.Database.SqlQuery<FullEmployeeDetail>(sequenceMaxQuery, new SqlParameter("employeeCode", employeeCode)).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    FullEmployeeDetail fullEmployeeDetailsVal = sequenceQueryResult;
                    return fullEmployeeDetailsVal;
                }
            }
            return new FullEmployeeDetail();
        }

        //Employee Details – View
        public MiniEmployeeDetail SimpleEmployeeDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT [EMPLOYEECODE] " +
                                            ",[FULLNAME] " +
                                            ",[HIREDATE] " +
                                            ",[Designation] " +
                                            ",[BusinessUnit] " +
                                            ",[Photo] " +
                                            "FROM[HRWorks].[dbo].[INT_EmployeeMasters] " +
                                            "where EMPLOYEECODE = '" + employeeCode + "'";

                var sequenceQueryResult = db.Database.SqlQuery<MiniEmployeeDetail>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    MiniEmployeeDetail simpleEmployeeDetailsVal = sequenceQueryResult;
                    return simpleEmployeeDetailsVal;
                }
            }
            return null;
        }

        //Department Head of the Employee  
        public dynamic DepartmentHead(string entityID)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT " +
                                            "Emp.[FirstName], " +
                                            "EM.Description FROM " +
                                            "HRW_Employee Emp " +
                                            "INNER JOIN " +
                                            "ORG_EmpEntityLink Oel " +
                                            "ON " +
                                            "(Emp.EmployeeID = Oel.EmployeeId) " +
                                            "INNER JOIN " +
                                            "ORG_EntityMaster EM " +
                                            "ON " +
                                            "(EM.EntityId = Oel.EntityId) " +
                                            "WHERE " +
                                            "Emp.EmployeeCode = '" + employeeCode + "'" +
                                            "AND EM.EntityTypeID = '" + entityID + "'";

                var sequenceQueryResult = db.Database.SqlQuery<string>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    var departmentHeadVal = sequenceQueryResult.ToString();
                    return departmentHeadVal;
                }
            }
            return null;
        }

        //Qatar ID Details - View 
        public QatarDetails QatarIDDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT [EMPLOYEEID] " +
                                            ",[EMPLOYEECODE] " +
                                            ",[FULLNAME] " +
                                            ",[HIREDATE] " +
                                            ",[QID] " +
                                            ",[QIDEDate] " +
                                            ",[QIdfile] " +
                                            ",[Mobile] " +
                                            "FROM[HRWorks].[dbo].[QATARIDDetails] " +
                                            "where EMPLOYEECODE = '" + employeeCode + "'";

                var sequenceQueryResult = db.Database.SqlQuery<QatarDetails>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    QatarDetails qatarIDDetailsVal = sequenceQueryResult;
                    return qatarIDDetailsVal;
                }
            }
            return null;
        }

        //Passport ID Details - View 
        public PassportDetails PassportDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT [EMPLOYEEID] " +
                                            ",[EMPLOYEECODE] " +
                                            ",[FULLNAME] " +
                                            ",[HIREDATE] " +
                                            ",[PassportID] " +
                                            ",[PassportexpireDate] " +
                                            ",[Passportissuedate] " +
                                            ",[Passportfile] " +
                                            "FROM[HRWorks].[dbo].[PassportDetails] " +
                                            "where EMPLOYEECODE = '" + employeeCode + "'";

                var sequenceQueryResult = db.Database.SqlQuery<PassportDetails>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    PassportDetails passportDetailsVal = sequenceQueryResult;
                    return passportDetailsVal;
                }
            }
            return null;
        }

        public EmailPersonDetails HRDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT Emp.EmployeeID " +
                                            ", Emp.EmployeeCode " +
                                            ",Emp.FullName " +
                                            ",isnull(( " +
                                                    "SELECT ParamValue " +
                                                    "FROM HRW_VEmployeeFields " +
                                                    "WHERE EmployeeId IN( " +
                                                            "SELECT Emp.EmployeeId " +
                                                            "FROM HRW_Employee Emp " +
                                                            "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                                            "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                                            "WHERE Emp.EmployeeCode = '164' " +
                                                                "AND EM.EntityTypeID = '101' " +
                                                            ") " +
                                                        "AND EntityTypeParamID = '15' " +
                                                    "), '') AS HRManagerEmail " +
                                        "FROM HRW_Employee Emp " +
                                        "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                        "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                        "WHERE Emp.EmployeeCode = '164' " +
                                            "AND EM.EntityTypeID = '101' ";

                var sequenceQueryResult = db.Database.SqlQuery<EmailPersonDetails>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    EmailPersonDetails emailPersonDetailsVal = sequenceQueryResult;
                    return emailPersonDetailsVal;
                }
            }
            return null;
        }

        public EmailPersonDetails HRNotificationDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT Emp.EmployeeID " +
                                            ", Emp.EmployeeCode " +
                                            ",Emp.FullName " +
                                            ",isnull(( " +
                                                    "SELECT ParamValue " +
                                                    "FROM HRW_VEmployeeFields " +
                                                    "WHERE EmployeeId IN( " +
                                                            "SELECT Emp.EmployeeId " +
                                                            "FROM HRW_Employee Emp " +
                                                            "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                                            "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                                            "WHERE Emp.EmployeeCode = '9' " +
                                                                "AND EM.EntityTypeID = '101' " +
                                                            ") " +
                                                        "AND EntityTypeParamID = '15' " +
                                                    "), '') AS HRManagerEmail " +
                                        "FROM HRW_Employee Emp " +
                                        "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                        "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                        "WHERE Emp.EmployeeCode = '9' " +
                                            "AND EM.EntityTypeID = '101' ";

                var sequenceQueryResult = db.Database.SqlQuery<EmailPersonDetails>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    EmailPersonDetails emailPersonDetailsVal = sequenceQueryResult;
                    return emailPersonDetailsVal;
                }
            }
            return null;
        }

        public EmailPersonDetails TravelCoordinatorDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT Emp.EmployeeID " +
                                            ", Emp.EmployeeCode " +
                                            ",Emp.FullName " +
                                            ",isnull(( " +
                                                    "SELECT ParamValue " +
                                                    "FROM HRW_VEmployeeFields " +
                                                    "WHERE EmployeeId IN( " +
                                                            "SELECT Emp.EmployeeId " +
                                                            "FROM HRW_Employee Emp " +
                                                            "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                                            "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                                            "WHERE Emp.EmployeeCode = '21' " +
                                                                "AND EM.EntityTypeID = '101' " +
                                                            ") " +
                                                        "AND EntityTypeParamID = '15' " +
                                                    "), '') AS HRManagerEmail " +
                                        "FROM HRW_Employee Emp " +
                                        "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                        "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                        "WHERE Emp.EmployeeCode = '21' " +
                                            "AND EM.EntityTypeID = '101' ";

                var sequenceQueryResult = db.Database.SqlQuery<EmailPersonDetails>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    EmailPersonDetails emailPersonDetailsVal = sequenceQueryResult;
                    return emailPersonDetailsVal;
                }
            }
            return null;
        }

        public EmailPersonDetails DepartmentHeadMailDetails(string EmployeeCode)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT Emp.EmployeeID " +
                                            ", Emp.EmployeeCode " +
                                            ",Emp.FullName " +
                                            ",isnull(( " +
                                                    "SELECT ParamValue " +
                                                    "FROM HRW_VEmployeeFields " +
                                                    "WHERE EmployeeId IN( " +
                                                            "SELECT Emp.EmployeeId " +
                                                            "FROM HRW_Employee Emp " +
                                                            "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                                            "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                                            "WHERE Emp.EmployeeCode IN ( " +
                                                                "SELECT Emp.EmployeeId " +
                                                                    "FROM HRW_Employee Emp " +
                                                                    "INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
                                                                    "INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
                                                                    "WHERE Emp.EmployeeCode = '@employeeCode' " +
                                                                    "AND EM.EntityTypeID = '96' " +
                                                                    ")  " +
                                                                "AND EM.EntityTypeID = '101' " +
                                                                ") " +
                                                        "AND EntityTypeParamID = '15' " +
                                                        "), '') AS HRManagerEmail " +
                                                        "FROM HRW_Employee Emp " +
                                                                    "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                                                    "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                                                    "WHERE Emp.EmployeeCode IN ( " +
                                                                        "SELECT Emp.EmployeeId " +
                                                                        "FROM HRW_Employee Emp " +
                                                                        "INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
                                                                        "INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
                                                                        "WHERE Emp.EmployeeCode = '@employeeCode' " +
                                                                            "AND EM.EntityTypeID = '96' " +
                                                        ") " +
                                            "AND EM.EntityTypeID = '101' ";

                var sequenceQueryResult = db.Database.SqlQuery<EmailPersonDetails>(sequenceMaxQuery, new SqlParameter("employeeCode", employeeCode)).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    EmailPersonDetails emailPersonDetailsVal = sequenceQueryResult;
                    return emailPersonDetailsVal;
                }
            }
            return null;
        }

        public EmailPersonDetails EmployeesUnderDepartmentHeadDetails(string EmployeeCode)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT Emp.EmployeeID " +
                                            ", Emp.EmployeeCode " +
                                            ",Emp.FullName " +
                                            ",isnull(( " +
                                                    "SELECT ParamValue " +
                                                    "FROM HRW_VEmployeeFields " +
                                                    "WHERE EmployeeId IN( " +
                                                            "SELECT Emp.EmployeeId " +
                                                            "FROM HRW_Employee Emp " +
                                                            "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                                            "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                                            "WHERE Emp.EmployeeCode IN ( " +
                                                                "SELECT Emp.EmployeeId " +
                                                                    "FROM HRW_Employee Emp " +
                                                                    "INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
                                                                    "INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
                                                                    "WHERE EM.EntityTypeID = '96' " +
                                                                    ")  " +
                                                                "AND EM.EntityTypeID = '101' " +
                                                                ") " +
                                                        "AND EntityTypeParamID = '15' " +
                                                        "), '') AS HRManagerEmail " +
                                                        "FROM HRW_Employee Emp " +
                                                                    "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                                                    "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                                                    "WHERE Emp.EmployeeCode = IN ( " +
                                                                        "SELECT Emp.EmployeeId " +
                                                                        "FROM HRW_Employee Emp " +
                                                                        "INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
                                                                        "INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
                                                                        "WHERE EM.EntityTypeID = '96' " +
                                                        ") " +
                                            "AND EM.EntityTypeID = '101' ";

                var sequenceQueryResult = db.Database.SqlQuery<EmailPersonDetails>(sequenceMaxQuery, new SqlParameter("employeeCode", employeeCode)).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    EmailPersonDetails emailPersonDetailsVal = sequenceQueryResult;
                    return emailPersonDetailsVal;
                }
            }
            return null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this); //Hey, GC: don't bother calling finalize later
        }
    }
}