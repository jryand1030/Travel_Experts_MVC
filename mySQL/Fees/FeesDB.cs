using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Fees
{
    public class FeesDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Fees GetValue(int objID)
        {
            Fees obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT FeeId, FeeName, FeeAmt, FeeDesc " +
                "FROM Fees " +
                "WHERE FeeId = @FeeId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@FeeId", objID);

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
                    obj = new Fees();
                    obj.FeeId = reader["FeeId"].ToString();
                    obj.FeeName = reader["FeeName"].ToString();
                    obj.FeeAmt = Convert.ToDecimal(reader["FeeAmt"]);
                    obj.FeeDesc = reader["FeeDesc"].ToString();
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
        public static List<Fees> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT FeeId, FeeName, FeeAmt, FeeDesc " +
                "FROM Fees ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Fees> dataList = new List<Fees>(); // epmty list
            Fees data; // for reading
            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Fees();
                data.FeeId = reader["FeeId"].ToString();
                data.FeeName = reader["FeeName"].ToString();
                data.FeeAmt = Convert.ToDecimal(reader["FeeAmt"]);
                data.FeeDesc = reader["FeeDesc"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Fees obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Fees(FeeId, FeeName, FeeAmt, FeeDesc) " +
                "OUTPUT inserted.[FeeId] " +
                "VALUES(@FeeId, @FeeName, @FeeAmt, @FeeDesc) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@FeeId", obj.FeeId);
            cmd.Parameters.AddWithValue("@FeeName", obj.FeeName);
            cmd.Parameters.AddWithValue("@FeeAmt", obj.FeeAmt);
            cmd.Parameters.AddWithValue("@FeeDesc", obj.FeeDesc);

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
        public static bool Delete(Fees obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Fees " +
                "WHERE FeeId = @FeeId " + // needed for identification of object
                "AND FeeName = @FeeName " + // the rest - for optimistic concurrency
                "AND FeeAmt = @FeeAmt " +
                "AND FeeDesc = @FeeDesc ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@FeeId", obj.FeeId);
            cmd.Parameters.AddWithValue("@FeeName", obj.FeeName);
            cmd.Parameters.AddWithValue("@FeeAmt", obj.FeeAmt);
            cmd.Parameters.AddWithValue("@FeeDesc", obj.FeeDesc);

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
        public static bool Update(Fees oldObj, Fees newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Fees SET " +
                "FeeId = @NewFeeId, " +
                "FeeName = @NewFeeName, " +
                "FeeAmt = @NewFeeAmt, " +
                "FeeDesc = @NewFeeDesc " +
                "WHERE FeeId = @OldFeeId " + // identifies
                "AND FeeName = @OldFeeName " + // the rest - for optimistic concurrency
                "AND FeeAmt = @OldFeeAmt " +
                "AND FeeDesc = @OldFeeDesc ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewFeeId", newObj.FeeId);
            cmd.Parameters.AddWithValue("@NewFeeName", newObj.FeeName);
            cmd.Parameters.AddWithValue("@NewFeeAmt", newObj.FeeAmt);
            cmd.Parameters.AddWithValue("@NewFeeDesc", newObj.FeeDesc);
            // ID
            cmd.Parameters.AddWithValue("@OldFeeId", oldObj.FeeId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldFeeName", oldObj.FeeName);
            cmd.Parameters.AddWithValue("@OldFeeAmt", oldObj.FeeAmt);
            cmd.Parameters.AddWithValue("@OldFeeDesc", oldObj.FeeDesc);

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
