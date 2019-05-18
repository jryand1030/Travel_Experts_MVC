using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Rewards
{
    public class RewardsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Rewards GetValue(int objID)
        {
            Rewards obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT RewardId, RwdName, RwdDesc " +
                "FROM Rewards " +
                "WHERE RewardId = @RewardId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@RewardId", objID);

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
                    obj = new Rewards();
                    obj.RewardId = Convert.ToInt32(reader["RewardId"]);
                    obj.RwdName = reader["RwdName"].ToString();
                    obj.RwdDesc = reader["RwdDesc"].ToString();
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
        public static List<Rewards> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT RewardId, RwdName, RwdDesc " +
                "FROM Rewards ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Rewards> dataList = new List<Rewards>(); // epmty list
            Rewards data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Rewards();
                data.RewardId = Convert.ToInt32(reader["RewardId"]);
                data.RwdName = reader["RwdName"].ToString();
                data.RwdDesc = reader["RwdDesc"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Rewards obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Rewards(RewardId, RwdName, RwdDesc) " +
                "OUTPUT inserted.[RewardId] " +
                "VALUES(@RewardId, @RwdName, @RwdDesc) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@RewardId", obj.RewardId);
            cmd.Parameters.AddWithValue("@RwdName", obj.RwdName);
            cmd.Parameters.AddWithValue("@RwdDesc", obj.RwdDesc);

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
        public static bool Delete(Rewards obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Rewards " +
                "WHERE RewardId = @RewardId " + // needed for identification of object
                "AND RwdName = @RwdName " + // the rest - for optimistic concurrency
                "AND RwdDesc = @RwdDesc ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@RewardId", obj.RewardId);
            cmd.Parameters.AddWithValue("@RwdName", obj.RwdName);
            cmd.Parameters.AddWithValue("@RwdDesc", obj.RwdDesc);

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
        public static bool Update(Rewards oldObj, Rewards newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Rewards SET " +
                "RewardId = @NewRewardId, " +
                "RwdName = @NewRwdName, " +
                "RwdDesc = @NewRwdDesc " +
                "WHERE RewardId = @OldRewardId " + // identifies
                "AND RwdName = @OldRwdName " + // the rest - for optimistic concurrency
                "AND RwdDesc = @OldRwdDesc ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewRewardId", newObj.RewardId);
            cmd.Parameters.AddWithValue("@NewRwdName", newObj.RwdName);
            cmd.Parameters.AddWithValue("@NewRwdDesc", newObj.RwdDesc);
            // ID
            cmd.Parameters.AddWithValue("@OldRewardId", oldObj.RewardId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldRwdName", oldObj.RwdName);
            cmd.Parameters.AddWithValue("@OldRwdDesc", oldObj.RwdDesc);

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
