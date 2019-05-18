using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Regions
{
    public class RegionsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Regions GetValue(int objID)
        {
            Regions obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT RegionId, RegionName " +
                "FROM Regions " +
                "WHERE RegionId = @RegionId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@RegionId", objID);

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
                    obj = new Regions();
                    obj.RegionId = reader["RegionId"].ToString();
                    obj.RegionName = reader["RegionName"].ToString();
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
        public static List<Regions> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT RegionId, RegionName " +
                "FROM Regions ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Regions> dataList = new List<Regions>(); // epmty list
            Regions data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Regions();
                data.RegionId = reader["RegionId"].ToString();
                data.RegionName = reader["RegionName"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Regions obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Regions(RegionId, RegionName) " +
                "OUTPUT inserted.[RegionId] " +
                "VALUES(@RegionId, @RegionName) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@RegionId", obj.RegionId);
            cmd.Parameters.AddWithValue("@RegionName", obj.RegionName);

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
        public static bool Delete(Regions obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Regions " +
                "WHERE RegionId = @RegionId " + // needed for identification of object
                "AND RegionName = @RegionName "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@RegionId", obj.RegionId);
            cmd.Parameters.AddWithValue("@RegionName", obj.RegionName);

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
        public static bool Update(Regions oldObj, Regions newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Bookings SET " +
                "RegionId = @NewRegionId, " +
                "RegionName = @NewRegionName " +
                "WHERE RegionId = @OldRegionId " + // identifies
                "AND RegionName = @OldRegionName "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewRegionId", newObj.RegionId);
            cmd.Parameters.AddWithValue("@NewRegionName", newObj.RegionName);
            // ID
            cmd.Parameters.AddWithValue("@OldRegionId", oldObj.RegionId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldRegionName", oldObj.RegionName);

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
