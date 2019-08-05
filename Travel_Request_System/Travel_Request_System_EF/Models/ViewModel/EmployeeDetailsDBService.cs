using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Travel_Request_System_EF.Models.ViewModel
{
    public class EmployeeDetailsDBService : IDisposable
    {
        string employeeCode = "";

        public EmployeeDetailsDBService(string employeeCode = "", string employeeID = "")
        {
            if (string.IsNullOrEmpty(employeeCode))
            {
                this.employeeCode = getEmployeeCode(employeeID);
            }
            else
            {
                this.employeeCode = employeeCode;
            }
        }

        public string getEmployeeCode(string employeeID)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT " +
                                            "Emp.EmployeeCode " +
                                            "FROM HRW_Employee Emp " +
                                            "WHERE " +
                                            "Emp.RecordType = 'EMP' " +
                                            "AND Emp.EmployeeID = '" + employeeID + "'";

                var sequenceQueryResult = db.Database.SqlQuery<string>(sequenceMaxQuery).FirstOrDefault();

                string EmployeeCode = string.Empty;

                if (sequenceQueryResult != null)
                {
                    EmployeeCode = sequenceQueryResult.ToString();
                }

                return EmployeeCode;
            }
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
                                            "Emp.RecordType = 'EMP' " +
                                            "AND Emp.EmployeeCode = '" + employeeCode + "'" +
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
                                            "Emp.RecordType = 'EMP' " +
                                            "AND Emp.EmployeeCode = '" + employeeCode + "'" +
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
                                            "Emp.RecordType = 'EMP' " +
                                            "AND Emp.EmployeeCode = '" + employeeCode + "'" +
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
                string sequenceMaxQuery = "SELECT CAST(a.EmployeeID AS NVARCHAR(100)) EmployeeID " +
"        ,CAST(a.EmployeeCode AS NVARCHAR(100)) EmployeeCode " +
"        ,CAST(a.FullName AS NVARCHAR(100)) FullName " +
"        ,CAST(a.FirstName AS NVARCHAR(100)) FirstName  " +
"        ,CAST(a.LastName AS NVARCHAR(100)) LastName " +
"        , isnull(( " +
"        SELECT ParamValue " +
"        FROM HRW_VEmployeeFields " +
"        WHERE EmployeeId = a.EmployeeId " +
"        AND EntityTypeParamID = '15' " +
"        ), '') AS Email " +
"        ,CAST(( " +
"                        SELECT TOP 1 Value " +
"                        FROM HRW_VEmpEntityValues " +
"                        WHERE EmployeeId IN ( " +
"                                        SELECT EmployeeId " +
"                                        FROM HRW_Employee " +
"                                        WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                AND RecordType = 'EMP' " +
"                                        ) " +
"                                AND EntityTypeId = '88' " +
"                        ) AS NVARCHAR(100)) AS Designation " +
"        ,CAST(( " +
"                        SELECT TOP 1 Value " +
"                        FROM HRW_VEmpEntityValues " +
"                        WHERE EmployeeId IN ( " +
"                                        SELECT EmployeeId " +
"                                        FROM HRW_Employee " +
"                                        WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                AND RecordType = 'EMP' " +
"                                        ) " +
"                                AND EntityTypeId = '10' " +
"                        ) AS NVARCHAR(100)) AS Department " +
"        ,CAST(( " +
"                        SELECT TOP 1 EM.Description " +
"                        FROM HRW_Employee Emp " +
"                        INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
"                        INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
"                        WHERE Emp.EmployeeCode = '" + employeeCode + "' " +
"                                AND EM.EntityTypeID = '96' " +
"                                AND Emp.RecordType = 'EMP' " +
"                        ) AS NVARCHAR(100)) AS DepartmentHead " +
"        ,CAST(isnull(( " +
"                                SELECT TOP 1 ParamValue " +
"                                FROM HRW_VEmployeeFields " +
"                                WHERE EmployeeId IN ( " +
"                                                SELECT Emp.EmployeeId " +
"                                                FROM HRW_Employee Emp " +
"                                                INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
"                                                INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
"                                                WHERE Emp.EmployeeCode = '" + employeeCode + "' " +
"                                                        AND EM.EntityTypeID = '96' " +
"                                                        AND Emp.RecordType = 'EMP' " +
"                                                ) " +
"                                        AND EntityTypeParamID = '15' " +
"                                ), '') AS NVARCHAR(100)) AS DepartmentHeadEmail " +
"        ,CAST(( " +
"                        SELECT TOP 1 Value " +
"                        FROM HRW_VEmpEntityValues " +
"                        WHERE EmployeeId IN ( " +
"                                        SELECT EmployeeId " +
"                                        FROM HRW_Employee " +
"                                        WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                AND RecordType = 'EMP' " +
"                                        ) " +
"                                AND EntityTypeId = '10' " +
"                        ) AS NVARCHAR(100)) AS CostCenter " +
"        ,CAST(( " +
"                        SELECT TOP 1 EM.Description " +
"                        FROM HRW_Employee Emp " +
"                        INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
"                        INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
"                        WHERE Emp.EmployeeCode = '" + employeeCode + "' " +
"                                AND Emp.RecordType = 'EMP' " +
"                                AND EM.EntityTypeID = '101' " +
"                        ) AS NVARCHAR(100)) AS CostCenterHead " +
"        ,CAST(isnull(( " +
"                                SELECT TOP 1 ParamValue " +
"                                FROM HRW_VEmployeeFields " +
"                                WHERE EmployeeId IN ( " +
"                                                SELECT Emp.EmployeeId " +
"                                                FROM HRW_Employee Emp " +
"                                                INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
"                                                INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
"                                                WHERE Emp.EmployeeCode = '" + employeeCode + "' " +
"                                                AND Emp.RecordType = 'EMP' " +
"                                                        AND EM.EntityTypeID = '101' " +
"                                                ) " +
"                                        AND EntityTypeParamID = '15' " +
"                                ), '') AS NVARCHAR(100)) AS CostCenterHeadEmail " +
"        ,CAST(PassportID AS NVARCHAR(100)) PassportID " +
"        ,CAST(PassportexpireDate AS NVARCHAR(100)) PassportexpireDate " +
"        ,CAST(Passportissuedate AS NVARCHAR(100)) Passportissuedate " +
"        ,CAST(QID AS NVARCHAR(100)) QatarID " +
"        ,CAST(QIDEDate AS NVARCHAR(100)) QIDEDate " +
"        ,CAST(( " +
"                        SELECT TOP 1 Value " +
"                        FROM HRW_VEmpEntityValues " +
"                        WHERE EmployeeId IN ( " +
"                                        SELECT EmployeeId " +
"                                        FROM HRW_Employee " +
"                                        WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                AND RecordType = 'EMP' " +
"                                        ) " +
"                                AND EntityTypeId = '84' " +
"                        ) AS NVARCHAR(100)) AS Location " +
"        ,CAST(( " +
"                        SELECT TOP 1 Value " +
"                        FROM HRW_VEmpEntityValues " +
"                        WHERE EmployeeId IN ( " +
"                                        SELECT EmployeeId " +
"                                        FROM HRW_Employee " +
"                                        WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                AND RecordType = 'EMP' " +
"                                        ) " +
"                                AND EntityTypeId = '85' " +
"                        ) AS NVARCHAR(100)) AS Section " +
"        ,CAST(( " +
"                        SELECT TOP 1 Value " +
"                        FROM HRW_VEmpEntityValues " +
"                        WHERE EmployeeId IN ( " +
"                                        SELECT EmployeeId " +
"                                        FROM HRW_Employee " +
"                                        WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                AND RecordType = 'EMP' " +
"                                        ) " +
"                                AND EntityTypeId = '93' " +
"                        ) AS NVARCHAR(100)) AS Contact " +
"        ,CAST(cast(a.HireDate AS DATE) AS NVARCHAR(100)) AS HireDate " +
"        ,CAST(a.TerminationDate AS NVARCHAR(100)) " +
"        ,CAST(isnull(( " +
"                                SELECT TOP 1 ParamValue " +
"                                FROM HRW_VEmployeeFields " +
"                                WHERE EmployeeId IN ( " +
"                                                SELECT EmployeeId " +
"                                                FROM HRW_Employee " +
"                                                WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                        AND RecordType = 'EMP' " +
"                                                ) " +
"                                        AND EntityTypeParamID = '15' " +
"                                ), '') AS NVARCHAR(100)) AS Email " +
"        ,CAST(isnull(( " +
"                                SELECT TOP 1 Value " +
"                                FROM HRW_VEmpEntityValues " +
"                                WHERE EmployeeId IN ( " +
"                                                SELECT EmployeeId " +
"                                                FROM HRW_Employee " +
"                                                WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                        AND RecordType = 'EMP' " +
"                                                ) " +
"                                        AND EntityTypeId = '10' " +
"                                ), '') AS NVARCHAR(100)) AS nvDept " +
"        ,CAST(isnull(( " +
"                                SELECT TOP 1 EntityCode " +
"                                FROM ORG_EntityMaster " +
"                                WHERE EntityId IN ( " +
"                                                SELECT EntityId " +
"                                                FROM HRW_VEmpEntityValues " +
"                                                WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                        AND RecordType = 'EMP' " +
"                                                ) " +
"                                        AND EntityTypeId = '10' " +
"                                ), '') AS NVARCHAR(100)) AS nvDeptCode " +
"        ,CAST(isnull(( " +
"                                SELECT TOP 1 Value " +
"                                FROM HRW_VEmpEntityValues " +
"                                WHERE EmployeeId IN ( " +
"                                                SELECT EmployeeId " +
"                                                FROM HRW_Employee " +
"                                                WHERE EmployeeCode = '" + employeeCode + "' " +
"                                                        AND RecordType = 'EMP' " +
"                                                ) " +
"                                        AND EntityTypeId = '88' " +
"                                ), '') AS NVARCHAR(100)) AS nvDesignation " +
"FROM HRW_Employee AS a " +
"INNER JOIN QATARIDDetails AS d ON a.EmployeeCode = d.EMPLOYEECODE " +
"INNER JOIN PassportDetails AS e ON a.EmployeeCode = e.EMPLOYEECODE " +
"WHERE a.EmployeeCode = '" + employeeCode + "' " +
"        AND TerminationDate IS NULL " +
"        AND a.RecordType = 'EMP'; ";

                var sequenceQueryResult = db.Database.SqlQuery<FullEmployeeDetail>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    FullEmployeeDetail fullEmployeeDetailsVal = sequenceQueryResult;
                    return fullEmployeeDetailsVal;
                }
            }
            return new FullEmployeeDetail();
        }

        //Employee Details
        public List<FullEmployeeDetail> AllEmployeeDetails(int numberOfRecords = 0)
        {
            string Toprecords = "";
            if (numberOfRecords > 0)
            {
                Toprecords = "TOP " + numberOfRecords + " ";
            }
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT " + Toprecords + " CAST(a.EmployeeID AS NVARCHAR(100)) EmployeeID  " +
"        ,CAST(a.EmployeeCode AS NVARCHAR(100)) EmployeeCode  " +
"        ,CAST(a.FullName AS NVARCHAR(100)) FullName  " +
"        ,CAST(a.FirstName AS NVARCHAR(100)) FirstName  " +
"        ,CAST(a.LastName AS NVARCHAR(100)) LastName " +
"        , isnull(( " +
"        SELECT ParamValue " +
"        FROM HRW_VEmployeeFields " +
"        WHERE EmployeeId = a.EmployeeId " +
"        AND EntityTypeParamID = '15' " +
"        ), '') AS Email " +
"        ,CAST((  " +
"                        SELECT TOP 1 Value  " +
"                        FROM HRW_VEmpEntityValues  " +
"                        WHERE EmployeeId IN (  " +
"                                        SELECT EmployeeId  " +
"                                        FROM HRW_Employee  " +
"                                        WHERE EmployeeCode = a.EmployeeCode  " +
"                                                AND RecordType = 'EMP'  " +
"                                        )  " +
"                                AND EntityTypeId = '88'  " +
"                        ) AS NVARCHAR(100)) AS Designation  " +
"        ,CAST((  " +
"                        SELECT TOP 1 Value  " +
"                        FROM HRW_VEmpEntityValues  " +
"                        WHERE EmployeeId IN (  " +
"                                        SELECT EmployeeId  " +
"                                        FROM HRW_Employee  " +
"                                        WHERE EmployeeCode = a.EmployeeCode  " +
"                                                AND RecordType = 'EMP'  " +
"                                        )  " +
"                                AND EntityTypeId = '10'  " +
"                        ) AS NVARCHAR(100)) AS Department  " +
"        ,CAST((  " +
"                        SELECT TOP 1 EM.Description  " +
"                        FROM HRW_Employee Emp  " +
"                        INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId)  " +
"                        INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId)  " +
"                        WHERE Emp.EmployeeCode = a.EmployeeCode  " +
"                                AND EM.EntityTypeID = '96'  " +
"                                AND Emp.RecordType = 'EMP' " +
"                        ) AS NVARCHAR(100)) AS DepartmentHead  " +
"        ,CAST(isnull((  " +
"                                SELECT TOP 1 ParamValue  " +
"                                FROM HRW_VEmployeeFields  " +
"                                WHERE EmployeeId IN (  " +
"                                                SELECT Emp.EmployeeId  " +
"                                                FROM HRW_Employee Emp  " +
"                                                INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId)  " +
"                                                INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId)  " +
"                                                WHERE Emp.EmployeeCode = a.EmployeeCode  " +
"                                                        AND EM.EntityTypeID = '96'  " +
"                                                        AND Emp.RecordType = 'EMP' " +
"                                                )  " +
"                                        AND EntityTypeParamID = '15'  " +
"                                ), '') AS NVARCHAR(100)) AS DepartmentHeadEmail  " +
"        ,CAST((  " +
"                        SELECT TOP 1 Value  " +
"                        FROM HRW_VEmpEntityValues  " +
"                        WHERE EmployeeId IN (  " +
"                                        SELECT EmployeeId  " +
"                                        FROM HRW_Employee  " +
"                                        WHERE EmployeeCode = a.EmployeeCode  " +
"                                                AND RecordType = 'EMP'  " +
"                                        )  " +
"                                AND EntityTypeId = '10'  " +
"                        ) AS NVARCHAR(100)) AS CostCenter  " +
"        ,CAST((  " +
"                        SELECT TOP 1 EM.Description  " +
"                        FROM HRW_Employee Emp  " +
"                        INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId)  " +
"                        INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId)  " +
"                        WHERE Emp.EmployeeCode = a.EmployeeCode  " +
"                                AND EM.EntityTypeID = '101'  " +
"                                AND Emp.RecordType = 'EMP' " +
"                        ) AS NVARCHAR(100)) AS CostCenterHead  " +
"        ,CAST(isnull((  " +
"                                SELECT TOP 1 ParamValue  " +
"                                FROM HRW_VEmployeeFields  " +
"                                WHERE EmployeeId IN (  " +
"                                                SELECT Emp.EmployeeId  " +
"                                                FROM HRW_Employee Emp  " +
"                                                INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId)  " +
"                                                INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId)  " +
"                                                WHERE Emp.EmployeeCode = a.EmployeeCode  " +
"                                                        AND EM.EntityTypeID = '101'  " +
"                                                        AND Emp.RecordType = 'EMP' " +
"                                                )  " +
"                                        AND EntityTypeParamID = '15'  " +
"                                ), '') AS NVARCHAR(100)) AS CostCenterHeadEmail  " +
"        ,CAST(PassportID AS NVARCHAR(100)) PassportID  " +
"        ,CAST(PassportexpireDate AS NVARCHAR(100)) PassportexpireDate  " +
"        ,CAST(Passportissuedate AS NVARCHAR(100)) Passportissuedate  " +
"        ,CAST(QID AS NVARCHAR(100)) QatarID  " +
"        ,CAST(QIDEDate AS NVARCHAR(100)) QIDEDate  " +
"        ,CAST((  " +
"                        SELECT TOP 1 Value  " +
"                        FROM HRW_VEmpEntityValues  " +
"                        WHERE EmployeeId IN (  " +
"                                        SELECT EmployeeId  " +
"                                        FROM HRW_Employee  " +
"                                        WHERE EmployeeCode = a.EmployeeCode  " +
"                                                AND RecordType = 'EMP'  " +
"                                        )  " +
"                                AND EntityTypeId = '84'  " +
"                        ) AS NVARCHAR(100)) AS Location  " +
"        ,CAST((  " +
"                        SELECT TOP 1 Value  " +
"                        FROM HRW_VEmpEntityValues  " +
"                        WHERE EmployeeId IN (  " +
"                                        SELECT EmployeeId  " +
"                                        FROM HRW_Employee  " +
"                                        WHERE EmployeeCode = a.EmployeeCode  " +
"                                                AND RecordType = 'EMP'  " +
"                                        )  " +
"                                AND EntityTypeId = '85'  " +
"                        ) AS NVARCHAR(100)) AS Section  " +
"        ,CAST((  " +
"                        SELECT TOP 1 Value  " +
"                        FROM HRW_VEmpEntityValues  " +
"                        WHERE EmployeeId IN (  " +
"                                        SELECT EmployeeId  " +
"                                        FROM HRW_Employee  " +
"                                        WHERE EmployeeCode = a.EmployeeCode  " +
"                                                AND RecordType = 'EMP'  " +
"                                        )  " +
"                                AND EntityTypeId = '93'  " +
"                        ) AS NVARCHAR(100)) AS Contact  " +
"        ,CAST(cast(a.HireDate AS DATE) AS NVARCHAR(100)) AS HireDate  " +
"        ,CAST(a.TerminationDate AS NVARCHAR(100))  " +
"        ,CAST(isnull((  " +
"                                SELECT TOP 1 ParamValue  " +
"                                FROM HRW_VEmployeeFields  " +
"                                WHERE EmployeeId IN (  " +
"                                                SELECT EmployeeId  " +
"                                                FROM HRW_Employee  " +
"                                                WHERE EmployeeCode = a.EmployeeCode  " +
"                                                        AND RecordType = 'EMP'  " +
"                                                )  " +
"                                        AND EntityTypeParamID = '15'  " +
"                                ), '') AS NVARCHAR(100)) AS Email  " +
"        ,CAST(isnull((  " +
"                                SELECT TOP 1 Value  " +
"                                FROM HRW_VEmpEntityValues  " +
"                                WHERE EmployeeId IN (  " +
"                                                SELECT EmployeeId  " +
"                                                FROM HRW_Employee  " +
"                                                WHERE EmployeeCode = a.EmployeeCode  " +
"                                                        AND RecordType = 'EMP'  " +
"                                                )  " +
"                                        AND EntityTypeId = '10'  " +
"                                ), '') AS NVARCHAR(100)) AS nvDept  " +
"        ,CAST(isnull((  " +
"                                SELECT TOP 1 EntityCode  " +
"                                FROM ORG_EntityMaster  " +
"                                WHERE EntityId IN (  " +
"                                                SELECT EntityId  " +
"                                                FROM HRW_VEmpEntityValues  " +
"                                                WHERE EmployeeCode = a.EmployeeCode  " +
"                                                        AND RecordType = 'EMP'  " +
"                                                )  " +
"                                        AND EntityTypeId = '10'  " +
"                                ), '') AS NVARCHAR(100)) AS nvDeptCode  " +
"        ,CAST(isnull((  " +
"                                SELECT TOP 1 Value  " +
"                                FROM HRW_VEmpEntityValues  " +
"                                WHERE EmployeeId IN (  " +
"                                                SELECT EmployeeId  " +
"                                                FROM HRW_Employee  " +
"                                                WHERE EmployeeCode = a.EmployeeCode  " +
"                                                        AND RecordType = 'EMP'  " +
"                                                )  " +
"                                        AND EntityTypeId = '88'  " +
"                                ), '') AS NVARCHAR(100)) AS nvDesignation  " +
"FROM HRW_Employee AS a  " +
"INNER JOIN QATARIDDetails AS d ON a.EmployeeCode = d.EMPLOYEECODE  " +
"INNER JOIN PassportDetails AS e ON a.EmployeeCode = e.EMPLOYEECODE  " +
"WHERE TerminationDate IS NULL  " +
"        AND a.RecordType = 'EMP';";

                var sequenceQueryResult = db.Database.SqlQuery<FullEmployeeDetail>(sequenceMaxQuery).ToList();

                if (sequenceQueryResult != null)
                {
                    List<FullEmployeeDetail> fullEmployeeDetailsVal = sequenceQueryResult;
                    return fullEmployeeDetailsVal;
                }
            }
            return new List<FullEmployeeDetail>();
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
                                            "Emp.RecordType = 'EMP' " +
                                            "AND Emp.EmployeeCode = '" + employeeCode + "'" +
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
                string sequenceMaxQuery = "SELECT CAST(Emp.EmployeeID AS nvarchar(100)) EmployeeID " +
                                            ", CAST(Emp.EmployeeCode AS nvarchar(100)) EmployeeCode " +
                                            ", CAST(Emp.FullName AS nvarchar(100)) FullName " +
                                            ", CAST(isnull(( " +
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
                                                    "), '')  AS nvarchar(100)) AS HRManagerEmail " +
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
            return new EmailPersonDetails();
        }

        public EmailPersonDetails HRNotificationDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT CAST(Emp.EmployeeID AS nvarchar(100)) EmployeeID " +
                                            ", CAST(Emp.EmployeeCode AS nvarchar(100)) EmployeeCode " +
                                            ", CAST(Emp.FullName AS nvarchar(100)) FullName " +
                                            ", CAST(isnull(( " +
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
                                                    "), '')  AS nvarchar(100)) AS HRManagerEmail " +
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
            return new EmailPersonDetails();
        }

        public EmailPersonDetails TravelCoordinatorDetails()
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT CAST(Emp.EmployeeID AS nvarchar(100)) EmployeeID " +
                                            ", CAST(Emp.EmployeeCode AS nvarchar(100)) EmployeeCode " +
                                            ", CAST(Emp.FullName AS nvarchar(100)) FullName " +
                                            ", CAST(isnull(( " +
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
                                                    "), '')  AS nvarchar(100)) AS HRManagerEmail " +
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
            return new EmailPersonDetails();
        }

        public EmailPersonDetails DepartmentHeadMailDetails(string EmployeeCode)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT CAST(Emp.EmployeeID AS nvarchar(100)) EmployeeID " +
                                            ", CAST(Emp.EmployeeCode AS nvarchar(100)) EmployeeCode " +
                                            ", CAST(Emp.FullName AS nvarchar(100)) FullName " +
                                            ",CAST(isnull(( " +
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
                                                                    "AND EM.EntityTypeID = '101' " +
                                                                    "AND Emp.RecordType = 'EMP' " +
                                                                    ")  " +
                                                                "AND EM.EntityTypeID = '101' " +
                                                                ") " +
                                                        "AND EntityTypeParamID = '15' " +
                                                        "), '') AS nvarchar(100)) AS HRManagerEmail " +
                                                        "FROM HRW_Employee Emp " +
                                                                    "INNER JOIN ORG_EmpEntityLink Oel ON(Emp.EmployeeID = Oel.EmployeeId) " +
                                                                    "INNER JOIN ORG_EntityMaster EM ON(EM.EntityId = Oel.EntityId) " +
                                                                    "WHERE Emp.EmployeeCode IN ( " +
                                                                        "SELECT Emp.EmployeeId " +
                                                                        "FROM HRW_Employee Emp " +
                                                                        "INNER JOIN ORG_EmpEntityLink Oel ON (Emp.EmployeeID = Oel.EmployeeId) " +
                                                                        "INNER JOIN ORG_EntityMaster EM ON (EM.EntityId = Oel.EntityId) " +
                                                                        "WHERE Emp.EmployeeCode = '@employeeCode' " +
                                                                        "AND EM.EntityTypeID = '101' " +
                                                                        "AND Emp.RecordType = 'EMP' " +
                                                        ") " +
                                            "AND EM.EntityTypeID = '101' ";

                var sequenceQueryResult = db.Database.SqlQuery<EmailPersonDetails>(sequenceMaxQuery, new SqlParameter("employeeCode", EmployeeCode)).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    EmailPersonDetails emailPersonDetailsVal = sequenceQueryResult;
                    return emailPersonDetailsVal;
                }
            }
            return new EmailPersonDetails();
        }

        public List<string> EmployeesUnderDepartmentHeadDetails(string EmployeeCode)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "select HRW_employee.EmployeeCode FROM HRW_employee where Employeeid in( SELECT oel.employeeid FROM " +
                                            "ORG_EmpEntityLink Oel " +
                                            "INNER JOIN " +
                                            "ORG_EntityMaster EM " +
                                            "ON " +
                                            "(EM.EntityId=Oel.EntityId) " +
                                            "WHERE EM.EntityTypeID='101' AND EM.Description = " +
                                            "(SELECT FullName from HRW_Employee where EmployeeCode = '@employeeCode' AND RecordType = 'EMP' )) ";

                var sequenceQueryResult = db.Database.SqlQuery<List<string>>(sequenceMaxQuery, new SqlParameter("employeeCode", EmployeeCode)).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    List<string> EmpCodes = sequenceQueryResult;
                    return EmpCodes;
                }
            }
            return new List<string>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this); //Hey, GC: don't bother calling finalize later
        }
    }
}