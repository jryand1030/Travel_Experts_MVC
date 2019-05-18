using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Customers
{
    public class CustomersDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Customers GetValue(int objID)
        {
            Customers obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT CustomerId, CustFirstName, CustLastName, CustAddress, CustCity, CustProv, CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail, AgentId " +
                "FROM Customers " +
                "WHERE CustomerId = @CustomerId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@CustomerId", objID);

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
                    obj = new Customers();
                    obj.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    obj.CustFirstName = reader["CustFirstName"].ToString();
                    obj.CustLastName = reader["CustLastName"].ToString();
                    obj.CustAddress = reader["CustAddress"].ToString();
                    obj.CustCity = reader["CustCity"].ToString();
                    obj.CustProv = reader["CustProv"].ToString();
                    obj.CustPostal = reader["CustPostal"].ToString();
                    obj.CustCountry = reader["CustCountry"].ToString();
                    obj.CustHomePhone = reader["CustHomePhone"].ToString();
                    obj.CustBusPhone = reader["CustBusPhone"].ToString();
                    obj.CustEmail = reader["CustEmail"].ToString();
                    obj.AgentId = Convert.ToInt32(reader["AgentId"]);
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
        public static List<Customers> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT CustomerId, CustFirstName, CustLastName, CustAddress, CustCity, CustProv, CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail, AgentId " +
                "FROM Customers ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Customers> dataList = new List<Customers>(); // epmty list
            Customers data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Customers();
                data.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                data.CustFirstName = reader["CustFirstName"].ToString();
                data.CustLastName = reader["CustLastName"].ToString();
                data.CustAddress = reader["CustAddress"].ToString();
                data.CustCity = reader["CustCity"].ToString();
                data.CustProv = reader["CustProv"].ToString();
                data.CustPostal = reader["CustPostal"].ToString();
                data.CustCountry = reader["CustCountry"].ToString();
                data.CustHomePhone = reader["CustHomePhone"].ToString();
                data.CustBusPhone = reader["CustBusPhone"].ToString();
                data.CustEmail = reader["CustEmail"].ToString();
                data.AgentId = Convert.ToInt32(reader["AgentId"]);
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Customers obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Customers(CustomerId, CustFirstName, CustLastName, CustAddress, CustCity, CustProv, CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail, AgentId) " +
                "OUTPUT inserted.[CustomerId] " +
                "VALUES(@CustomerId, @CustFirstName, @CustLastName, @CustAddress, @CustCity, @CustProv, @CustPostal, @CustCountry, @CustHomePhone, @CustBusPhone, @CustEmail, @AgentId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
            cmd.Parameters.AddWithValue("@CustFirstName", obj.CustFirstName);
            cmd.Parameters.AddWithValue("@CustLastName", obj.CustLastName);
            cmd.Parameters.AddWithValue("@CustAddress", obj.CustAddress);
            cmd.Parameters.AddWithValue("@CustCity", obj.CustCity);
            cmd.Parameters.AddWithValue("@CustProv", obj.CustProv);
            cmd.Parameters.AddWithValue("@CustPostal", obj.CustPostal);
            cmd.Parameters.AddWithValue("@CustCountry", obj.CustCountry);
            cmd.Parameters.AddWithValue("@CustHomePhone", obj.CustHomePhone);
            cmd.Parameters.AddWithValue("@CustBusPhone", obj.CustBusPhone);
            cmd.Parameters.AddWithValue("@CustEmail", obj.CustEmail);
            cmd.Parameters.AddWithValue("@AgentId", obj.AgentId);

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
        public static bool Delete(Customers obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Customers " +
                "WHERE CustomerId = @CustomerId " + // needed for identification of object
                "AND CustFirstName = @CustFirstName " + // the rest - for optimistic concurrency
                "AND CustLastName = @CustLastName " +
                "AND CustAddress = @CustAddress " +
                "AND CustCity = @CustCity " +
                "AND CustProv = @CustProv " +
                "AND CustPostal = @CustPostal " +
                "AND CustCountry = @CustCountry " +
                "AND CustHomePhone = @CustHomePhone " +
                "AND CustBusPhone = @CustBusPhone " +
                "AND CustEmail = @CustEmail " +
                "AND AgentId = @AgentId ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
            cmd.Parameters.AddWithValue("@CustFirstName", obj.CustFirstName);
            cmd.Parameters.AddWithValue("@CustLastName", obj.CustLastName);
            cmd.Parameters.AddWithValue("@CustAddress", obj.CustAddress);
            cmd.Parameters.AddWithValue("@CustCity", obj.CustCity);
            cmd.Parameters.AddWithValue("@CustProv", obj.CustProv);
            cmd.Parameters.AddWithValue("@CustPostal", obj.CustPostal);
            cmd.Parameters.AddWithValue("@CustCountry", obj.CustCountry);
            cmd.Parameters.AddWithValue("@CustHomePhone", obj.CustHomePhone);
            cmd.Parameters.AddWithValue("@CustBusPhone", obj.CustBusPhone);
            cmd.Parameters.AddWithValue("@CustEmail", obj.CustEmail);
            cmd.Parameters.AddWithValue("@AgentId", obj.AgentId);

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
        public static bool UpdateCustomer(Customers oldObj, Customers newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Customers SET " +
                "CustomerId = @NewCustomerId, " +
                "CustFirstName = @NewCustFirstName, " +
                "CustLastName = @NewCustLastName, " +
                "CustAddress = @NewCustAddress, " +
                "CustCity = @NewCustCity, " +
                "CustProv = @NewCustProv, " +
                "CustPostal = @NewCustPostal, " +
                "CustCountry = @NewCustCountry, " +
                "CustHomePhone = @NewCustHomePhone, " +
                "CustBusPhone = @NewCustBusPhone, " +
                "CustEmail = @NewCustEmail, " +
                "AgentId = @NewAgentId " +
                "WHERE CustomerId = @OldCustomerId " + // identifies
                "AND CustFirstName = @OldCustFirstName " + // the rest - for optimistic concurrency
                "AND CustLastName = @OldCustLastName " +
                "AND CustAddress = @OldCustAddress " +
                "AND CustCity = @OldCustCity " +
                "AND CustProv = @OldCustProv " +
                "AND CustPostal = @OldCustPostal " +
                "AND CustCountry = @OldCustCountry " +
                "AND CustHomePhone = @OldCustHomePhone " +
                "AND CustBusPhone = @OldCustBusPhone " +
                "AND CustEmail = @OldCustEmail " +
                "AND AgentId = @OldAgentId ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewCustomerId", newObj.CustomerId);
            cmd.Parameters.AddWithValue("@NewCustFirstName", newObj.CustFirstName);
            cmd.Parameters.AddWithValue("@NewCustLastName", newObj.CustLastName);
            cmd.Parameters.AddWithValue("@NewCustAddress", newObj.CustAddress);
            cmd.Parameters.AddWithValue("@NewCustCity", newObj.CustCity);
            cmd.Parameters.AddWithValue("@NewCustProv", newObj.CustProv);
            cmd.Parameters.AddWithValue("@NewCustPostal", newObj.CustPostal);
            cmd.Parameters.AddWithValue("@NewCustCountry", newObj.CustCountry);
            cmd.Parameters.AddWithValue("@NewCustHomePhone", newObj.CustHomePhone);
            cmd.Parameters.AddWithValue("@NewCustBusPhone", newObj.CustBusPhone);
            cmd.Parameters.AddWithValue("@NewCustEmail", newObj.CustEmail);
            cmd.Parameters.AddWithValue("@NewAgentId", newObj.AgentId);
            // ID
            cmd.Parameters.AddWithValue("@OldCustomerId", oldObj.CustomerId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldCustFirstName", oldObj.CustFirstName);
            cmd.Parameters.AddWithValue("@OldCustLastName", oldObj.CustLastName);
            cmd.Parameters.AddWithValue("@OldCustAddress", oldObj.CustAddress);
            cmd.Parameters.AddWithValue("@OldCustCity", oldObj.CustCity);
            cmd.Parameters.AddWithValue("@OldCustProv", oldObj.CustProv);
            cmd.Parameters.AddWithValue("@OldCustPostal", oldObj.CustPostal);
            cmd.Parameters.AddWithValue("@OldCustCountry", oldObj.CustCountry);
            cmd.Parameters.AddWithValue("@OldCustHomePhone", oldObj.CustHomePhone);
            cmd.Parameters.AddWithValue("@OldCustBusPhone", oldObj.CustBusPhone);
            cmd.Parameters.AddWithValue("@OldCustEmail", oldObj.CustEmail);
            cmd.Parameters.AddWithValue("@OldAgentId", oldObj.AgentId);

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
