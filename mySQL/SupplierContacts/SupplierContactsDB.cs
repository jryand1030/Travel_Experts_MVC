using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.SupplierContacts
{
    public class SupplierContactsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static SupplierContacts GetValue(int objID)
        {
            SupplierContacts obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT SupplierContactId, SupConFirstName, SupConLastName, SupConCompany, SupConAddress, SupConCity, SupConProv, SupConPostal, SupConCountry, SupConBusPhone, SupConFax, SupConEmail, SupConURL, AffiliationId, SupplierId " +
                "FROM SupplierContacts " +
                "WHERE SupplierContactId = @SupplierContactId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@SupplierContactId", objID);

            // run the SELECT query
            try
            {
                // open the conection
                connection.Open();

                // run the command
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                // build object object to return
                if (reader.Read()) // if there is a object with this ID
                {
                    obj = new SupplierContacts();
                    obj.SupplierContactId = Convert.ToInt32(reader["SupplierContactId"]);
                    obj.SupConFirstName = reader["SupConFirstName"].ToString();
                    obj.SupConLastName = reader["SupConLastName"].ToString();
                    obj.SupConCompany = reader["SupConCompany"].ToString();
                    obj.SupConAddress = reader["SupConAddress"].ToString();
                    obj.SupConCity = reader["SupConCity"].ToString();
                    obj.SupConProv = reader["SupConProv"].ToString();
                    obj.SupConPostal = reader["SupConPostal"].ToString();
                    obj.SupConCountry = reader["SupConCountry"].ToString();
                    obj.SupConBusPhone = reader["SupConBusPhone"].ToString();
                    obj.SupConFax = reader["SupConFax"].ToString();
                    obj.SupConEmail = reader["SupConEmail"].ToString();
                    obj.SupConURL = reader["SupConURL"].ToString();
                    obj.AffiliationId = reader["AffiliationId"].ToString();
                    obj.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally // executes always
            {
                connection.Close();
            }

            return obj;
        }
        #endregion

        #region GetAll
        // retrieve all objects
        public static List<SupplierContacts> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT SupplierContactId, SupConFirstName, SupConLastName, SupConCompany, SupConAddress, SupConCity, SupConProv, SupConPostal, SupConCountry, SupConBusPhone, SupConFax, SupConEmail, SupConURL, AffiliationId, SupplierId " +
                "FROM SupplierContacts ";
            SqlConnection connection = TravelExperts.GetConection();
            List<SupplierContacts> dataList = new List<SupplierContacts>(); // epmty list
            SupplierContacts data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new SupplierContacts();
                data.SupplierContactId = Convert.ToInt32(reader["SupplierContactId"]);
                data.SupConFirstName = reader["SupConFirstName"].ToString();
                data.SupConLastName = reader["SupConLastName"].ToString();
                data.SupConCompany = reader["SupConCompany"].ToString();
                data.SupConAddress = reader["SupConAddress"].ToString();
                data.SupConCity = reader["SupConCity"].ToString();
                data.SupConProv = reader["SupConProv"].ToString();
                data.SupConPostal = reader["SupConPostal"].ToString();
                data.SupConCountry = reader["SupConCountry"].ToString();
                data.SupConBusPhone = reader["SupConBusPhone"].ToString();
                data.SupConFax = reader["SupConFax"].ToString();
                data.SupConEmail = reader["SupConEmail"].ToString();
                data.SupConURL = reader["SupConURL"].ToString();
                data.AffiliationId = reader["AffiliationId"].ToString();
                data.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(SupplierContacts obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO SupplierContacts(SupplierContactId, SupConFirstName, SupConLastName, SupConCompany, SupConAddress, SupConCity, SupConProv, SupConPostal, SupConCountry, SupConBusPhone, SupConFax, SupConEmail, SupConURL, AffiliationId, SupplierId) " +
                "OUTPUT inserted.[SupplierContactId] " +
                "VALUES(@SupplierContactId, @SupConFirstName, @SupConLastName, @SupConCompany, @SupConAddress, @SupConCity, @SupConProv, @SupConPostal, @SupConCountry, @SupConBusPhone, @SupConFax, @SupConEmail, @SupConURL, @AffiliationId, @SupplierId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@SupplierContactId", obj.SupplierContactId);
            cmd.Parameters.AddWithValue("@SupConFirstName", obj.SupConFirstName);
            cmd.Parameters.AddWithValue("@SupConLastName", obj.SupConLastName);
            cmd.Parameters.AddWithValue("@SupConCompany", obj.SupConCompany);
            cmd.Parameters.AddWithValue("@SupConAddress", obj.SupConAddress);
            cmd.Parameters.AddWithValue("@SupConCity", obj.SupConCity);
            cmd.Parameters.AddWithValue("@SupConProv", obj.SupConProv);
            cmd.Parameters.AddWithValue("@SupConPostal", obj.SupConPostal);
            cmd.Parameters.AddWithValue("@SupConCountry", obj.SupConCountry);
            cmd.Parameters.AddWithValue("@SupConBusPhone", obj.SupConBusPhone);
            cmd.Parameters.AddWithValue("@SupConFax", obj.SupConFax);
            cmd.Parameters.AddWithValue("@SupConEmail", obj.SupConEmail);
            cmd.Parameters.AddWithValue("@SupConURL", obj.SupConURL);
            cmd.Parameters.AddWithValue("@AffiliationId", obj.AffiliationId);
            cmd.Parameters.AddWithValue("@SupplierId", obj.SupplierId);

            // execute the INSERT command
            try
            {
                // open the conection
                connection.Open();

                // execute insert command
                custID = (int)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally // executes always
            {
                connection.Close();
            }
            // retrieve generated customer nID to return
            return custID;
        }
        #endregion

        #region Delete
        // Delete object
        // return indicator of success
        public static bool Delete(SupplierContacts obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM SupplierContacts " +
                "WHERE SupplierContactId = @SupplierContactId " + // needed for identification of object
                "AND SupConFirstName = @SupConFirstName " + // the rest - for optimistic concurrency
                "AND SupConLastName = @SupConLastName " +
                "AND SupConCompany = @SupConCompany " +
                "AND SupConAddress = @SupConAddress " +
                "AND SupConCity = @SupConCity " +
                "AND SupConProv = @SupConProv " +
                "AND SupConPostal = @SupConPostal " +
                "AND SupConCountry = @SupConCountry " +
                "AND SupConBusPhone = @SupConBusPhone " +
                "AND SupConFax = @SupConFax " +
                "AND SupConEmail = @SupConEmail " +
                "AND SupConURL = @SupConURL " +
                "AND AffiliationId = @AffiliationId " +
                "AND SupplierId = @SupplierId ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@SupplierContactId", obj.SupplierContactId);
            cmd.Parameters.AddWithValue("@SupConFirstName", obj.SupConFirstName);
            cmd.Parameters.AddWithValue("@SupConLastName", obj.SupConLastName);
            cmd.Parameters.AddWithValue("@SupConCompany", obj.SupConCompany);
            cmd.Parameters.AddWithValue("@SupConAddress", obj.SupConAddress);
            cmd.Parameters.AddWithValue("@SupConCity", obj.SupConCity);
            cmd.Parameters.AddWithValue("@SupConProv", obj.SupConProv);
            cmd.Parameters.AddWithValue("@SupConPostal", obj.SupConPostal);
            cmd.Parameters.AddWithValue("@SupConCountry", obj.SupConCountry);
            cmd.Parameters.AddWithValue("@SupConBusPhone", obj.SupConBusPhone);
            cmd.Parameters.AddWithValue("@SupConFax", obj.SupConFax);
            cmd.Parameters.AddWithValue("@SupConEmail", obj.SupConEmail);
            cmd.Parameters.AddWithValue("@SupConURL", obj.SupConURL);
            cmd.Parameters.AddWithValue("@AffiliationId", obj.AffiliationId);
            cmd.Parameters.AddWithValue("@SupplierId", obj.SupplierId);


            // execute the command
            try
            {
                // open the conection
                connection.Open();

                // execute the command
                int count = cmd.ExecuteNonQuery();

                // check if successful
                if (count > 0) success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally // executes always
            {
                connection.Close();
            }

            // retrieve generated customer nID to return
            return success;
        }
        #endregion

        #region Update
        // Update object
        // return indicator of success
        public static bool Update(SupplierContacts oldObj, SupplierContacts newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE SupplierContacts SET " +
                "SupplierContactId = @NewSupplierContactId, " +
                "SupConFirstName = @NewSupConFirstName, " +
                "SupConLastName = @NewSupConLastName, " +
                "SupConCompany = @NewSupConCompany, " +
                "SupConAddress = @NewSupConAddress, " +
                "SupConCity = @NewSupConCity, " +
                "SupConProv = @NewSupConProv, " +
                "SupConPostal = @NewSupConPostal, " +
                "SupConCountry = @NewSupConCountry, " +
                "SupConBusPhone = @NewSupConBusPhone, " +
                "SupConFax = @NewSupConFax, " +
                "SupConEmail = @NewSupConEmail, " +
                "SupConURL = @NewSupConURL, " +
                "AffiliationId = @NewAffiliationId, " +
                "SupplierId = @NewSupplierId " +
                "WHERE SupplierContactId = @OldSupplierContactId " + // identifies
                "AND SupConFirstName = @OldSupConFirstName " + // the rest - for optimistic concurrency
                "AND SupConLastName = @OldSupConLastName " +
                "AND SupConCompany = @OldSupConCompany " +
                "AND SupConAddress = @OldSupConAddress " +
                "AND SupConCity = @OldSupConCity " +
                "AND SupConProv = @OldSupConProv " +
                "AND SupConPostal = @OldSupConPostal " +
                "AND SupConCountry = @OldSupConCountry " +
                "AND SupConBusPhone = @OldSupConBusPhone " +
                "AND SupConFax = @OldSupConFax " +
                "AND SupConEmail = @OldSupConEmail " +
                "AND SupConURL = @OldSupConURL " +
                "AND AffiliationId = @OldAffiliationId " +
                "AND SupplierId = @OldSupplierId ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewSupplierContactId", newObj.SupplierContactId);
            cmd.Parameters.AddWithValue("@NewSupConFirstName", newObj.SupConFirstName);
            cmd.Parameters.AddWithValue("@NewSupConLastName", newObj.SupConLastName);
            cmd.Parameters.AddWithValue("@NewSupConCompany", newObj.SupConCompany);
            cmd.Parameters.AddWithValue("@NewSupConAddress", newObj.SupConAddress);
            cmd.Parameters.AddWithValue("@NewSupConCity", newObj.SupConCity);
            cmd.Parameters.AddWithValue("@NewSupConProv", newObj.SupConProv);
            cmd.Parameters.AddWithValue("@NewSupConPostal", newObj.SupConPostal);
            cmd.Parameters.AddWithValue("@NewSupConCountry", newObj.SupConCountry);
            cmd.Parameters.AddWithValue("@NewSupConBusPhone", newObj.SupConBusPhone);
            cmd.Parameters.AddWithValue("@NewSupConFax", newObj.SupConFax);
            cmd.Parameters.AddWithValue("@NewSupConEmail", newObj.SupConEmail);
            cmd.Parameters.AddWithValue("@NewSupConURL", newObj.SupConURL);
            cmd.Parameters.AddWithValue("@NewAffiliationId", newObj.AffiliationId);
            cmd.Parameters.AddWithValue("@NewSupplierId", newObj.SupplierId);
            // ID
            cmd.Parameters.AddWithValue("@OldSupplierContactId", oldObj.SupplierContactId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldSupConFirstName", oldObj.SupConFirstName);
            cmd.Parameters.AddWithValue("@OldSupConLastName", oldObj.SupConLastName);
            cmd.Parameters.AddWithValue("@OldSupConCompany", oldObj.SupConCompany);
            cmd.Parameters.AddWithValue("@OldSupConAddress", oldObj.SupConAddress);
            cmd.Parameters.AddWithValue("@OldSupConCity", oldObj.SupConCity);
            cmd.Parameters.AddWithValue("@OldSupConProv", oldObj.SupConProv);
            cmd.Parameters.AddWithValue("@OldSupConPostal", oldObj.SupConPostal);
            cmd.Parameters.AddWithValue("@OldSupConCountry", oldObj.SupConCountry);
            cmd.Parameters.AddWithValue("@OldSupConBusPhone", oldObj.SupConBusPhone);
            cmd.Parameters.AddWithValue("@OldSupConFax", oldObj.SupConFax);
            cmd.Parameters.AddWithValue("@OldSupConEmail", oldObj.SupConEmail);
            cmd.Parameters.AddWithValue("@OldSupConURL", oldObj.SupConURL);
            cmd.Parameters.AddWithValue("@OldAffiliationId", oldObj.AffiliationId);
            cmd.Parameters.AddWithValue("@OldSupplierId", oldObj.SupplierId);


            // execute the UPDATE command
            try
            {
                // open the conection
                connection.Open();

                // execute the command
                int count = cmd.ExecuteNonQuery();

                // check if successful
                if (count > 0) success = true; // updated

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally // executes always
            {
                connection.Close();
            }
            // retrieve generated object ID to return
            return success;
        }
        #endregion
    }
}
