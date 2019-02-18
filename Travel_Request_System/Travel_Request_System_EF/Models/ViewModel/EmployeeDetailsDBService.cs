using System.Linq;

namespace Travel_Request_System_EF.Models.ViewModel
{
    public class EmployeeDetailsDBService
    {
        //Cost Center Manager of the Employee 

        public string EmployeeManager(string employeeCode, string entityTypeID)
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

        public string EmployeeManagerDetails(string employeeCode, string entityTypeID)
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

        public string DepartmentHeadDetails(string employeeCode, string entityTypeID)
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
        public FullEmployeeDetail FullEmployeeDetails(string employeeCode, string entityID)
        {
            using (var db = new BTCEntities())
            {
                string sequenceMaxQuery = "SELECT a.EmployeeID,a.EmployeeCode,a.FullName as Name,cast(a.HireDate as date) as HireDate,a.TerminationDate,a.EntityTypeId,c.Description " +
                                            ", a.EntityId,b.Description as Costcenter, b.EntityCode as CompanyCode " +
                                            ",isnull((Select[ParamValue] FROM[HRWorks].[dbo].[HRW_VEmployeeFields]" +
                                            "where EmployeeId in (select EmployeeId from[HRWorks].[dbo].HRW_Employee where EmployeeCode = @EmpCode and RecordType = 'EMP') and[EntityTypeParamID] = '15'),'') as Email " +
                                            ",isnull((select Value from[HRWorks].[dbo].HRW_VEmpEntityValues " +
                                            "where EmployeeId in (Select EmployeeId from hrworks.dbo.HRW_Employee where EmployeeCode = @EmpCode and RecordType = 'EMP') and EntityTypeId = '10'),'' ) as nvDept " +
                                            ",isnull((select EntityCode from[HRWorks].[dbo].[ORG_EntityMaster] " +
                                            "where EntityId in (Select EntityId from[HRWorks].[dbo].HRW_VEmpEntityValues where EmployeeCode = @EmpCode and RecordType = 'EMP') and EntityTypeId = '10'),'' ) as nvDeptCode " +
                                            ",isnull((select Picture from[HRWorks].[dbo].[HRW_Picture] " +
                                            "where[TableReferenceID] in (Select EmployeeId from hrworks.dbo.HRW_Employee where EmployeeCode = @EmpCode and RecordType = 'EMP') and Category = 'EMPPHOTO' ),'' ) as Photo " +
                                            ",isnull((select Value from[HRWorks].[dbo].HRW_VEmpEntityValues " +
                                            "where EmployeeId in (Select EmployeeId from hrworks.dbo.HRW_Employee where EmployeeCode = @EmpCode and RecordType = 'EMP') and EntityTypeId = '88'),'' ) as nvDesignation " +
                                            "FROM[HRWorks].[dbo].HRW_VEmpEntityValues as a  " +
                                            "inner join " +
                                            "[HRWorks].[dbo].[ORG_EntityMaster] as b on a.EntityId=b.EntityId " +
                                            "inner join " +
                                            "[HRWorks].[dbo].[ORG_EntityType] as c on a.EntityTypeId=c.EntityTypeId " +
                                            "where  a.employeecode='" + employeeCode + "' and a.EntityTypeId='" + entityID + "'  and TerminationDate is null and a.RecordType='EMP'";

                var sequenceQueryResult = db.Database.SqlQuery<FullEmployeeDetail>(sequenceMaxQuery).FirstOrDefault();

                if (sequenceQueryResult != null)
                {
                    FullEmployeeDetail fullEmployeeDetailsVal = sequenceQueryResult;
                    return fullEmployeeDetailsVal;
                }
            }
            return null;
        }

        //Employee Details – View
        public MiniEmployeeDetail SimpleEmployeeDetails(string employeeCode)
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
        public dynamic DepartmentHead(string employeeCode, string entityID)
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
        public QatarDetails QatarIDDetails(string employeeCode)
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
        public PassportDetails PassportDetails(string employeeCode)
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


    }
}