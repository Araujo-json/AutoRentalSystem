using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
//using GlobalServicesLayer;


namespace DALayer
{
    public class CreditCardDAO : ICreditCardDAO
    {
        public CreditCardDTO GetRecordByID(string key)
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Step A-Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string
                string strSQL = "SELECT * FROM CreditCard WHERE CardNumber = @CardNumber;";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Step 6-Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                objCmd.Parameters.Add("@CardNumber", SqlDbType.VarChar).Value = key;
                //Create DATAREADER POINTER & Execute Query via
                //COMMAND OBJECT ExecuteReader Method which returns a populated
                //DATAREADER OBJECT with the results of the query
                SqlDataReader objDR = objCmd.ExecuteReader();
                //Test to make sure there is data in the DataReader Object
                if (objDR.HasRows)
                {
                    //Create Data Transfer Object
                    CreditCardDTO objDTO = new CreditCardDTO();
                    //Call Read() Method to point and read the first record
                    objDR.Read();
                    //Extract data from a row s Object Populates itself.
                    //IMPORTANT! Note that data must be extracted in the ORDER
                    //in which the QUERY RETURNS THE DATA.
                    objDTO.CardNumber = objDR.GetString(0);
                    objDTO.CardOwnerName = objDR.GetString(1);
                    objDTO.MerchantName = objDR.GetString(2);
                    objDTO.ExpirationDate = objDR.GetDateTime(3);
                    objDTO.AddressLine1 = objDR.GetString(4);
                    objDTO.City = objDR.GetString(5);
                    objDTO.State = objDR.GetString(6);
                    objDTO.ZipCode = objDR.GetString(7);
                    objDTO.Country = objDR.GetString(8);
                    objDTO.CreditLimit = Convert.ToDecimal(objDR.GetInt32(9));
                    objDTO.ActivationStatus = objDR.GetBoolean(10);
                    //Return Data Transfer Object
                    return objDTO;
                }
                //Terminate ADO Objects
                objDR.Close();
                objDR = null;
                objCmd.Dispose();
                objCmd = null;
                //return null since no data found
                return null;
            }//End of try
             //Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO GetRecordByID(key) Method:{ 0 } " + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of GetRecordByID

        public bool Insert(CreditCardDTO objDTO)
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string
                string strSQL;
                strSQL = "INSERT INTO CreditCard (CardNumber,OwnerName,MerchantName,ExpDate,";
                strSQL += "HouseStreetAddress,City,State,Zipcode,Country,";
                strSQL += "CreditLimit,ActivationStatus)";
                strSQL += "VALUES(@CardNumber,@CardOwnerName,@MerchantName,@ExpDate,";
                strSQL += "@HouseStreetAddress,@City,@State,@ZipCode,@Country,";
                strSQL += "@CreditLimit,@ActivationStatus);";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                //IMPORTANT! Parameter TOKENS @XXXXX name must match same name Used in the INSERT QUERY
                //AND IN LISTED IN THE ORDER LISTED IN INSERT QUERY! NOTE WE ARE ASSIGNING ALL OBJECT'S DATA
                objCmd.Parameters.Add("@CardNumber", SqlDbType.VarChar).Value = objDTO.CardNumber;
                objCmd.Parameters.Add("@CardOwnerName", SqlDbType.VarChar).Value = objDTO.CardOwnerName;
                objCmd.Parameters.Add("@MerchantName", SqlDbType.VarChar).Value = objDTO.MerchantName;
                objCmd.Parameters.Add("@ExpDate", SqlDbType.Date).Value = objDTO.ExpirationDate;
                objCmd.Parameters.Add("@HouseStreetAddress", SqlDbType.VarChar).Value = objDTO.AddressLine1;
                objCmd.Parameters.Add("@City", SqlDbType.VarChar).Value = objDTO.City;
                objCmd.Parameters.Add("@State", SqlDbType.Char).Value = objDTO.State.ToCharArray();
                objCmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = objDTO.ZipCode;
                objCmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = objDTO.Country;
                objCmd.Parameters.Add("@CreditLimit", SqlDbType.Decimal).Value = objDTO.CreditLimit;
                objCmd.Parameters.Add("@ActivationStatus", SqlDbType.Bit).Value = objDTO.ActivationStatus;
                //Execute ACTION-Query, Test result and throw exception if failed
                int intRecordsAffected = objCmd.ExecuteNonQuery();

