using System;
using System.IO;
using DALayer;

namespace BOLayer
{
    public class CreditCard
    {
        private string m_CardNumber;
        private string m_CardOwnerName;
        private string m_MerchantName;
        private DateTime m_ExpDate;
        private string m_AddressLines1;
        private string m_AddressLines2;
        private string m_City;
        private string m_State;
        private string m_ZipCode;
        private string m_Country;
        private decimal m_CreditLimit;
        private bool m_ActivationStatus;

        public string CardNumber
        {
            get
            {
                return m_CardNumber;
            }
            set
            {
                m_CardNumber = value;
            }
        }
        public string CardOwnerName
        {
            get
            {
                return m_CardOwnerName;
            }
            set
            {
                m_CardOwnerName = value;
            }
        }
        public string MerchantName
        {
            get
            {
                return m_MerchantName;
            }
            set
            {
                m_MerchantName = value;
            }
        }
        public DateTime ExpDate
        {
            get
            {
                return m_ExpDate;
            }
            set 
            {
                m_ExpDate = value;
            }
        }
        public string AddressLines1
        {
            get
            {
                return m_AddressLines1;
            }
            set
            {
                m_AddressLines1 = value;
            }
        }
        public string AddressLines2
        {
            get
            {
                return m_AddressLines2;
            }
            set
            {
                m_AddressLines2 = value;
            }
        }
        public string City
        {
            get
            {
                return m_City;
            }
            set
            {
                m_City = value;
            }
        }
        public string State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }
        public string ZipCode
        {
            get
            {
                return m_ZipCode;
            }
            set
            {
                m_ZipCode = value;
            }
        }
        public string Country
        {
            get
            {
                return m_Country;
            }
            set
            {
                m_Country = value;
            }
        }
        public decimal CreditLimit
        {
            get
            {
                return m_CreditLimit;
            }
            set
            {
                m_CreditLimit = value;
            }
        }
        public bool ActivationStatus
        {
            get
            {
                return m_ActivationStatus;
            }
        }

        public CreditCard()
        {
            m_CardNumber = "";
            m_CardOwnerName = "";
            m_MerchantName = "";
            m_ExpDate = new DateTime().Date;
            m_AddressLines1 = "";
            m_AddressLines2 = "";
            m_City = "";
            m_State = "";
            m_ZipCode = "";
            m_Country = "";
            m_CreditLimit = 0.0M;
            m_ActivationStatus = false;
        }

        public CreditCard(string cardnumber, string ownername, string merchantname, string expdate, string address1, string address2, string city, string state, string zip, string country, decimal creditlimit)
        {
            this.CardNumber = cardnumber;
            this.CardOwnerName = ownername;
            this.MerchantName = merchantname;
            this.ExpDate = Convert.ToDateTime(expdate);
            this.AddressLines1 = address1;
            this.AddressLines2 = address2;
            this.City = city;
            this.State = state;
            this.ZipCode = zip;
            this.Country = country;
            this.CreditLimit = creditlimit;
            m_ActivationStatus = true;
        }
        ~CreditCard(){}

        public void Print()
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter("Network_Printer.txt");

                streamWriter.WriteLine("Credit Card information:");
                streamWriter.WriteLine("Card Number = {0}", CardNumber);
                streamWriter.WriteLine("Card Owner Name = {0}", CardOwnerName);
                streamWriter.WriteLine("Merchant Name = {0}", MerchantName);
                streamWriter.WriteLine("Expiration Date = {0}", ExpDate);
                streamWriter.WriteLine("AddressLine1 = {0}", AddressLines1);
                streamWriter.WriteLine("AddressLine2 = {0}", AddressLines2);
                streamWriter.WriteLine("City = {0}", City);
                streamWriter.WriteLine("State = {0}", State);
                streamWriter.WriteLine("Zip code = {0}", ZipCode);
                streamWriter.WriteLine("Country = {0}", Country);
                streamWriter.WriteLine("Credit Limit = {0}", CreditLimit);
                streamWriter.WriteLine("Activation Status = {0}", ActivationStatus);
                
                streamWriter.WriteLine();
                streamWriter.WriteLine();

                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public bool Activate()
        {
            m_ActivationStatus = true;
            return m_ActivationStatus;
        }
        public bool Deactivate()
        {
            m_ActivationStatus = false;
            return m_ActivationStatus;
        }

        public bool Load(string key) 
        {
            return DALayer_Load(key);
        }
        public bool Insert()
        {
            return DALayer_Insert();
        }
        public bool InsertCreditCardOfACustomer(string customerkey)
        {
            //return true if it worked, false otherwise
            return DALayer_InsertCreditCardOfACustomer(customerkey);
        }
        public bool Update()
        {
            //return true if it worked, false otherwise
            return DALayer_Update();
        }
        public bool Delete(string key)
        {
            //return true if it worked, false otherwise
            return DALayer_Delete(key);
        }

