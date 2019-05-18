using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Classes
{
    public class ClassesDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Classes GetValue(int objID)
        {
            Classes obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT ClassId, ClassName, ClassDesc " +
                "FROM Classes " +
                "WHERE ClassId = @ClassId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ClassId", objID);

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
                    obj = new Classes();
                    obj.ClassId = reader["ClassId"].ToString();
                    obj.ClassName = reader["ClassName"].ToString();
                    obj.ClassDesc = reader["ClassDesc"].ToString();
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
        public static List<Classes> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT ClassId, ClassName, ClassDesc " +
                "FROM Classes ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Classes> dataList = new List<Classes>(); // epmty list
            Classes data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Classes();
                data.ClassId = reader["ClassId"].ToString();
                data.ClassName = reader["ClassName"].ToString();
                data.ClassDesc = reader["ClassDesc"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Classes obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Classes(ClassId, ClassName, ClassDesc) " +
                "OUTPUT inserted.[ClassId] " +
                "VALUES(@ClassId, @ClassName, @ClassDesc) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ClassId", obj.ClassId);
            cmd.Parameters.AddWithValue("@ClassName", obj.ClassName);
            cmd.Parameters.AddWithValue("@ClassDesc", obj.ClassDesc);

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
        public static bool Delete(Classes obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Classes " +
                "WHERE ClassId = @ClassId " + // needed for identification of object
                "AND ClassName = @ClassName " + // the rest - for optimistic concurrency
                "AND ClassDesc = @ClassDesc ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ClassId", obj.ClassId);
            cmd.Parameters.AddWithValue("@ClassName", obj.ClassName);
            cmd.Parameters.AddWithValue("@ClassDesc", obj.ClassDesc);

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
        public static bool Update(Classes oldObj, Classes newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Classes SET " +
                "ClassId = @NewClassId, " +
                "ClassName = @NewClassName, " +
                "ClassDesc = @NewClassDesc " +
                "WHERE ClassId = @OldClassId " + // identifies
                "AND ClassName = @OldClassName " + // the rest - for optimistic concurrency
                "AND ClassDesc = @OldClassDesc ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewClassId", newObj.ClassId);
            cmd.Parameters.AddWithValue("@NewClassName", newObj.ClassName);
            cmd.Parameters.AddWithValue("@NewClassDesc", newObj.ClassDesc);
            // ID
            cmd.Parameters.AddWithValue("@OldClassId", oldObj.ClassId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldClassName", oldObj.ClassName);
            cmd.Parameters.AddWithValue("@OldClassDesc", oldObj.ClassDesc);

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
