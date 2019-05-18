using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Suppliers
{
    public class SuppliersDB
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
                "SELECT SupplierId, SupName " +
                "FROM Suppliers " +
                "WHERE SupplierId = @SupplierId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@SupplierId", objID);

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
                    obj.TripTypeId = reader["SupplierId"].ToString();
                    obj.TTName = reader["SupName"].ToString();
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
                "SELECT SupplierId, SupName " +
                "FROM Suppliers ";
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
                data.TripTypeId = reader["SupplierId"].ToString();
                data.TTName = reader["SupName"].ToString();
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
                "INSERT INTO Suppliers(SupplierId, SupName) " +
                "OUTPUT inserted.[SupplierId] " +
                "VALUES(@SupplierId, @SupName) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@SupplierId", obj.TripTypeId);
            cmd.Parameters.AddWithValue("@SupName", obj.TTName);

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
                "DELETE FROM Suppliers " +
                "WHERE SupplierId = @SupplierId " + // needed for identification of object
                "AND SupName = @SupName "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@SupplierId", obj.TripTypeId);
            cmd.Parameters.AddWithValue("@SupName", obj.TTName);

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
                "UPDATE Suppliers SET " +
                "SupplierId = @NewSupplierId, " +
                "SupName = @NewTTName " +
                "WHERE SupplierId = @OldSupplierId " + // identifies
                "AND SupName = @OldSupName "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewSupplierId", newObj.TripTypeId);
            cmd.Parameters.AddWithValue("@NewSupName", newObj.TTName);
            // ID
            cmd.Parameters.AddWithValue("@OldSupplierId", oldObj.TripTypeId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldSupName", oldObj.TTName);

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
