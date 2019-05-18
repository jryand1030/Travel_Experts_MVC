using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.CreditCards
{
    public class CreditCardsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static CreditCards GetValue(int objID)
        {
            CreditCards obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT CreditCardId, CCName, CCNumber, CCExpiry, CustomerId " +
                "FROM CreditCards " +
                "WHERE CreditCardId = @CreditCardId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@CreditCardId", objID);

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
                    obj = new CreditCards();
                    obj.CreditCardId = Convert.ToInt32(reader["CreditCardId"]);
                    obj.CCName = reader["CCName"].ToString();
                    obj.CCNumber = reader["CCNumber"].ToString();
                    obj.CCExpiry = (DateTime)reader["CCExpiry"];
                    obj.CustomerId = Convert.ToInt32(reader["CustomerId"]);
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
        public static List<CreditCards> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT CreditCardId, CCName, CCNumber, CCExpiry, CustomerId " +
                "FROM CreditCards ";
            SqlConnection connection = TravelExperts.GetConection();
            List<CreditCards> dataList = new List<CreditCards>(); // epmty list
            CreditCards data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new CreditCards();
                data.CreditCardId = Convert.ToInt32(reader["CreditCardId"]);
                data.CCName = reader["CCName"].ToString();
                data.CCNumber = reader["CCNumber"].ToString();
                data.CCExpiry = (DateTime)reader["CCExpiry"];
                data.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(CreditCards obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO CreditCards(CreditCardId, CCName, CCNumber, CCExpiry, CustomerId) " +
                "OUTPUT inserted.[CreditCardId] " +
                "VALUES(@CreditCardId, @CCName, @CCNumber, @CCExpiry, @CustomerId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@CreditCardId", obj.CreditCardId);
            cmd.Parameters.AddWithValue("@CCName", obj.CCName);
            cmd.Parameters.AddWithValue("@CCNumber", obj.CCNumber);
            cmd.Parameters.AddWithValue("@CCExpiry", obj.CCExpiry);
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);

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
        public static bool Delete(CreditCards obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM CreditCards " +
                "WHERE CreditCardId = @CreditCardId " + // needed for identification of object
                "AND CCName = @CCName " + // the rest - for optimistic concurrency
                "AND CCNumber = @CCNumber " +
                "AND CCExpiry = @CCExpiry " +
                "AND CustomerId = @CustomerId ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@CreditCardId", obj.CreditCardId);
            cmd.Parameters.AddWithValue("@CCName", obj.CCName);
            cmd.Parameters.AddWithValue("@CCNumber", obj.CCNumber);
            cmd.Parameters.AddWithValue("@CCExpiry", obj.CCExpiry);
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);

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
        public static bool UpdateCreditCard(CreditCards oldObj, CreditCards newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE CreditCards SET " +
                "CreditCardId = @NewCreditCardId, " +
                "CCName = @NewCCName, " +
                "CCNumber = @NewCCNumber, " +
                "CCExpiry = @NewCCExpiry " +
                "WHERE CreditCardId = @OldCreditCardId " + // identifies
                "AND CCName = @OldCCName " + // the rest - for optimistic concurrency
                "AND CCNumber = @OldCCNumber " +
                "AND CCExpiry = @OldCCExpiry " +
                "AND CustomerId = @OldCustomerId ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewCreditCardId", newObj.CreditCardId);
            cmd.Parameters.AddWithValue("@NewCCName", newObj.CCName);
            cmd.Parameters.AddWithValue("@NewCCNumber", newObj.CCNumber);
            cmd.Parameters.AddWithValue("@NewCCExpiry", newObj.CCExpiry);
            cmd.Parameters.AddWithValue("@NewCustomerId", newObj.CustomerId);
            // ID
            cmd.Parameters.AddWithValue("@OldCreditCardId", oldObj.CreditCardId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldCCName", oldObj.CCName);
            cmd.Parameters.AddWithValue("@OldCCNumber", oldObj.CCNumber);
            cmd.Parameters.AddWithValue("@OldCCExpiry", oldObj.CCExpiry);
            cmd.Parameters.AddWithValue("@OldCustomerId", oldObj.CustomerId);

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