        protected bool DALayer_Load(string key)
        {
            //Start Error Trapping
            try
            {
                //Use DAL object Factory to get the SQL Server FACTORY Data Access Object
                DALObjectFactoryBase objSQLDAOFactory =
                DALObjectFactoryBase.GetDataSourceDAOFactory(DALObjectFactoryBase.SQLSERVER);
                //now that you have the sql FACTORY data access object
                //call the correct Data Access Object to perform the Data Access
                CreditCardDAO objCreditCardDAO = objSQLDAOFactory.GetCreditCardDAO();
                //call the CreditCardDAO Data Access Object to do the work
                CreditCardDTO objCreditCardDTO = objCreditCardDAO.GetRecordByID(key);
                //test if DTO object exists & populate this object with DTO object's properties
                //and return a true or return a False if no DTO object exists.
                if (objCreditCardDTO != null)
                {
                    //get the data from the Data Transfer Object
                    this.CardNumber = objCreditCardDTO.CardNumber;
                    this.CardOwnerName = objCreditCardDTO.CardOwnerName;
                    this.MerchantName = objCreditCardDTO.MerchantName;
                    this.ExpDate = Convert.ToDateTime(objCreditCardDTO.ExpirationDate);
                    this.AddressLines1 = objCreditCardDTO.AddressLine1;
                    this.AddressLines2 = objCreditCardDTO.AddressLine2;
                    this.City = objCreditCardDTO.City;
                    this.State = objCreditCardDTO.State;
                    this.ZipCode = objCreditCardDTO.ZipCode;
                    this.Country = objCreditCardDTO.Country;
                    this.CreditLimit = objCreditCardDTO.CreditLimit;
                    //this.ActivationStatus = objCreditCardDTO.ActivationStatus;
                    //Handle activation status accordingly using methods
                    //since ActivationStutus property is read only
                    if (objCreditCardDTO.ActivationStatus == true)
                        this.Activate();
                    else
                        this.Deactivate();
                    //Returns a true since this class object has been populated.
                    return true;
                }
                else
                {
                    //No object returned from DALayer, return a false
                    return false;
                }
            }//End of try
             //Traps for general exception.
            catch (Exception objE)
            {
                //Re-Throw an general exceptions
                throw new Exception("Unexpected Error in DALayer_Load(key) Method: {0} " + objE.Message);
            }
        }//End of method

        protected bool DALayer_Insert()
        {
            //Start Error Trapping
            try
            {
                //Use DAL object Factory to get the SQL Server FACTORY Data Access Object
                DALObjectFactoryBase objSQLDAOFactory =
                DALObjectFactoryBase.GetDataSourceDAOFactory(DALObjectFactoryBase.SQLSERVER);
                //now that you have the sql FACTORY data access object
                //call the correct Data Access Object to perform the Data Access
                CreditCardDAO objCreditCardDAO = objSQLDAOFactory.GetCreditCardDAO();
                //Create new Data Transfer Object to send to DA Later for DATA ACCESS LAYER
                CreditCardDTO objCreditCardDTO = new CreditCardDTO();
                //POPULATE the Data Transfer Object with data from THIS OBJECT to send to database
                objCreditCardDTO.CardNumber = this.CardNumber;
                objCreditCardDTO.CardOwnerName = this.CardOwnerName;
                objCreditCardDTO.MerchantName = this.MerchantName;
                objCreditCardDTO.ExpirationDate = this.ExpDate;
                objCreditCardDTO.AddressLine1 = this.AddressLines1;
                objCreditCardDTO.AddressLine2 = this.AddressLines2;
                objCreditCardDTO.City = this.City;
                objCreditCardDTO.State = this.State;
                objCreditCardDTO.ZipCode = this.ZipCode;
                objCreditCardDTO.Country = this.Country;
                objCreditCardDTO.CreditLimit = this.CreditLimit;
                objCreditCardDTO.ActivationStatus = this.ActivationStatus;
                //call the CreditCardDAO Data Access Object to do the work
                bool inserted = objCreditCardDAO.Insert(objCreditCardDTO);
                //test if insert to database was successful return true,
                //otherwise return false
                if (inserted == true)
                {
                    //Returns a true since this class object has been inserted & marked as old.
                    return true;
                }
                else
                {
                    //No record inserted, return a false
                    return false;
                }
            }//End of try
             //Traps for general exception.
            catch (Exception objE)
            {
                //Re-Throw an general exceptions
                throw new Exception("Unexpected Error in DALayer_Insert() Method: {0} " + objE.Message);
            }
        }//End of method

