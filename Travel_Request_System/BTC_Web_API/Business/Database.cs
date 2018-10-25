using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BTC_Web_API.Models;

namespace BTC_Web_API.Business
{
    public class Employee_Mapper : IDataMapper<HRW_Employee>
    {
        public override HRW_Employee Create(HRW_Employee instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO customer (FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive) " +
                           "VALUES (@FirstName, @MiddleName,@Lastname, @Email, @Password, @DOB, @Gender,@Mobile,@Address,@City,@Country,@Pincode,@RoleID,0); " +
                           "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = instance.FirstName;
                    command.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = instance.MiddleName;
                    command.Parameters.Add("@Lastname", SqlDbType.NVarChar).Value = instance.LastName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = instance.Email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = instance.Password;
                    command.Parameters.Add("@DOB", SqlDbType.DateTime).Value = instance.DOB;
                    command.Parameters.Add("@Gender", SqlDbType.Bit).Value = instance.Gender;
                    command.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = instance.Mobile;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = instance.Address;
                    command.Parameters.Add("@City", SqlDbType.NVarChar).Value = instance.City;
                    command.Parameters.Add("@Country", SqlDbType.NVarChar).Value = instance.Country;
                    command.Parameters.Add("@Pincode", SqlDbType.NVarChar).Value = instance.Pincode;
                    command.Parameters.Add("@RoleID", SqlDbType.Int).Value = instance.RoleID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM customer WHERE CustomerID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override HRW_Employee Select(int ID, out Exception exError)
        {
            HRW_Employee returnValue = new HRW_Employee();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM customer Where CustomerID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new HRW_Employee()
                            {
                                CustomerID = reader.SafeGetInt("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.SafeGetDateTime("DOB"),
                                Gender = reader.SafeGetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.SafeGetBoolean("IsActive"),
                                RoleID = reader.SafeGetInt("RoleID")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<HRW_Employee> SelectAll(out Exception exError)
        {
            List<HRW_Employee> returnValue = new List<HRW_Employee>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM customer ", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HRW_Employee()
                            {
                                CustomerID = reader.SafeGetInt("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.SafeGetDateTime("DOB"),
                                Gender = reader.SafeGetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.SafeGetBoolean("IsActive"),
                                RoleID = reader.SafeGetInt("RoleID")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<HRW_Employee> SelectedID(List<int> IDs, out Exception exError)
        {
            List<HRW_Employee> returnValue = new List<HRW_Employee>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM customer WHERE CustomerID IN (@CustomerIDs)", (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerIDs", SqlDbType.NVarChar).Value = string.Join(",", IDs.ToArray());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HRW_Employee()
                            {
                                CustomerID = reader.SafeGetInt("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.SafeGetDateTime("DOB"),
                                Gender = reader.SafeGetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.SafeGetBoolean("IsActive"),
                                RoleID = reader.SafeGetInt("RoleID")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override HRW_Employee Update(HRW_Employee instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE customer SET " +
                    "FirstName = @FirstName, " +
                    "MiddleName = @MiddleName," +
                    "Lastname = @Lastname," +
                    "Email = @Email," +
                    "Password = @Password," +
                    "DOB = @DOB," +
                    "Gender = @Gender," +
                    "Mobile = @Mobile," +
                    "Address = @Address," +
                    "City = @City," +
                    "Country = @Country," +
                    "Pincode = @Pincode, " +
                    "RoleID = @RoleID " +
                    "WHERE CustomerID = @CustomerID ";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = instance.FirstName;
                    command.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = instance.MiddleName;
                    command.Parameters.Add("@Lastname", SqlDbType.NVarChar).Value = instance.LastName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = instance.Email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = instance.Password;
                    command.Parameters.Add("@DOB", SqlDbType.DateTime).Value = instance.DOB;
                    command.Parameters.Add("@Gender", SqlDbType.Bit).Value = instance.Gender;
                    command.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = instance.Mobile;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = instance.Address;
                    command.Parameters.Add("@City", SqlDbType.NVarChar).Value = instance.City;
                    command.Parameters.Add("@Country", SqlDbType.NVarChar).Value = instance.Country;
                    command.Parameters.Add("@Pincode", SqlDbType.NVarChar).Value = instance.Pincode;
                    command.Parameters.Add("@RoleID", SqlDbType.Int).Value = instance.RoleID;
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = instance.CustomerID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.CustomerID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public HRW_Employee LoginCheck(string Email, string Password, out Exception exError)
        {
            HRW_Employee returnValue = new HRW_Employee();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT CustomerID, FirstName, MiddleName,Lastname, Email, Password, DOB, Gender,Mobile,Address,City,Country,Pincode,RoleID,IsActive FROM customer WHERE Email = '" + Email + "' AND Password = '" + Password + "'", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new HRW_Employee()
                            {
                                CustomerID = reader.SafeGetInt("CustomerID"),
                                FirstName = reader.SafeGetString("FirstName"),
                                MiddleName = reader.SafeGetString("MiddleName"),
                                LastName = reader.SafeGetString("Lastname"),
                                Email = reader.SafeGetString("Email"),
                                Password = reader.SafeGetString("Password"),
                                DOB = reader.SafeGetDateTime("DOB"),
                                Gender = reader.SafeGetBoolean("Gender"),
                                Mobile = reader.SafeGetString("Mobile"),
                                Address = reader.SafeGetString("Address"),
                                City = reader.SafeGetString("City"),
                                Country = reader.SafeGetString("Country"),
                                Pincode = reader.SafeGetString("Pincode"),
                                IsActive = reader.SafeGetBoolean("IsActive"),
                                RoleID = reader.SafeGetInt("RoleID")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return returnValue;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool ResetPassword(string Email, string Password, out Exception exError)
        {
            bool returnValue = false;
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE customer SET Password = '" + Password + "' WHERE Email = '" + Email + "' AND IsActive = 0 ", (SqlConnection)this.Connection))
                {
                    int Rows = Convert.ToInt32(command.ExecuteScalar());
                    returnValue = Rows > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool ForgotPassword(string Email, out Exception exError)
        {
            bool returnValue = false;
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE customer SET IsActive = 0 WHERE Email = '" + Email + "'", (SqlConnection)this.Connection))
                {
                    int Rows = Convert.ToInt32(command.ExecuteScalar());
                    returnValue = Rows > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool UserCheck(string Email, out Exception exError)
        {
            bool returnValue = false;
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT Count(1) FROM customer WHERE Email = '" + Email + "'", (SqlConnection)this.Connection))
                {
                    int Rows = Convert.ToInt32(command.ExecuteScalar());
                    returnValue = Rows > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool SetUserActive(int ID, out Exception exError)
        {
            bool returnValue = false;
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE customer SET IsActive = 1 WHERE CustomerID = " + ID, (SqlConnection)this.Connection))
                {
                    int Rows = Convert.ToInt32(command.ExecuteScalar());
                    returnValue = Rows > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }
    }

    public class EmployeeDetail_Mapper : IDataMapper<HRW_EmpEntityParamValues>
    {
        public override HRW_EmpEntityParamValues Create(HRW_EmpEntityParamValues instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO `order` (CustomerID, IsNew,Fault,Evidence, Company, Reference, OrderTypeID,OrderStatusID,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,BlindTypeID,Transport,OrderM2,Notes) " +
                           "VALUES (@CustomerID, @IsNew, @Fault,@Evidence, @Company, @Reference, @OrderTypeID, @OrderStatusID, @OrderDate, @NumbOfBlinds, @ConsignNoteNum, @CompleteDate, @DeliveryDate, @DepartureDate, @ArrivalDate,@BlindTypeID,@Transport, @OrderM2,@Notes); " +
                           "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = instance.CustomerID;
                    command.Parameters.Add("@IsNew", SqlDbType.Bit).Value = instance.IsNew;
                    command.Parameters.Add("@Fault", SqlDbType.NVarChar).Value = instance.Fault;
                    command.Parameters.Add("@Evidence", SqlDbType.Bit).Value = instance.Evidence;
                    command.Parameters.Add("@Company", SqlDbType.NVarChar).Value = instance.Company;
                    command.Parameters.Add("@Reference", SqlDbType.NVarChar).Value = instance.Reference;
                    command.Parameters.Add("@OrderTypeID", SqlDbType.Int).Value = instance.OrderTypeID;
                    command.Parameters.Add("@OrderStatusID", SqlDbType.Int).Value = instance.OrderStatusID;
                    command.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = instance.OrderDate;
                    command.Parameters.Add("@NumbOfBlinds", SqlDbType.Int).Value = instance.NumbOfBlinds;
                    command.Parameters.Add("@ConsignNoteNum", SqlDbType.NVarChar).Value = instance.ConsignNoteNum;
                    command.Parameters.Add("@CompleteDate", SqlDbType.DateTime).Value = instance.CompleteDate;
                    command.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = instance.DeliveryDate;
                    command.Parameters.Add("@DepartureDate", SqlDbType.DateTime).Value = instance.DepartureDate;
                    command.Parameters.Add("@ArrivalDate", SqlDbType.DateTime).Value = instance.ArrivalDate;
                    command.Parameters.Add("@BlindTypeID", SqlDbType.Int).Value = instance.BlindTypeID;
                    command.Parameters.Add("@Transport", SqlDbType.NVarChar).Value = instance.Transport;
                    command.Parameters.Add("@OrderM2", SqlDbType.Decimal).Value = instance.OrderM2;
                    command.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = instance.Notes;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        query = "Update `order` SET OrderNumber = @OrderNumber WHERE OrderID = " + RetVal;
                        using (SqlCommand command1 = new SqlCommand(query, (SqlConnection)this.Connection))
                        {
                            command1.Parameters.Add("@OrderNumber", SqlDbType.NVarChar).Value = RetVal;
                            command1.ExecuteScalar();
                        }
                        Exception custException = new Exception();
                        instance.OrderID = RetVal;
                        instance.OrderNumber = RetVal.ToString();
                        return instance;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM `order` WHERE OrderID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override HRW_EmpEntityParamValues Select(int ID, out Exception exError)
        {
            HRW_EmpEntityParamValues returnValue = new HRW_EmpEntityParamValues();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    "Where OrderID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new HRW_EmpEntityParamValues()
                            {
                                OrderID = reader.SafeGetInt("OrderID"),
                                CustomerID = reader.SafeGetInt("CustomerID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<HRW_EmpEntityParamValues> SelectAll(out Exception exError)
        {
            List<HRW_EmpEntityParamValues> returnValue = new List<HRW_EmpEntityParamValues>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID ", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HRW_EmpEntityParamValues()
                            {
                                OrderID = reader.SafeGetInt("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                CustomerID = reader.SafeGetInt("CustomerID"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override HRW_EmpEntityParamValues Update(HRW_EmpEntityParamValues instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE `order` SET " +
                    //"CustomerID = @CustomerID ," +
                    "IsNew = @IsNew ," +
                    "Evidence = @Evidence," +
                    "Company = @Company, " +
                    "Reference = @Reference, " +
                    //"OrderTypeID = @OrderTypeID, " +
                    "OrderStatusID = @OrderStatusID," +
                    "OrderDate = @OrderDate," +
                    "NumbOfBlinds = @NumbOfBlinds," +
                    "ConsignNoteNum = @ConsignNoteNum," +
                    "CompleteDate = @CompleteDate," +
                    "DeliveryDate = @DeliveryDate," +
                    "DepartureDate = @DepartureDate," +
                    "ArrivalDate = @ArrivalDate," +
                    "BlindTypeID = @BlindTypeID," +
                    "Transport = @Transport," +
                    "OrderM2 = @OrderM2," +
                    "Notes = @Notes, " +
                    "IsApproved = @IsApproved " +
                    "WHERE OrderID = @OrderID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderID", SqlDbType.Int).Value = instance.OrderID;
                    //command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = instance.CustomerID;
                    command.Parameters.Add("@IsNew", SqlDbType.Bit).Value = instance.IsNew;
                    command.Parameters.Add("@Fault", SqlDbType.NVarChar).Value = instance.Fault;
                    command.Parameters.Add("@Evidence", SqlDbType.Bit).Value = instance.Evidence;
                    command.Parameters.Add("@Company", SqlDbType.NVarChar).Value = instance.Company;
                    command.Parameters.Add("@Reference", SqlDbType.NVarChar).Value = instance.Reference;
                    command.Parameters.Add("@OrderTypeID", SqlDbType.Int).Value = instance.OrderTypeID;
                    command.Parameters.Add("@OrderStatusID", SqlDbType.Int).Value = instance.OrderStatusID;
                    command.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = instance.OrderDate;
                    command.Parameters.Add("@NumbOfBlinds", SqlDbType.Int).Value = instance.NumbOfBlinds;
                    command.Parameters.Add("@ConsignNoteNum", SqlDbType.NVarChar).Value = instance.ConsignNoteNum;
                    command.Parameters.Add("@CompleteDate", SqlDbType.DateTime).Value = instance.CompleteDate;
                    command.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = instance.DeliveryDate;
                    command.Parameters.Add("@DepartureDate", SqlDbType.DateTime).Value = instance.DepartureDate;
                    command.Parameters.Add("@ArrivalDate", SqlDbType.DateTime).Value = instance.ArrivalDate;
                    command.Parameters.Add("@BlindTypeID", SqlDbType.Int).Value = instance.BlindTypeID;
                    command.Parameters.Add("@Transport", SqlDbType.NVarChar).Value = instance.Transport;
                    command.Parameters.Add("@OrderM2", SqlDbType.Decimal).Value = instance.OrderM2;
                    command.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = instance.Notes;
                    command.Parameters.Add("@IsApproved", SqlDbType.Bit).Value = instance.IsApproved;

                    int RetVal = Convert.ToInt32(command.ExecuteNonQuery());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.OrderID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<HRW_EmpEntityParamValues> SelectedID(List<int> IDs, out Exception exError)
        {
            List<HRW_EmpEntityParamValues> returnValue = new List<HRW_EmpEntityParamValues>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    "WHERE OrderID IN (@OrderIDs)", (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", SqlDbType.NVarChar).Value = string.Join(",", IDs.ToArray());
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HRW_EmpEntityParamValues()
                            {
                                OrderID = reader.SafeGetInt("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<HRW_EmpEntityParamValues> SelectedRole(int RoleID, out Exception exError)
        {
            List<HRW_EmpEntityParamValues> returnValue = new List<HRW_EmpEntityParamValues>();
            exError = null;

            try
            {
                string strFilterBy = "";
                switch (RoleID)
                {
                    case 1:
                        strFilterBy = " WHERE `order`.OrderStatusID IN (1,2) ";
                        break;
                    case 2:
                        strFilterBy = " WHERE `order`.OrderStatusID = 3";
                        break;
                    case 3:
                        strFilterBy = " WHERE `order`.OrderStatusID = 4";
                        break;
                    default:
                        break;
                }
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    " " + strFilterBy, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HRW_EmpEntityParamValues()
                            {
                                OrderID = reader.SafeGetInt("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<HRW_EmpEntityParamValues> SelectedCustomer(int ID, out Exception exError)
        {
            List<HRW_EmpEntityParamValues> returnValue = new List<HRW_EmpEntityParamValues>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    "WHERE CustomerID = @CustomerID ", (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = ID;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HRW_EmpEntityParamValues()
                            {

                                OrderID = reader.SafeGetInt("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public bool ApproveOrders(List<int> OrderIDs, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE `order` SET " +
                    "IsApproved = 1 " +
                    "WHERE OrderID=" + OrderIDs[0];

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    //command.Parameters.Add("@OrderIDs", SqlDbType.Int).Value = OrderIDs[0];

                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public bool ChangeOrderStatus(List<int> OrderIDs, int StatusID, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE `order` SET " +
                    " OrderStatusID = " + StatusID +
                    " WHERE OrderID=" + OrderIDs[0];

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", SqlDbType.NVarChar).Value = OrderIDs;
                    command.Parameters.Add("@OrderStatusID", SqlDbType.Int).Value = StatusID;


                    int RetVal = command.ExecuteNonQuery();

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<HRW_EmpEntityParamValues> GetCustomerOrders(int CustId, string FilterBy, string SearchCriteria, string OrderBy, out Exception exError)
        {
            List<HRW_EmpEntityParamValues> returnValue = new List<HRW_EmpEntityParamValues>();
            exError = null;

            try
            {
                string strFilterBy = "", strOrderBy = "";
                switch (FilterBy.ToLower())
                {
                    case "company":
                        strFilterBy = " WHERE company = '" + SearchCriteria + "' ";
                        break;
                    case "ordertype":
                        strFilterBy = " WHERE OrderTypeDesc = '" + SearchCriteria + "' ";
                        break;
                    case "orderstatus":
                        strFilterBy = " WHERE OrderStatusDesc = '" + SearchCriteria + "' ";
                        break;
                    case "completedate":
                        strFilterBy = " WHERE completedate = '" + SearchCriteria + "' ";
                        break;
                    case "deliverydate":
                        strFilterBy = " WHERE deliverydate = '" + SearchCriteria + "' ";
                        break;
                    case "departuredate":
                        strFilterBy = " WHERE departuredate = '" + SearchCriteria + "' ";
                        break;
                    case "arrivaldate":
                        strFilterBy = " WHERE arrivaldate = '" + SearchCriteria + "' ";
                        break;
                    case "isapproved":
                        strFilterBy = " WHERE isapproved = " + SearchCriteria;
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrWhiteSpace(OrderBy))
                {
                    strOrderBy = " Order By " + OrderBy;
                }

                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderID,CustomerID,IsNew,Fault,Evidence, Company, Reference, `order`.OrderTypeID,OrderTypeDesc,`order`.OrderStatusID,OrderStatusDesc,OrderDate,NumbOfBlinds,ConsignNoteNum,CompleteDate,DeliveryDate,DepartureDate,ArrivalDate,`order`.BlindTypeID,BlindTypeDesc,Transport,OrderM2,Notes,IsApproved FROM `order` " +
                    "LEFT JOIN ordertype ON ordertype.OrderTypeID = `order`.OrderTypeID " +
                    "LEFT JOIN orderstatus ON orderstatus.orderstatusID = `order`.orderstatusID " +
                    "LEFT JOIN blindtype ON blindtype.BlindTypeID = `order`.BlindTypeID " +
                    " " + strFilterBy + " " + strOrderBy, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HRW_EmpEntityParamValues()
                            {

                                OrderID = reader.SafeGetInt("OrderID"),
                                Fault = reader.SafeGetString("Fault"),
                                IsNew = reader.SafeGetBoolean("IsNew"),
                                Evidence = reader.SafeGetBoolean("Evidence"),
                                Company = reader.SafeGetString("Company"),
                                Reference = reader.SafeGetString("Reference"),
                                OrderTypeID = reader.SafeGetInt("OrderTypeID"),
                                OrderStatusID = reader.SafeGetInt("OrderStatusID"),
                                OrderDate = reader.SafeGetDateTime("OrderDate"),
                                NumbOfBlinds = reader.SafeGetInt("NumbOfBlinds"),
                                ConsignNoteNum = reader.SafeGetString("ConsignNoteNum"),
                                CompleteDate = reader.SafeGetDateTime("CompleteDate"),
                                DeliveryDate = reader.SafeGetDateTime("DeliveryDate"),
                                DepartureDate = reader.SafeGetDateTime("DepartureDate"),
                                ArrivalDate = reader.SafeGetDateTime("ArrivalDate"),
                                BlindTypeID = reader.SafeGetInt("BlindTypeID"),
                                Transport = reader.SafeGetString("Transport"),
                                OrderM2 = reader.SafeGetDouble("OrderM2"),
                                Notes = reader.SafeGetString("Notes"),
                                IsApproved = reader.SafeGetBoolean("IsApproved"),
                                BlindTypeName = reader.SafeGetString("BlindTypeDesc"),
                                OrderStatusName = reader.SafeGetString("OrderStatusDesc"),
                                OrderTypeName = reader.SafeGetString("OrderTypeDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

    }
    public class EmpOrgDetails_Mapper : IDataMapper<ORG_EmpEntityLink>
    {
        public override ORG_EmpEntityLink Create(ORG_EmpEntityLink instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO orderdetail (OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,SlatStyleID,CordStyleID,ReturnRequired,MountType,SquareMeter,ControlID,ControlStyle,OpeningStyle,PelmetStyle,ColorID,MaterialID,Roll,ReadyMadeSize) " +
                           "VALUES (@OrderID, @Width,@Height, @SplPelmetWidth, @WidthMadeBy, @HeightMadeBy, @QualityCheckedBy,@SlatStyleID,@CordStyleID,@ReturnRequired,@MountType,@SquareMeter,@ControlID,@ControlStyle,@OpeningStyle,@PelmetStyle,@ColorID,@MaterialID,@Roll,@ReadyMadeSize); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderID", SqlDbType.Int).Value = instance.OrderID;
                    command.Parameters.Add("@Width", SqlDbType.Decimal).Value = instance.Width;
                    command.Parameters.Add("@Height", SqlDbType.Decimal).Value = instance.Height;
                    command.Parameters.Add("@SplPelmetWidth", SqlDbType.Decimal).Value = instance.SplPelmetWidth;
                    command.Parameters.Add("@WidthMadeBy", SqlDbType.NVarChar).Value = instance.WidthMadeBy;
                    command.Parameters.Add("@HeightMadeBy", SqlDbType.NVarChar).Value = instance.HeightMadeBy;
                    command.Parameters.Add("@QualityCheckedBy", SqlDbType.NVarChar).Value = instance.QualityCheckedBy;
                    command.Parameters.Add("@SlatStyleID", SqlDbType.Int).Value = instance.SlatStyleID;
                    command.Parameters.Add("@CordStyleID", SqlDbType.Int).Value = instance.CordStyleID;
                    command.Parameters.Add("@ReturnRequired", SqlDbType.Bit).Value = instance.ReturnRequired;
                    command.Parameters.Add("@MountType", SqlDbType.NVarChar).Value = instance.MountType;
                    command.Parameters.Add("@SquareMeter", SqlDbType.Decimal).Value = instance.SquareMeter;
                    command.Parameters.Add("@ControlID", SqlDbType.Int).Value = instance.ControlID;
                    command.Parameters.Add("@ControlStyle", SqlDbType.NVarChar).Value = instance.ControlStyle;
                    command.Parameters.Add("@OpeningStyle", SqlDbType.NVarChar).Value = instance.OpeningStyle;
                    command.Parameters.Add("@PelmetStyle", SqlDbType.NVarChar).Value = instance.PelmetStyle;
                    command.Parameters.Add("@ColorID", SqlDbType.Int).Value = instance.ColorID;
                    command.Parameters.Add("@MaterialID", SqlDbType.Int).Value = instance.MaterialID;
                    command.Parameters.Add("@Roll", SqlDbType.NVarChar).Value = instance.Roll;
                    command.Parameters.Add("@ReadyMadeSize", SqlDbType.Decimal).Value = instance.ReadyMadeSize;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM orderdetail WHERE OrderDetailID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override ORG_EmpEntityLink Select(int ID, out Exception exError)
        {
            ORG_EmpEntityLink returnValue = new ORG_EmpEntityLink();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,OrderDetail.SlatStyleID,SlatStyledesc,OrderDetail.CordStyleID,CordStyleDesc,ReturnRequired,MountType,SquareMeter,OrderDetail.ControlID,ControlDesc,ControlStyle,OpeningStyle,PelmetStyle,OrderDetail.ColorID,ColorsDesc,OrderDetail.MaterialID,MaterialDesc,Roll,ReadyMadeSize FROM orderdetail " +
                                        "LEFT JOIN slatstyle ON slatstyle.SlatStyleID = OrderDetail.SlatStyleID " +
                                        "LEFT JOIN cordstyle ON cordstyle.CordStyleID = OrderDetail.CordStyleID " +
                                        "LEFT JOIN control ON control.ControlID = OrderDetail.ControlID " +
                                        "LEFT JOIN colors ON colors.ColorsID = OrderDetail.ColorID " +
                                        "LEFT JOIN material ON material.MaterialID = OrderDetail.MaterialID " +
                                        "Where OrderDetailID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new ORG_EmpEntityLink()
                            {
                                OrderID = reader.SafeGetInt("OrderID"),
                                Width = reader.SafeGetDouble("Width"),
                                Height = reader.SafeGetDouble("Height"),
                                SplPelmetWidth = reader.SafeGetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.SafeGetInt("SlatStyleID"),
                                CordStyleID = reader.SafeGetInt("CordStyleID"),
                                ReturnRequired = reader.SafeGetBoolean("ReturnRequired"),
                                MountType = reader.SafeGetString("MountType"),
                                SquareMeter = reader.SafeGetDouble("SquareMeter"),
                                ControlID = reader.SafeGetInt("ControlID"),
                                ControlStyle = reader.SafeGetString("ControlStyle"),
                                OpeningStyle = reader.SafeGetString("OpeningStyle"),
                                PelmetStyle = reader.SafeGetString("PelmetStyle"),
                                ColorID = reader.SafeGetInt("ColorID"),
                                MaterialID = reader.SafeGetInt("MaterialID"),
                                Roll = reader.SafeGetString("Roll"),
                                ReadyMadeSize = reader.SafeGetDouble("ReadyMadeSize"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                ControlName = reader.SafeGetString("ControlDesc"),
                                CordStyleName = reader.SafeGetString("CordStyleDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SlatStyleName = reader.SafeGetString("SlatStyleDesc")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<ORG_EmpEntityLink> SelectAll(out Exception exError)
        {
            List<ORG_EmpEntityLink> returnValue = new List<ORG_EmpEntityLink>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,OrderDetail.SlatStyleID,SlatStyledesc,OrderDetail.CordStyleID,CordStyleDesc,ReturnRequired,MountType,SquareMeter,OrderDetail.ControlID,ControlDesc,ControlStyle,OpeningStyle,PelmetStyle,OrderDetail.ColorID,ColorsDesc,OrderDetail.MaterialID,MaterialDesc,Roll,ReadyMadeSize FROM orderdetail " +
                                        "LEFT JOIN slatstyle ON slatstyle.SlatStyleID = OrderDetail.SlatStyleID " +
                                        "LEFT JOIN cordstyle ON cordstyle.CordStyleID = OrderDetail.CordStyleID " +
                                        "LEFT JOIN control ON control.ControlID = OrderDetail.ControlID " +
                                        "LEFT JOIN colors ON colors.ColorsID = OrderDetail.ColorID " +
                                        "LEFT JOIN material ON material.MaterialID = OrderDetail.MaterialID ", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new ORG_EmpEntityLink()
                            {
                                OrderID = reader.SafeGetInt("OrderID"),
                                Width = reader.SafeGetDouble("Width"),
                                Height = reader.SafeGetDouble("Height"),
                                SplPelmetWidth = reader.SafeGetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.SafeGetInt("SlatStyleID"),
                                CordStyleID = reader.SafeGetInt("CordStyleID"),
                                ReturnRequired = reader.SafeGetBoolean("ReturnRequired"),
                                MountType = reader.SafeGetString("MountType"),
                                SquareMeter = reader.SafeGetDouble("SquareMeter"),
                                ControlID = reader.SafeGetInt("ControlID"),
                                ControlStyle = reader.SafeGetString("ControlStyle"),
                                OpeningStyle = reader.SafeGetString("OpeningStyle"),
                                PelmetStyle = reader.SafeGetString("PelmetStyle"),
                                ColorID = reader.SafeGetInt("ColorID"),
                                MaterialID = reader.SafeGetInt("MaterialID"),
                                Roll = reader.SafeGetString("Roll"),
                                ReadyMadeSize = reader.SafeGetDouble("ReadyMadeSize"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                ControlName = reader.SafeGetString("ControlDesc"),
                                CordStyleName = reader.SafeGetString("CordStyleDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SlatStyleName = reader.SafeGetString("SlatStyleDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<ORG_EmpEntityLink> SelectedID(List<int> IDs, out Exception exError)
        {
            List<ORG_EmpEntityLink> returnValue = new List<ORG_EmpEntityLink>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,OrderDetail.SlatStyleID,SlatStyledesc,OrderDetail.CordStyleID,CordStyleDesc,ReturnRequired,MountType,SquareMeter,OrderDetail.ControlID,ControlDesc,ControlStyle,OpeningStyle,PelmetStyle,OrderDetail.ColorID,ColorsDesc,OrderDetail.MaterialID,MaterialDesc,Roll,ReadyMadeSize FROM orderdetail " +
                                        "LEFT JOIN slatstyle ON slatstyle.SlatStyleID = OrderDetail.SlatStyleID " +
                                        "LEFT JOIN cordstyle ON cordstyle.CordStyleID = OrderDetail.CordStyleID " +
                                        "LEFT JOIN control ON control.ControlID = OrderDetail.ControlID " +
                                        "LEFT JOIN colors ON colors.ColorsID = OrderDetail.ColorID " +
                                        "LEFT JOIN material ON material.MaterialID = OrderDetail.MaterialID " +
                                        "WHERE OrderDetailID IN (@OrderDetailID)", (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderDetailIDs", SqlDbType.NVarChar).Value = string.Join(",", IDs.ToArray());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new ORG_EmpEntityLink()
                            {
                                OrderID = reader.SafeGetInt("OrderID"),
                                Width = reader.SafeGetDouble("Width"),
                                Height = reader.SafeGetDouble("Height"),
                                SplPelmetWidth = reader.SafeGetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.SafeGetInt("SlatStyleID"),
                                CordStyleID = reader.SafeGetInt("CordStyleID"),
                                ReturnRequired = reader.SafeGetBoolean("ReturnRequired"),
                                MountType = reader.SafeGetString("MountType"),
                                SquareMeter = reader.SafeGetDouble("SquareMeter"),
                                ControlID = reader.SafeGetInt("ControlID"),
                                ControlStyle = reader.SafeGetString("ControlStyle"),
                                OpeningStyle = reader.SafeGetString("OpeningStyle"),
                                PelmetStyle = reader.SafeGetString("PelmetStyle"),
                                ColorID = reader.SafeGetInt("ColorID"),
                                MaterialID = reader.SafeGetInt("MaterialID"),
                                Roll = reader.SafeGetString("Roll"),
                                ReadyMadeSize = reader.SafeGetDouble("ReadyMadeSize"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                ControlName = reader.SafeGetString("ControlDesc"),
                                CordStyleName = reader.SafeGetString("CordStyleDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SlatStyleName = reader.SafeGetString("SlatStyleDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<ORG_EmpEntityLink> SelectedOrderID(List<int> IDs, out Exception exError)
        {
            List<ORG_EmpEntityLink> returnValue = new List<ORG_EmpEntityLink>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT OrderDetailID, OrderID, Width,Height, SplPelmetWidth, WidthMadeBy, HeightMadeBy, QualityCheckedBy,orderdetail.SlatStyleID,SlatStyledesc,orderdetail.CordStyleID,CordStyleDesc,ReturnRequired,MountType,SquareMeter,orderdetail.ControlID,ControlDesc,ControlStyle,OpeningStyle,PelmetStyle,orderdetail.ColorID,ColorsDesc,orderdetail.MaterialID,MaterialDesc,Roll,ReadyMadeSize FROM orderdetail " +
                                         "LEFT JOIN slatstyle ON slatstyle.SlatStyleID = orderdetail.SlatStyleID " +
                                         "LEFT JOIN cordstyle ON cordstyle.CordStyleID = orderdetail.CordStyleID " +
                                         "LEFT JOIN control ON control.ControlID = orderdetail.ControlID " +
                                         "LEFT JOIN colors ON colors.ColorsID = orderdetail.ColorID " +
                                         "LEFT JOIN material ON material.MaterialID = orderdetail.MaterialID " +
                                         "WHERE OrderID IN (@OrderIDs)", (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderIDs", SqlDbType.NVarChar).Value = string.Join(",", IDs.ToArray());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new ORG_EmpEntityLink()
                            {
                                OrderID = reader.SafeGetInt("OrderID"),
                                Width = reader.SafeGetDouble("Width"),
                                Height = reader.SafeGetDouble("Height"),
                                SplPelmetWidth = reader.SafeGetDouble("SplPelmetWidth"),
                                WidthMadeBy = reader.SafeGetString("WidthMadeBy"),
                                HeightMadeBy = reader.SafeGetString("HeightMadeBy"),
                                QualityCheckedBy = reader.SafeGetString("QualityCheckedBy"),
                                SlatStyleID = reader.SafeGetInt("SlatStyleID"),
                                CordStyleID = reader.SafeGetInt("CordStyleID"),
                                ReturnRequired = reader.SafeGetBoolean("ReturnRequired"),
                                MountType = reader.SafeGetString("MountType"),
                                SquareMeter = reader.SafeGetDouble("SquareMeter"),
                                ControlID = reader.SafeGetInt("ControlID"),
                                ControlStyle = reader.SafeGetString("ControlStyle"),
                                OpeningStyle = reader.SafeGetString("OpeningStyle"),
                                PelmetStyle = reader.SafeGetString("PelmetStyle"),
                                ColorID = reader.SafeGetInt("ColorID"),
                                MaterialID = reader.SafeGetInt("MaterialID"),
                                Roll = reader.SafeGetString("Roll"),
                                ReadyMadeSize = reader.SafeGetDouble("ReadyMadeSize"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                ControlName = reader.SafeGetString("ControlDesc"),
                                CordStyleName = reader.SafeGetString("CordStyleDesc"),
                                MaterialName = reader.SafeGetString("MaterialDesc"),
                                SlatStyleName = reader.SafeGetString("SlatStyleDesc")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override ORG_EmpEntityLink Update(ORG_EmpEntityLink instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE orderdetail SET " +
                    "OrderID = @OrderID," +
                    "Width = @Width," +
                    "Height = @Height," +
                    "SplPelmetWidth = @SplPelmetWidth," +
                    "WidthMadeBy = @WidthMadeBy," +
                    "HeightMadeBy = @HeightMadeBy," +
                    "QualityCheckedBy = @QualityCheckedBy," +
                    "SlatStyleID = @SlatStyleID," +
                    "CordStyleID = @CordStyleID," +
                    "ReturnRequired = @ReturnRequired," +
                    "MountType = @MountType," +
                    "SquareMeter = @SquareMeter," +
                    "ControlID = @ControlID," +
                    "ControlStyle = @ControlStyle," +
                    "OpeningStyle = @OpeningStyle," +
                    "PelmetStyle = @PelmetStyle," +
                    "ColorID = @ColorID," +
                    "MaterialID = @MaterialID," +
                    "Roll = @Roll," +
                    "ReadyMadeSize = @ReadyMadeSize " +
                    "WHERE OrderDetailID = @OrderDetailID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@OrderID", SqlDbType.Int).Value = instance.OrderID;
                    command.Parameters.Add("@Width", SqlDbType.Decimal).Value = instance.Width;
                    command.Parameters.Add("@Height", SqlDbType.Decimal).Value = instance.Height;
                    command.Parameters.Add("@SplPelmetWidth", SqlDbType.Decimal).Value = instance.SplPelmetWidth;
                    command.Parameters.Add("@WidthMadeBy", SqlDbType.NVarChar).Value = instance.WidthMadeBy;
                    command.Parameters.Add("@HeightMadeBy", SqlDbType.NVarChar).Value = instance.HeightMadeBy;
                    command.Parameters.Add("@QualityCheckedBy", SqlDbType.NVarChar).Value = instance.QualityCheckedBy;
                    command.Parameters.Add("@SlatStyleID", SqlDbType.Int).Value = instance.SlatStyleID;
                    command.Parameters.Add("@CordStyleID", SqlDbType.Int).Value = instance.CordStyleID;
                    command.Parameters.Add("@ReturnRequired", SqlDbType.Bit).Value = instance.ReturnRequired;
                    command.Parameters.Add("@MountType", SqlDbType.NVarChar).Value = instance.MountType;
                    command.Parameters.Add("@SquareMeter", SqlDbType.Decimal).Value = instance.SquareMeter;
                    command.Parameters.Add("@ControlID", SqlDbType.Int).Value = instance.ControlID;
                    command.Parameters.Add("@ControlStyle", SqlDbType.NVarChar).Value = instance.ControlStyle;
                    command.Parameters.Add("@OpeningStyle", SqlDbType.NVarChar).Value = instance.OpeningStyle;
                    command.Parameters.Add("@PelmetStyle", SqlDbType.NVarChar).Value = instance.PelmetStyle;
                    command.Parameters.Add("@ColorID", SqlDbType.Int).Value = instance.ColorID;
                    command.Parameters.Add("@MaterialID", SqlDbType.Int).Value = instance.MaterialID;
                    command.Parameters.Add("@Roll", SqlDbType.NVarChar).Value = instance.Roll;
                    command.Parameters.Add("@ReadyMadeSize", SqlDbType.Decimal).Value = instance.ReadyMadeSize;
                    command.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = instance.OrderDetailID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.OrderDetailID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }

    public class Payroll_Mapper : IDataMapper<PRL_PayrollSheet06>
    {
        public override PRL_PayrollSheet06 Create(PRL_PayrollSheet06 instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO fabric (UtilityOrderID, FabricType, ColorID, SizeID, Boxes) " +
                           "VALUES (@UtilityOrderID, @FabricType, @ColorID, @SizeID, @Boxes); " +
                            "SELECT LAST_INSERT_ID();";


                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@UtilityOrderID", SqlDbType.Int).Value = instance.UtilityOrderID;
                    command.Parameters.Add("@FabricType", SqlDbType.NVarChar).Value = instance.FabricType;
                    command.Parameters.Add("@ColorID", SqlDbType.Int).Value = instance.ColorID;
                    command.Parameters.Add("@SizeID", SqlDbType.Int).Value = instance.SizeID;
                    command.Parameters.Add("@Boxes", SqlDbType.Int).Value = instance.Boxes;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM fabric WHERE FabricID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override PRL_PayrollSheet06 Select(int ID, out Exception exError)
        {
            PRL_PayrollSheet06 returnValue = new PRL_PayrollSheet06();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT UtilityOrderID, FabricID, FabricType, fabric.ColorID, ColorsDesc, fabric.SizeID, SizeDesc, Boxes FROM fabric " +
                                                                "LEFT JOIN size ON size.SizeID = fabric.SizeID " +
                                                                "LEFT JOIN colors ON colors.ColorsID = fabric.ColorID " +
                                                                "Where FabricID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new PRL_PayrollSheet06()
                            {
                                FabricID = reader.SafeGetInt("FabricID"),
                                FabricType = reader.SafeGetString("FabricType"),
                                ColorID = reader.SafeGetInt("ColorID"),
                                Boxes = reader.SafeGetInt("Boxes"),
                                UtilityOrderID = reader.SafeGetInt("UtilityOrderID"),
                                SizeID = reader.SafeGetInt("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                SizeValue = reader.SafeGetString("SizeDesc"),
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<PRL_PayrollSheet06> SelectAll(out Exception exError)
        {
            List<PRL_PayrollSheet06> returnValue = new List<PRL_PayrollSheet06>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT UtilityOrderID, FabricID, FabricType, fabric.ColorID, ColorsDesc, fabric.SizeID, SizeDesc, Boxes FROM fabric " +
                                                "LEFT JOIN size ON size.SizeID = fabric.SizeID " +
                                                "LEFT JOIN colors ON colors.ColorsID = fabric.ColorID ", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new PRL_PayrollSheet06()
                            {
                                FabricID = reader.SafeGetInt("FabricID"),
                                FabricType = reader.SafeGetString("FabricType"),
                                ColorID = reader.SafeGetInt("ColorID"),
                                UtilityOrderID = reader.SafeGetInt("UtilityOrderID"),
                                Boxes = reader.SafeGetInt("Boxes"),
                                SizeID = reader.SafeGetInt("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                SizeValue = reader.SafeGetString("SizeDesc"),
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<PRL_PayrollSheet06> SelectAll(int ID, out Exception exError)
        {
            List<PRL_PayrollSheet06> returnValue = new List<PRL_PayrollSheet06>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT UtilityOrderID, FabricID, FabricType, fabric.ColorID, ColorsDesc, fabric.SizeID, SizeDesc, Boxes FROM fabric " +
                                 "LEFT JOIN size ON size.SizeID = fabric.SizeID " +
                                 "LEFT JOIN colors ON colors.ColorsID = fabric.ColorID " +
                                 "WHERE UtilityOrderID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new PRL_PayrollSheet06()
                            {
                                FabricID = reader.SafeGetInt("FabricID"),
                                FabricType = reader.SafeGetString("FabricType"),
                                ColorID = reader.SafeGetInt("ColorID"),
                                UtilityOrderID = reader.SafeGetInt("UtilityOrderID"),
                                Boxes = reader.SafeGetInt("Boxes"),
                                SizeID = reader.SafeGetInt("SizeID"),
                                ColorName = reader.SafeGetString("ColorsDesc"),
                                SizeValue = reader.SafeGetString("SizeDesc"),
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override PRL_PayrollSheet06 Update(PRL_PayrollSheet06 instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE fabric SET " +
                    "FabricType = @FabricType, " +
                    "ColorID = @ColorID," +
                    "Boxes = @Boxes," +
                    "SizeID = @SizeID " +
                    "WHERE FabricID = @FabricID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@FabricType", SqlDbType.NVarChar).Value = instance.FabricType;
                    command.Parameters.Add("@ColorID", SqlDbType.Int).Value = instance.ColorID;
                    command.Parameters.Add("@SizeID", SqlDbType.Int).Value = instance.SizeID;
                    command.Parameters.Add("@FabricID", SqlDbType.Int).Value = instance.FabricID;
                    command.Parameters.Add("@Boxes", SqlDbType.Int).Value = instance.Boxes;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.FabricID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class EntityMaster_Mapper : IDataMapper<ORG_EntityMaster>
    {
        public override ORG_EntityMaster Create(ORG_EntityMaster instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO rollerblindtype (Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ) " +
                           "VALUES (@Description, @Profile, @RollerColor, @DLXCODE, @PCSCTN, @MOQ); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.DateTime).Value = instance.MOQ;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM rollerblindtype WHERE RollerBlindsID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override ORG_EntityMaster Select(int ID, out Exception exError)
        {
            ORG_EntityMaster returnValue = new ORG_EntityMaster();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype Where RollerBlindTypeID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new ORG_EntityMaster()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<ORG_EntityMaster> SelectAll(out Exception exError)
        {
            List<ORG_EntityMaster> returnValue = new List<ORG_EntityMaster>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new ORG_EntityMaster()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override ORG_EntityMaster Update(ORG_EntityMaster instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE rollerblindtype SET " +
                    "Description = @Description, " +
                    "Profile = @Profile," +
                    "RollerColor = @RollerColor," +
                    "DLXCODE = @DLXCODE," +
                    "PCSCTN = @PCSCTN," +
                    "MOQ = @MOQ " +
                    "WHERE RollerBlindTypeID = @RollerBlindTypeID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.NVarChar).Value = instance.MOQ;
                    command.Parameters.Add("@RollerBlindTypeID", SqlDbType.Int).Value = instance.RollerBlindTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RollerBlindTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class TravelRequest_Mapper : IDataMapper<ORG_TravelRequest>
    {
        public override ORG_TravelRequest Create(ORG_TravelRequest instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO rollerblindtype (Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ) " +
                           "VALUES (@Description, @Profile, @RollerColor, @DLXCODE, @PCSCTN, @MOQ); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.DateTime).Value = instance.MOQ;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM rollerblindtype WHERE RollerBlindsID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override ORG_TravelRequest Select(int ID, out Exception exError)
        {
            ORG_TravelRequest returnValue = new ORG_TravelRequest();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype Where RollerBlindTypeID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new ORG_TravelRequest()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<ORG_TravelRequest> SelectAll(out Exception exError)
        {
            List<ORG_TravelRequest> returnValue = new List<ORG_TravelRequest>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new ORG_TravelRequest()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override ORG_TravelRequest Update(ORG_TravelRequest instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE rollerblindtype SET " +
                    "Description = @Description, " +
                    "Profile = @Profile," +
                    "RollerColor = @RollerColor," +
                    "DLXCODE = @DLXCODE," +
                    "PCSCTN = @PCSCTN," +
                    "MOQ = @MOQ " +
                    "WHERE RollerBlindTypeID = @RollerBlindTypeID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.NVarChar).Value = instance.MOQ;
                    command.Parameters.Add("@RollerBlindTypeID", SqlDbType.Int).Value = instance.RollerBlindTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RollerBlindTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class Quote_Mapper : IDataMapper<ORG_Quote>
    {
        public override ORG_Quote Create(ORG_Quote instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO rollerblindtype (Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ) " +
                           "VALUES (@Description, @Profile, @RollerColor, @DLXCODE, @PCSCTN, @MOQ); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.DateTime).Value = instance.MOQ;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM rollerblindtype WHERE RollerBlindsID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override ORG_Quote Select(int ID, out Exception exError)
        {
            ORG_Quote returnValue = new ORG_Quote();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype Where RollerBlindTypeID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new ORG_Quote()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<ORG_Quote> SelectAll(out Exception exError)
        {
            List<ORG_Quote> returnValue = new List<ORG_Quote>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new ORG_Quote()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override ORG_Quote Update(ORG_Quote instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE rollerblindtype SET " +
                    "Description = @Description, " +
                    "Profile = @Profile," +
                    "RollerColor = @RollerColor," +
                    "DLXCODE = @DLXCODE," +
                    "PCSCTN = @PCSCTN," +
                    "MOQ = @MOQ " +
                    "WHERE RollerBlindTypeID = @RollerBlindTypeID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.NVarChar).Value = instance.MOQ;
                    command.Parameters.Add("@RollerBlindTypeID", SqlDbType.Int).Value = instance.RollerBlindTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RollerBlindTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class LPO_Mapper : IDataMapper<ORG_LPO>
    {
        public override ORG_LPO Create(ORG_LPO instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO rollerblindtype (Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ) " +
                           "VALUES (@Description, @Profile, @RollerColor, @DLXCODE, @PCSCTN, @MOQ); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.DateTime).Value = instance.MOQ;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM rollerblindtype WHERE RollerBlindsID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override ORG_LPO Select(int ID, out Exception exError)
        {
            ORG_LPO returnValue = new ORG_LPO();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype Where RollerBlindTypeID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new ORG_LPO()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<ORG_LPO> SelectAll(out Exception exError)
        {
            List<ORG_LPO> returnValue = new List<ORG_LPO>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new ORG_LPO()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override ORG_LPO Update(ORG_LPO instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE rollerblindtype SET " +
                    "Description = @Description, " +
                    "Profile = @Profile," +
                    "RollerColor = @RollerColor," +
                    "DLXCODE = @DLXCODE," +
                    "PCSCTN = @PCSCTN," +
                    "MOQ = @MOQ " +
                    "WHERE RollerBlindTypeID = @RollerBlindTypeID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.NVarChar).Value = instance.MOQ;
                    command.Parameters.Add("@RollerBlindTypeID", SqlDbType.Int).Value = instance.RollerBlindTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RollerBlindTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class Notification_Mapper : IDataMapper<HRW_Notification>
    {
        public override HRW_Notification Create(HRW_Notification instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO rollerblindtype (Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ) " +
                           "VALUES (@Description, @Profile, @RollerColor, @DLXCODE, @PCSCTN, @MOQ); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.DateTime).Value = instance.MOQ;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM rollerblindtype WHERE RollerBlindsID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override HRW_Notification Select(int ID, out Exception exError)
        {
            HRW_Notification returnValue = new HRW_Notification();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype Where RollerBlindTypeID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new HRW_Notification()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<HRW_Notification> SelectAll(out Exception exError)
        {
            List<HRW_Notification> returnValue = new List<HRW_Notification>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HRW_Notification()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override HRW_Notification Update(HRW_Notification instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE rollerblindtype SET " +
                    "Description = @Description, " +
                    "Profile = @Profile," +
                    "RollerColor = @RollerColor," +
                    "DLXCODE = @DLXCODE," +
                    "PCSCTN = @PCSCTN," +
                    "MOQ = @MOQ " +
                    "WHERE RollerBlindTypeID = @RollerBlindTypeID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.NVarChar).Value = instance.MOQ;
                    command.Parameters.Add("@RollerBlindTypeID", SqlDbType.Int).Value = instance.RollerBlindTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RollerBlindTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class TravelAgency_Mapper : IDataMapper<ORG_TravelAgency>
    {
        public override ORG_TravelAgency Create(ORG_TravelAgency instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO rollerblindtype (Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ) " +
                           "VALUES (@Description, @Profile, @RollerColor, @DLXCODE, @PCSCTN, @MOQ); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.DateTime).Value = instance.MOQ;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM rollerblindtype WHERE RollerBlindsID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override ORG_TravelAgency Select(int ID, out Exception exError)
        {
            ORG_TravelAgency returnValue = new ORG_TravelAgency();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype Where RollerBlindTypeID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new ORG_TravelAgency()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<ORG_TravelAgency> SelectAll(out Exception exError)
        {
            List<ORG_TravelAgency> returnValue = new List<ORG_TravelAgency>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RollerBlindTypeID, Description, Profile, RollerColor, DLXCODE, PCSCTN, MOQ FROM rollerblindtype", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new ORG_TravelAgency()
                            {
                                RollerBlindTypeID = reader.SafeGetInt("RollerBlindTypeID"),
                                Description = reader.SafeGetString("Description"),
                                Profile = reader.SafeGetString("Profile"),
                                RollerColor = reader.SafeGetString("RollerColor"),
                                DLXCODE = reader.SafeGetString("DLXCODE"),
                                PCSCTN = reader.SafeGetString("PCSCTN"),
                                MOQ = reader.SafeGetString("MOQ")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override ORG_TravelAgency Update(ORG_TravelAgency instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE rollerblindtype SET " +
                    "Description = @Description, " +
                    "Profile = @Profile," +
                    "RollerColor = @RollerColor," +
                    "DLXCODE = @DLXCODE," +
                    "PCSCTN = @PCSCTN," +
                    "MOQ = @MOQ " +
                    "WHERE RollerBlindTypeID = @RollerBlindTypeID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = instance.Description;
                    command.Parameters.Add("@Profile", SqlDbType.NVarChar).Value = instance.Profile;
                    command.Parameters.Add("@RollerColor", SqlDbType.NVarChar).Value = instance.RollerColor;
                    command.Parameters.Add("@DLXCODE", SqlDbType.NVarChar).Value = instance.DLXCODE;
                    command.Parameters.Add("@PCSCTN", SqlDbType.NVarChar).Value = instance.PCSCTN;
                    command.Parameters.Add("@MOQ", SqlDbType.NVarChar).Value = instance.MOQ;
                    command.Parameters.Add("@RollerBlindTypeID", SqlDbType.Int).Value = instance.RollerBlindTypeID;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RollerBlindTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class Destinations_Mapper : IDataMapper<Destinations>
    {
        public override Destinations Create(Destinations instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO Destinations (DestinationsDesc,For) " +
                           "VALUES (@DestinationsDesc,@For); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@DestinationsDesc", SqlDbType.NVarChar).Value = instance.DestinationsDesc;
                    command.Parameters.Add("@For", SqlDbType.NVarChar).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Destinations WHERE DestinationsID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override Destinations Select(int ID, out Exception exError)
        {
            Destinations returnValue = new Destinations();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT DestinationsID, DestinationsDesc, `For` FROM Destinations Where DestinationsID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new Destinations()
                            {
                                DestinationsID = reader.SafeGetInt("DestinationsID"),
                                DestinationsDesc = reader.SafeGetString("DestinationsDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<Destinations> SelectAll(out Exception exError)
        {
            List<Destinations> returnValue = new List<Destinations>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT DestinationsID, DestinationsDesc, `For` FROM Destinations", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Destinations()
                            {
                                DestinationsID = reader.SafeGetInt("DestinationsID"),
                                DestinationsDesc = reader.SafeGetString("DestinationsDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<Destinations> SelectFor(string For, out Exception exError)
        {
            List<Destinations> returnValue = new List<Destinations>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT DestinationsID, DestinationsDesc, `For` FROM Destinations WHERE `For` = '" + For + "'", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new Destinations()
                            {
                                DestinationsID = reader.SafeGetInt("DestinationsID"),
                                DestinationsDesc = reader.SafeGetString("DestinationsDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override Destinations Update(Destinations instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE Destinations SET " +
                    "DestinationsDesc = @DestinationsDesc," +
                    "`For` = @For " +
                    "WHERE DestinationsID = @DestinationsID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@DestinationsDesc", SqlDbType.NVarChar).Value = instance.DestinationsDesc;
                    command.Parameters.Add("@DestinationsID", SqlDbType.Int).Value = instance.DestinationsID;
                    command.Parameters.Add("@For", SqlDbType.NVarChar).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.DestinationsID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class HotelCategory_Mapper : IDataMapper<HotelCategory>
    {
        public override HotelCategory Create(HotelCategory instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO HotelCategory (HotelCategoryDesc, `For`) " +
                           "VALUES (@HotelCategoryDesc, @For); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@HotelCategoryDesc", SqlDbType.NVarChar).Value = instance.HotelCategoryDesc;
                    command.Parameters.Add("@For", SqlDbType.NVarChar).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM HotelCategory WHERE HotelCategoryID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override HotelCategory Select(int ID, out Exception exError)
        {
            HotelCategory returnValue = new HotelCategory();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT HotelCategoryID, HotelCategoryDesc, `For` FROM HotelCategory Where HotelCategoryID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new HotelCategory()
                            {
                                HotelCategoryID = reader.SafeGetInt("HotelCategoryID"),
                                HotelCategoryDesc = reader.SafeGetString("HotelCategoryDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<HotelCategory> SelectAll(out Exception exError)
        {
            List<HotelCategory> returnValue = new List<HotelCategory>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT HotelCategoryID, HotelCategoryDesc, `For` FROM HotelCategory", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HotelCategory()
                            {
                                HotelCategoryID = reader.SafeGetInt("HotelCategoryID"),
                                HotelCategoryDesc = reader.SafeGetString("HotelCategoryDesc"),
                                For = reader.SafeGetString("FOr")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<HotelCategory> SelectFor(string For, out Exception exError)
        {
            List<HotelCategory> returnValue = new List<HotelCategory>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT HotelCategoryID, HotelCategoryDesc, `For` FROM HotelCategory WHERE `For` = '" + For + "'", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new HotelCategory()
                            {
                                HotelCategoryID = reader.SafeGetInt("HotelCategoryID"),
                                HotelCategoryDesc = reader.SafeGetString("HotelCategoryDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override HotelCategory Update(HotelCategory instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE HotelCategory SET " +
                    "HotelCategoryDesc = @HotelCategoryDesc," +
                    "`For` = @For " +
                    "WHERE HotelCategoryID = @HotelCategoryID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@HotelCategoryDesc", SqlDbType.NVarChar).Value = instance.HotelCategoryDesc;
                    command.Parameters.Add("@HotelCategoryID", SqlDbType.Int).Value = instance.HotelCategoryID;
                    command.Parameters.Add("@For", SqlDbType.NVarChar).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.HotelCategoryID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public class RoomType_Mapper : IDataMapper<RoomType>
    {
        public override RoomType Create(RoomType instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "INSERT INTO RoomType (RoomTypeDesc, `For`) " +
                           "VALUES (@RoomTypeDesc, @For); " +
                            "SELECT LAST_INSERT_ID();";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@RoomTypeDesc", SqlDbType.NVarChar).Value = instance.RoomTypeDesc;
                    command.Parameters.Add("@For", SqlDbType.NVarChar).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(RetVal, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }

        public override bool Delete(int ID, out Exception exError)
        {
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM RoomType WHERE RoomTypeID = " + ID, (SqlConnection)this.Connection))
                {
                    int rows = command.ExecuteNonQuery();
                    if (rows <= 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }

        public override RoomType Select(int ID, out Exception exError)
        {
            RoomType returnValue = new RoomType();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RoomTypeID, RoomTypeDesc, `For` FROM RoomType Where RoomTypeID = " + ID, (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            returnValue = new RoomType()
                            {
                                RoomTypeID = reader.SafeGetInt("RoomTypeID"),
                                RoomTypeDesc = reader.SafeGetString("RoomTypeDesc"),
                                For = reader.SafeGetString("For")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override List<RoomType> SelectAll(out Exception exError)
        {
            List<RoomType> returnValue = new List<RoomType>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RoomTypeID, RoomTypeDesc, `For` FROM RoomType", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new RoomType()
                            {
                                RoomTypeID = reader.SafeGetInt("RoomTypeID"),
                                RoomTypeDesc = reader.SafeGetString("RoomTypeDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public List<RoomType> SelectFor(string For, out Exception exError)
        {
            List<RoomType> returnValue = new List<RoomType>();
            exError = null;

            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT RoomTypeID, RoomTypeDesc, `For` FROM RoomType WHERE `For` = '" + For + "'", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            returnValue.Add(new RoomType()
                            {
                                RoomTypeID = reader.SafeGetInt("RoomTypeID"),
                                RoomTypeDesc = reader.SafeGetString("RoomTypeDesc"),
                                For = reader.SafeGetString("For")
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
            }
            finally
            {
                Connection.Close();
            }

            return returnValue;
        }

        public override RoomType Update(RoomType instance, out Exception exError)
        {
            exError = null;
            try
            {
                if (this.Connection == null)
                {
                    throw new Exception("Unable to Connect to Database");
                }
                else if (this.Connection != null && this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                string query = "UPDATE RoomType SET " +
                    "RoomTypeDesc = @RoomTypeDesc," +
                    "`For` = @For " +
                    "WHERE RoomTypeID = @RoomTypeID";

                using (SqlCommand command = new SqlCommand(query, (SqlConnection)this.Connection))
                {
                    command.Parameters.Add("@RoomTypeDesc", SqlDbType.NVarChar).Value = instance.RoomTypeDesc;
                    command.Parameters.Add("@RoomTypeID", SqlDbType.Int).Value = instance.RoomTypeID;
                    command.Parameters.Add("@For", SqlDbType.NVarChar).Value = instance.For;

                    int RetVal = Convert.ToInt32(command.ExecuteScalar());

                    if (RetVal <= 0)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        Exception custException = new Exception();
                        return Select(instance.RoomTypeID, out custException);
                    }
                }
            }
            catch (Exception ex)
            {
                exError = ex;
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
    public static class ExtMethods
    {
        public static string SafeGetString(this SqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetString(reader.GetOrdinal(colIndex));
            return string.Empty;
        }

        public static int SafeGetInt(this SqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetInt32(reader.GetOrdinal(colIndex));
            return 0;
        }

        public static Double SafeGetDouble(this SqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetDouble(reader.GetOrdinal(colIndex));
            return 0;
        }

        public static DateTime SafeGetDateTime(this SqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetDateTime(reader.GetOrdinal(colIndex));
            return DateTime.MinValue;
        }

        public static Boolean SafeGetBoolean(this SqlDataReader reader, string colIndex)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colIndex)))
                return reader.GetBoolean(reader.GetOrdinal(colIndex));
            return false;
        }

    }
}