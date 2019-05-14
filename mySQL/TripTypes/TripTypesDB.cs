using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL
{
    public class TripTypesDB
    {
        #region GetValue
        // retrieve object with given ID
        public static TripTypes GetValue(int objID)
        {
            TripTypes obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT TripTypeId, TTName " +
                "FROM TripTypes " +
                "WHERE TripTypeId = @TripTypeId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@TripTypeId", objID);

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
                    obj = new TripTypes();
                    obj.TripTypeId = reader["TripTypeId"].ToString();
                    obj.TTName = reader["TTName"].ToString();
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
        public static List<TripTypes> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT TripTypeId, TTName " +
                "FROM TripTypes ";
            SqlConnection connection = TravelExperts.GetConection();
            List<TripTypes> dataList = new List<TripTypes>(); // epmty list
            TripTypes data; // for reading
            // create connection
            

            
            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new TripTypes();
                data.TripTypeId = reader["TripTypeId"].ToString();
                data.TTName = reader["TTName"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(TripTypes obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO TripTypes(TripTypeId, TTName) " +
                "OUTPUT inserted.[TripTypeId] " +
                "VALUES(@TripTypeId, @TTName) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@TripTypeId", obj.TripTypeId);
            cmd.Parameters.AddWithValue("@TTName", obj.TTName);

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
        public static bool Delete(TripTypes obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM TripTypes " +
                "WHERE TripTypeId = @TripTypeId " + // needed for identification of object
                "AND TTName = @TTName "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@TripTypeId", obj.TripTypeId);
            cmd.Parameters.AddWithValue("@TTName", obj.TTName);

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
        public static bool Update(TripTypes oldObj, TripTypes newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE TripTypes SET " +
                "TripTypeId = @NewTripTypeId, " +
                "TTName = @NewTTName " +
                "WHERE TripTypeId = @OldTripTypeId " + // identifies
                "AND TTName = @OldTTName "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewTripTypeId", newObj.TripTypeId);
            cmd.Parameters.AddWithValue("@NewTTName", newObj.TTName);
            // ID
            cmd.Parameters.AddWithValue("@OldTripTypeId", oldObj.TripTypeId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldTTName", oldObj.TTName);

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
