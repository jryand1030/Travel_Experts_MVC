using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Affiliations
{
    public class AffiliationsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Affiliations GetValue(int objID)
        {
            Affiliations obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT AffilitationId, AffName, AffDesc " +
                "FROM Affiliations " +
                "WHERE AffilitationId = @AffilitationId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AffilitationId", objID);

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
                    obj = new Affiliations();
                    obj.AffilitationId = reader["AffilitationId"].ToString();
                    obj.AffName = reader["AffName"].ToString();
                    obj.AffDesc = reader["AffDesc"].ToString();
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
        public static List<Affiliations> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT AffilitationId, AffName, AffDesc " +
                "FROM Affiliations ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Affiliations> dataList = new List<Affiliations>(); // epmty list
            Affiliations data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Affiliations();
                data.AffilitationId = reader["AffilitationId"].ToString();
                data.AffName = reader["AffName"].ToString();
                data.AffDesc = reader["AffDesc"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Affiliations obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Affiliations(AffilitationId, AffName, AffDesc) " +
                "OUTPUT inserted.[AffilitationId] " +
                "VALUES(@AffilitationId, @AffName, @AffDesc) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AffilitationId", obj.AffilitationId);
            cmd.Parameters.AddWithValue("@AffName", obj.AffName);
            cmd.Parameters.AddWithValue("@AffDesc", obj.AffDesc);

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
        public static bool Delete(Affiliations obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Affiliations " +
                "WHERE AffilitationId = @AffilitationId " + // needed for identification of object
                "AND AffName = @AffName " + // the rest - for optimistic concurrency
                "AND AffDesc = @AffDesc ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AffilitationId", obj.AffilitationId);
            cmd.Parameters.AddWithValue("@AffName", obj.AffName);
            cmd.Parameters.AddWithValue("@AffDesc", obj.AffDesc);

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
        public static bool Update(Affiliations oldObj, Affiliations newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Affiliations SET " +
                "AffilitationId = @NewAffilitationId, " +
                "AffName = @NewAffName, " +
                "AffDesc = @NewAffDesc " +
                "WHERE TripTypeId = @OldTripTypeId " + // identifies
                "AND AffName = @OldAffName " + // the rest - for optimistic concurrency
                "AND AffDesc = @OldAffDesc ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewAffilitationId", newObj.AffilitationId);
            cmd.Parameters.AddWithValue("@NewAffName", newObj.AffName);
            cmd.Parameters.AddWithValue("@NewAffDesc", newObj.AffDesc);
            // ID
            cmd.Parameters.AddWithValue("@OldAffilitationId", oldObj.AffilitationId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldAffName", oldObj.AffName);
            cmd.Parameters.AddWithValue("@OldAffDesc", oldObj.AffDesc);

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