        protected bool DALayer_InsertCreditCardOfACustomer(string parentKey)
        {
            //Start Error Trapping
            try
            {
                //Use DAL object Factory to get the SQL Server FACTORY Data Access Object
                DALObjectFactoryBase objSQLDAOFactory =
                DALObjectFactoryBase.GetDataSourceDAOFactory(DALObjectFactoryBase.SQLSERVER);
                //now that you have the sql FACTORY data access object
                //call the correct Data Access Object to perform the Data Access
                CreditCardDAO objCreditCardDAO = objSQLDAOFactory.GetCreditCardDAO();
                //Create new Data Transfer Object to send to DA Later for DATA ACCESS LAYER
                CreditCardDTO objCreditCardDTO = new CreditCardDTO();
                //POPULATE the Data Transfer Object with data from THIS OBJECT to send to database
                objCreditCardDTO.CardNumber = this.CardNumber;
                objCreditCardDTO.CardOwnerName = this.CardOwnerName;
                objCreditCardDTO.MerchantName = this.MerchantName;
                objCreditCardDTO.ExpirationDate = this.ExpDate;
                objCreditCardDTO.AddressLine1 = this.AddressLines1;
                objCreditCardDTO.AddressLine2 = this.AddressLines2;
                objCreditCardDTO.City = this.City;
                objCreditCardDTO.State = this.State;
                objCreditCardDTO.ZipCode = this.ZipCode;
                objCreditCardDTO.Country = this.Country;
                //call the CreditCardDAO Data Access Object to do the work
                bool inserted = objCreditCardDAO.InsertChildObjectOfAParent(Convert.ToInt32(parentKey), objCreditCardDTO);
                //test if insert to database was successful & MARK the object as old return true,
                //otherwise return false
                if (inserted == true)
                {
                    //Returns a true since this class object has been inserted.
                    return true;
                }
                else
                {
                    //No record inserted, return a false
                    return false;
                }
            }//End of try
             //Traps for general exception.
            catch (Exception objE)
            {
                //Re-Throw an general exceptions
                throw new Exception("Unexpected Error in DALayer_InsertCreditCardOfACustomer () Method: {0}" + objE.Message);
            }
        }//End of method
        protected bool DALayer_Update()
        {
            //Start Error Trapping
            try
            {
                //Use DAL object Factory to get the SQL Server FACTORY Data Access Object
                DALObjectFactoryBase objSQLDAOFactory =
                DALObjectFactoryBase.GetDataSourceDAOFactory(DALObjectFactoryBase.SQLSERVER);
                //now that you have the sql FACTORY data access object
                //call the correct Data Access Object to perform the Data Access
                CreditCardDAO objCreditCardDAO = objSQLDAOFactory.GetCreditCardDAO();
                //Create new Data Transfer Object to send to DA Later for DATA ACCESS LAYER
                CreditCardDTO objCreditCardDTO = new CreditCardDTO();
                //POPULATE the Data Transfer Object with data from THIS OBJECT to send to database
                objCreditCardDTO.CardNumber = this.CardNumber;
                objCreditCardDTO.CardOwnerName = this.CardOwnerName;
                objCreditCardDTO.MerchantName = this.MerchantName;
                objCreditCardDTO.ExpirationDate = this.ExpDate;
                objCreditCardDTO.AddressLine1 = this.AddressLines1;
                objCreditCardDTO.AddressLine2 = this.AddressLines2;
                objCreditCardDTO.City = this.City;
                objCreditCardDTO.State = this.State;
                objCreditCardDTO.ZipCode = this.ZipCode;
                objCreditCardDTO.Country = this.Country;
                objCreditCardDTO.CreditLimit = this.CreditLimit;
                objCreditCardDTO.ActivationStatus = this.ActivationStatus;
                //call the CreditCardDAO Data Access Object to do the work
                bool updated = objCreditCardDAO.Update(objCreditCardDTO);
                //test if update to database was successful & MARK the object as old return true,
                //otherwise return false
                if (updated == true)
                {
                    //Returns a true since this class object has been updated.
                    return true;
                }
                else
                {
                    //No record updated, return a false
                    return false;
                }
            }//End of try
             //Traps for general exception.
            catch (Exception objE)
            {
                //Re-Throw an general exceptions
                throw new Exception("Unexpected Error in DALayer_Update() Method: {0} " + objE.Message);
            }
        }//End of method

        protected bool DALayer_Delete(string key)
        {
            //Start Error Trapping
            try
            {
                //Use DAL object Factory to get the SQL Server FACTORY Data Access Object
                DALObjectFactoryBase objSQLDAOFactory =
                DALObjectFactoryBase.GetDataSourceDAOFactory(DALObjectFactoryBase.SQLSERVER);
                //now that you have the sql FACTORY data access object
                //call the correct Data Access Object to perform the Data Access
                CreditCardDAO objCreditCardDAO = objSQLDAOFactory.GetCreditCardDAO();
                //call the CreditCardDAO Data Access Object to do the work
                bool deleted = objCreditCardDAO.Delete(key);
                //Test if delete to database was successful & MARK the object as NEW since
                //deleted from database and NEW in memory and return a true, otherwise return false
                if (deleted == true)
                {
                    //Returns a true since this class object has been deleted & marked as NEW.
                    return true;
                }
                else
                {
                    //No record deleted, return a false
                    return false;
                }
            }//End of try
             //Traps for general exception.
            catch (Exception objE)
            {
                //Re-Throw an general exceptions
                throw new Exception("Unexpected Error in DALayer_Update() Method: {0} " + objE.Message);
            }
        }//End of method

    }
}