                //validate if INSERT QUERY was successful
                if (intRecordsAffected == 1)
                {
                    return true;
                }
                //Terminate ADO Objects
                objCmd.Dispose();
                objCmd = null;
                //Step10-return false
                return false;
            }//End of try
             //Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO Insert(CreditCardDTO objDTO) Method:{ 0 } " + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of Insert

        public bool InsertChildObjectOfAParent(int parentKey, CreditCardDTO objDTO)
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string
                string strSQL;
                //This is a multi-query where two INSERT statements are executed one after the other
                //The first inserts into the CreditCard table, the second adds a row to the
                //Person_CreditCard Table to complete the association betweeen the Person and the
                //Credit Card they own. Note spaces within the string for formatting the query
                strSQL = "INSERT INTO CreditCard (CardNumber,CardOwnerName,MerchantName,ExpDate,";
                strSQL += "HouseStreetAddress,City,State,ZipCode,Country,";
                strSQL += "CreditLimit,ActivationStatus)";
                strSQL += "VALUES(@CardNumber,@CardOwnerName,@MerchantName,@ExpDate,";
                strSQL += "@HouseStreetAddress,@City,@State,@ZipCode,@Country,";
                strSQL += "@CreditLimit,@ActivationStatus);";
                strSQL += " INSERT INTO Customer_CreditCard (CustomerID,CardNumber)";
                strSQL += "VALUES (@CustomerID,@CardNumber);";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                //IMPORTANT! Parameter TOKENS @XXXXX name must match same name Used in the INSERT QUERY
                //AND IN LISTED IN THE ORDER LISTED IN INSERT QUERY! NOTE WE ARE ASSIGNING ALL OBJECT'S DATA
                objCmd.Parameters.Add("@CardNumber", SqlDbType.VarChar).Value = objDTO.CardNumber;
                objCmd.Parameters.Add("@CardOwnerName", SqlDbType.VarChar).Value = objDTO.CardOwnerName;
                objCmd.Parameters.Add("@MerchantName", SqlDbType.VarChar).Value = objDTO.MerchantName;
                objCmd.Parameters.Add("@ExpDate", SqlDbType.VarChar).Value = objDTO.ExpirationDate;
                objCmd.Parameters.Add("@HouseStreetName", SqlDbType.VarChar).Value = objDTO.AddressLine1;
                objCmd.Parameters.Add("@City", SqlDbType.VarChar).Value = objDTO.City;
                objCmd.Parameters.Add("@State", SqlDbType.Char).Value = objDTO.State.ToCharArray();
                objCmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = objDTO.ZipCode;
                objCmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = objDTO.Country;
                objCmd.Parameters.Add("@CreditLimit", SqlDbType.Decimal).Value = objDTO.CreditLimit;
                objCmd.Parameters.Add("@ActivationStatus", SqlDbType.Bit).Value = objDTO.ActivationStatus;
                objCmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = parentKey;

                //Execute ACTION-Query, Test result and throw exception if failed
                int intRecordsAffected = objCmd.ExecuteNonQuery();

                //validate if INSERT QUERY was successful
                if (intRecordsAffected == 2)
                {
                    return true;
                }
                //Terminate ADO Objects
                objCmd.Dispose();
                objCmd = null;
                
                return false;
            }//End of try
             //Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO InsertChildObjectOfAParent (Key, CreditCardDTO objDTO) Method: { 0}" + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of Insert

        public bool Update(CreditCardDTO objDTO)
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string
                string strSQL;
                strSQL = "UPDATE CreditCard";
                strSQL += " SET OwnerName=@CardOwnerName,";
                strSQL += "MerchantName=@MerchantName,";
                strSQL += "ExpDate=@ExpDate,";
                strSQL += "HouseStreetAddress=@HouseStreetAddress,";
                strSQL += "City=@City,";
                strSQL += "State=@State,";
                strSQL += "ZipCode=@ZipCode,";
                strSQL += "Country=@Country,";
                strSQL += "CreditLimit=@CreditLimit,";
                strSQL += "ActivationStatus=@ActivationStatus";
                strSQL += " WHERE CardNumber=@CardNumber;";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                //IMPORTANT! Parameter TOKENS @XXXXX name must match same name Used in the UPDATE QUERY
                //AND IN LISTED IN THE ORDER LISTED IN INSERT QUERY! NOTE WE ARE ASSIGNING ALL OBJECT'S DATA
                objCmd.Parameters.Add("@CardOwnerName", SqlDbType.VarChar).Value = objDTO.CardOwnerName;
                objCmd.Parameters.Add("@MerchantName", SqlDbType.VarChar).Value = objDTO.MerchantName;
                objCmd.Parameters.Add("@ExpDate", SqlDbType.VarChar).Value = objDTO.ExpirationDate;
                objCmd.Parameters.Add("@HouseStreetAddress", SqlDbType.VarChar).Value = objDTO.AddressLine1;
                objCmd.Parameters.Add("@City", SqlDbType.VarChar).Value = objDTO.City;
                objCmd.Parameters.Add("@State", SqlDbType.Char).Value = objDTO.State.ToCharArray();
                objCmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = objDTO.ZipCode;
                objCmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = objDTO.Country;
                objCmd.Parameters.Add("@CreditLimit", SqlDbType.Decimal).Value = Convert.ToDecimal(objDTO.CreditLimit);
                objCmd.Parameters.Add("@ActivationStatus", SqlDbType.Bit).Value = objDTO.ActivationStatus;
                objCmd.Parameters.Add("@CardNumber", SqlDbType.VarChar).Value = objDTO.CardNumber;
                //Execute ACTION-Query, Test result and throw exception if failed
                int intRecordsAffected = objCmd.ExecuteNonQuery();

                //validate if INSERT QUERY was successful
                if (intRecordsAffected == 1)
                {
                    return true;
                }
                //Terminate ADO Objects
                objCmd.Dispose();
                objCmd = null;
                //return false
                return false;
            }//End of try
             //Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO Update(CreditCardDTO objDTO) Method:{ 0 } " + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of Insert

        public bool Delete(string objDTO)
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string
                string strSQL = "DELETE FROM CreditCard WHERE CardNumber = @CardNumber;";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                //IMPORTANT! Parameter TOKENS @XXXXX name must match same name Used in the UPDATE QUERY
                //AND IN LISTED IN THE ORDER LISTED IN INSERT QUERY! NOTE WE ARE ASSIGNING ALL OBJECT'S DATA
                objCmd.Parameters.Add("@CardNumber", SqlDbType.VarChar).Value = objDTO;
                //Execute ACTION-Query, Test result and throw exception if failed
                int intRecordsAffected = objCmd.ExecuteNonQuery();

                //validate if INSERT QUERY was successful
                if (intRecordsAffected == 1)
                {
                    return true;
                }
                //Terminate ADO Objects
                objCmd.Dispose();
                objCmd = null;
                //return false
                return false;
            }//End of try
             //Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO Delete(key) Method: { 0 } " + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of Insert


        public List<CreditCardDTO> GetAllRecords()
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string
                string strSQL = "SELECT * FROM CreditCard;";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Create DATAREADER POINTER & Execute Query via
                //COMMAND OBJECT ExecuteReader Method which returns a populated
                //DATAREADER OBJECT with the results of the query
                SqlDataReader objDR = objCmd.ExecuteReader();
                //Test to make sure there is data in the DataReader Object
                if (objDR.HasRows)
                {
                    //Test Create a Generic List Collection Object of Data Transfer Objects
                    List<CreditCardDTO> colRecordList = new List<CreditCardDTO>();
                    //Loop through the Collection to Add Data Transfer Object
                    while (objDR.Read())
                    {
                        //Create Data Transfer Object
                        CreditCardDTO objDTO = new CreditCardDTO();
                        //Populate Data Transfer Object with DataReader records
                        //IMPORTANT! Note that data must be extracted in the ORDER
                        //in which the QUERY RETURNS THE DATA.
                        objDTO.CardNumber = objDR.GetString(0);
                        objDTO.CardOwnerName = objDR.GetString(1);
                        objDTO.MerchantName = objDR.GetString(2);
                        objDTO.ExpirationDate = objDR.GetDateTime(3);
                        objDTO.AddressLine1 = objDR.GetString(4);
                        objDTO.City = objDR.GetString(5);
                        objDTO.State = objDR.GetString(6);
                        objDTO.ZipCode = objDR.GetString(7);
                        objDTO.Country = objDR.GetString(8);
                        objDTO.CreditLimit = objDR.GetDecimal(9);
                        objDTO.ActivationStatus = objDR.GetBoolean(10);
                        //Add Data Transfer Object to the collection
                        colRecordList.Add(objDTO);
                    }//End of loop
                     //Return the collection
                    return colRecordList;
                }
                else
                {
                    //Terminate ADO Objects
                    objDR.Close();
                    objDR = null;
                    objCmd.Dispose();
                    objCmd = null;
                    //return null since no records found
                    return null;
                }//End of if/else
            }//End of try
             //Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO GetAllRecords() Method:{ 0 } " + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of GetAllRecords

        public List<string> GetAllKeys()
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string
                string strSQL = "SELECT CardNumber FROM CreditCard;";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Create DATAREADER POINTER & Execute Query via
                //COMMAND OBJECT ExecuteReader Method which returns a populated
                //DATAREADER OBJECT with the results of the query
                SqlDataReader objDR = objCmd.ExecuteReader();
                //Step 8-Test to make sure there is data in the DataReader Object
                if (objDR.HasRows)
                {
                    //Test Create a Generic List Collection Object of Data Transfer Objects
                    List<string> colKeyList = new List<string>();
                    //Loop through the Collection & Add results from the first column of DataReader
                    while (objDR.Read())
                    {
                        //Add Data Transfer Object to the collection
                        colKeyList.Add(objDR.GetString(0));
                    }//End of loop
                     //Return the collection
                    return colKeyList;
                }
                else
                {
                    //Terminate ADO Objects
                    objDR.Close();
                    objDR = null;
                    objCmd.Dispose();
                    objCmd = null;
                    //return null since no records found
                    return null;
                }//End of if/else
            }//End of try
             //Step B-Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO GetAllKeys() Method:{ 0 } " + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of GetAllKeys

        public List<CreditCardDTO> GetAllChildRecordsOwnedByParent(int ParentKey)
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string. Note spaces between SELECT, FROM, WHERE & AND clauses
                string strSQL;
                strSQL = "SELECT CreditCard.CardNumber,CreditCard.CardOwnerName,";
                strSQL += "CreditCard.MerchantName,CreditCard.ExpDate,";
                strSQL += "CreditCard.HouseStreetName, CreditCard.City,CreditCard.State,";
                strSQL += "CreditCard.ZipCode,CreditCard.Country,";
                strSQL += "CreditCard.CreditLimit,CreditCard.ActivationStatus)";
                strSQL += " FROM CreditCard, Customer_CreditCard";
                strSQL += " WHERE CreditCard.CardNumber = Customer_CreditCard.CardNumber";
                strSQL += " AND Customer_CreditCard.Customer_IDNumber = @Customer_IDNumber;";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                objCmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = ParentKey;
                //Create DATAREADER POINTER & Execute Query via
                //COMMAND OBJECT ExecuteReader Method which returns a populated
                //DATAREADER OBJECT with the results of the query
                SqlDataReader objDR = objCmd.ExecuteReader();
                //Test to make sure there is data in the DataReader Object
                if (objDR.HasRows)
                {
                    //Test Create a Generic List Collection Object of Data Transfer Objects
                    List<CreditCardDTO> colRecordList = new List<CreditCardDTO>();
                    //Loop through the Collection & Add Data Transfer Object (DTO)
                    while (objDR.Read())
                    {
                        //Create Data Transfer Object
                        CreditCardDTO objDTO = new CreditCardDTO();
                        //Populate Data Transfer Object with DataReader records
                        //IMPORTANT! Note that data must be extracted in the ORDER
                        //in which the QUERY RETURNS THE DATA.
                        objDTO.CardNumber = objDR.GetString(0);
                        objDTO.CardOwnerName = objDR.GetString(1);
                        objDTO.MerchantName = objDR.GetString(2);
                        objDTO.ExpirationDate = objDR.GetDateTime(3);
                        objDTO.AddressLine1 = objDR.GetString(4);
                        objDTO.City = objDR.GetString(5);
                        objDTO.State = objDR.GetString(6);
                        objDTO.ZipCode = objDR.GetString(7);
                        objDTO.Country = objDR.GetString(8);
                        objDTO.CreditLimit = objDR.GetDecimal(9);
                        objDTO.ActivationStatus = objDR.GetBoolean(10);
                        //Add Data Transfer Object to the collection
                        colRecordList.Add(objDTO);
                    }//End of loop
                     //Return the collection
                    return colRecordList;
                }
                else
                {
                    //Terminate ADO Objects
                    objDR.Close();
                    objDR = null;
                    objCmd.Dispose();
                    objCmd = null;
                    //return null since no records found
                    return null;
                }//End of if/else
            }//End of try
             //Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO GetAllChildKeysOwnedByParent() Method:{ 0 } " + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of GetAllChildRecordsOwnedByParent

        public List<string> GetAllChildKeysOwnedByParent(int ParentKey)
        {
            //Create Connection, assign Connection to string
            SqlConnection objConn = new SqlConnection(SQLServerDAOFactory.ConnectionString());
            //Start Error Trapping
            try
            {
                //Open connection
                objConn.Open();
                //Create SQL string. Note spaces between SELECT, FROM, WHERE & AND clauses
                string strSQL;
                strSQL = "SELECT CreditCard.CardNumber";
                strSQL += " FROM CreditCard, Customer_CreditCard";
                strSQL += " WHERE CreditCard.CardNumber = Customer_CreditCard.CardNumber";
                strSQL += " AND Customer_CreditCard.CustomerID = @CustomerID;";
                //Create Command object, pass query and connection object
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //SET CommandType Property to text since we have a query string & NOT a Stored-Procedure
                //For stored procedures syntax is objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandType = CommandType.Text;
                //Add Parameter to. NOTE WE ARE ASSIGNING METHOD PARAMETER
                objCmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = ParentKey;
                //Create DATAREADER POINTER & Execute Query via
                //COMMAND OBJECT ExecuteReader Method which returns a populated
                //DATAREADER OBJECT with the results of the query
                SqlDataReader objDR = objCmd.ExecuteReader();
                //Test to make sure there is data in the DataReader Object
                if (objDR.HasRows)
                {
                    //Test Create a Generic List Collection Object of Data Transfer Objects
                    List<string> colKeyList = new List<string>();
                    //Loop through the Collection & Add results from the first column of DataReader
                    while (objDR.Read())
                    {
                        //Add Data Transfer Object to the collection
                        colKeyList.Add(objDR.GetString(0));
                    }//End of loop
                     //Return the collection
                    return colKeyList;
                }
                else
                {
                    //Terminate ADO Objects
                    objDR.Close();
                    objDR = null;
                    objCmd.Dispose();
                    objCmd = null;
                    //return null since no records found
                    return null;
                }//End of if/else
            }//End of try
             //Trap for BO, App & General Exceptions
            catch (Exception objE)
            {
                //throw system exception since run time error has occurred.
                throw new Exception("Unexpected Error in CreditCardADO GetAllChildKeysOwnedByParent() Method:{ 0 } " + objE.Message);
            }
            finally
            {
                //Terminate connection
                objConn.Close();
                objConn.Dispose();
                objConn = null;
            }
        }//End of GetAllChildKeysOwnedByParent

        
        bool ICreditCardDAO.InsertChildObjectOfAParenet(string parentKey, CreditCardDTO objDTO)
        {
            throw new NotImplementedException();
        }
        List<CreditCardDTO> ICreditCardDAO.GetAllChildRecordOwnedByParent(string parentKey)
        {
            throw new NotImplementedException();
        }
        List<string> ICreditCardDAO.GetAllChildKeysOwnedByParent(string parentKey)
        {
            throw new NotImplementedException();
        }
    }
}
