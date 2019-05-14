using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Customers_Rewards
{
    public class Customers_RewardsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Customers_Rewards GetValue(int objID)
        {
            Customers_Rewards obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT CustomerId, RewardId, RwdNumber " +
                "FROM Customers_Rewards " +
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
                    obj = new Customers_Rewards();
                    obj.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    obj.RewardId = Convert.ToInt32(reader["RewardId"]);
                    obj.RwdNumber = reader["RwdNumber"].ToString();
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
        public static List<Customers_Rewards> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT CustomerId, RewardId, RwdNumber " +
                "FROM Customers_Rewards ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Customers_Rewards> dataList = new List<Customers_Rewards>(); // epmty list
            Customers_Rewards data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Customers_Rewards();
                data.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                data.RewardId = Convert.ToInt32(reader["RewardId"]);
                data.RwdNumber = reader["RwdNumber"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Customers_Rewards obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Customers_Rewards(CustomerId, RewardId, RwdNumber) " +
                "OUTPUT inserted.[CustomerId] " +
                "VALUES(@CustomerId, @RewardId, @RwdNumber) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
            cmd.Parameters.AddWithValue("@RewardId", obj.RewardId);
            cmd.Parameters.AddWithValue("@RwdNumber", obj.RwdNumber);

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
        public static bool Delete(Customers_Rewards obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Customers_Rewards " +
                "WHERE CustomerId = @CustomerId " + // needed for identification of object
                "AND RewardId = @RewardId " + // the rest - for optimistic concurrency
                "AND RwdNumber = @RwdNumber ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
            cmd.Parameters.AddWithValue("@RewardId", obj.RewardId);
            cmd.Parameters.AddWithValue("@RwdNumber", obj.RwdNumber);

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
        public static bool UpdateCustomers_Reward(Customers_Rewards oldObj, Customers_Rewards newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Customers_Rewards SET " +
                "CustomerId = @NewCustomerId, " +
                "RewardId = @NewRewardId, " +
                "RwdNumber = @NewRwdNumber " +
                "WHERE CustomerId = @OldCustomerId " + // identifies
                "AND RewardId = @OldRewardId " + // the rest - for optimistic concurrency
                "AND RwdNumber = @OldRwdNumber ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewCustomerId", newObj.CustomerId);
            cmd.Parameters.AddWithValue("@NewRewardId", newObj.RewardId);
            cmd.Parameters.AddWithValue("@NewRwdNumber", newObj.RwdNumber);
            // ID
            cmd.Parameters.AddWithValue("@OldCustomerId", oldObj.CustomerId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldRewardId", oldObj.RewardId);
            cmd.Parameters.AddWithValue("@OldRwdNumber", oldObj.RwdNumber);

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
