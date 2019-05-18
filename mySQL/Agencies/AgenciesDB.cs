using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Agencies
{
    public class AgenciesDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Agencies GetValue(int objID)
        {
            Agencies obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT AgencyId, AgncyAddress, AgncyCity, AgncyProv, AgncyPostal, AgncyCountry, AgncyPhone, AgncyFax " +
                "FROM Agencies " +
                "WHERE AgencyId = @AgencyId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AgencyId", objID);

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
                    obj = new Agencies();
                    obj.AgencyId = Convert.ToInt32(reader["AgencyId"]);
                    obj.AgncyAddress = reader["AgncyAddress"].ToString();
                    obj.AgncyCity = reader["AgncyCity"].ToString();
                    obj.AgncyProv = reader["AgncyProv"].ToString();
                    obj.AgncyPostal = reader["AgncyPostal"].ToString();
                    obj.AgncyCountry = reader["AgncyCountry"].ToString();
                    obj.AgncyPhone = reader["AgncyPhone"].ToString();
                    obj.AgncyFax = reader["AgncyFax"].ToString();
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
        public static List<Agencies> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT AgencyId, AgncyAddress, AgncyCity, AgncyProv, AgncyPostal, AgncyCountry, AgncyPhone, AgncyFax " +
                "FROM Agencies ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Agencies> dataList = new List<Agencies>(); // epmty list
            Agencies data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Agencies();
                data.AgencyId = Convert.ToInt32(reader["AgencyId"]);
                data.AgncyAddress = reader["AgncyAddress"].ToString();
                data.AgncyCity = reader["AgncyCity"].ToString();
                data.AgncyProv = reader["AgncyProv"].ToString();
                data.AgncyPostal = reader["AgncyPostal"].ToString();
                data.AgncyCountry = reader["AgncyCountry"].ToString();
                data.AgncyPhone = reader["AgncyPhone"].ToString();
                data.AgncyFax = reader["AgncyFax"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Agencies obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Agencies(AgencyId, AgncyAddress, AgncyCity, AgncyProv, AgncyPostal, AgncyCountry, AgncyPhone, AgncyFax) " +
                "OUTPUT inserted.[AgencyId] " +
                "VALUES(@AgencyId, @AgencyId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AgencyId", obj.AgencyId);
            cmd.Parameters.AddWithValue("@AgncyAddress", obj.AgncyAddress);
            cmd.Parameters.AddWithValue("@AgncyCity", obj.AgncyCity);
            cmd.Parameters.AddWithValue("@AgncyProv", obj.AgncyProv);
            cmd.Parameters.AddWithValue("@AgncyPostal", obj.AgncyPostal);
            cmd.Parameters.AddWithValue("@AgncyCountry", obj.AgncyCountry);
            cmd.Parameters.AddWithValue("@AgncyPhone", obj.AgncyPhone);
            cmd.Parameters.AddWithValue("@AgncyFax", obj.AgncyFax);

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
        public static bool Delete(Agencies obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Agencies " +
                "WHERE AgencyId = @AgencyId " + // needed for identification of object
                "AND AgncyAddress = @AgncyAddress " + // the rest - for optimistic concurrency
                "AND AgncyCity = @AgncyCity " +
                "AND AgncyProv = @AgncyProv " +
                "AND AgncyPostal = @AgncyPostal " +
                "AND AgncyCountry = @AgncyCountry " +
                "AND AgncyPhone = @AgncyPhone " +
                "AND AgncyFax = @AgncyFax ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AgencyId", obj.AgencyId);
            cmd.Parameters.AddWithValue("@AgncyAddress", obj.AgncyAddress);
            cmd.Parameters.AddWithValue("@AgncyCity", obj.AgncyCity);
            cmd.Parameters.AddWithValue("@AgncyProv", obj.AgncyProv);
            cmd.Parameters.AddWithValue("@AgncyPostal", obj.AgncyPostal);
            cmd.Parameters.AddWithValue("@AgncyCountry", obj.AgncyCountry);
            cmd.Parameters.AddWithValue("@AgncyPhone", obj.AgncyPhone);
            cmd.Parameters.AddWithValue("@AgncyFax", obj.AgncyFax);

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
        public static bool Update(Agencies oldObj, Agencies newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Agencies SET " +
                "AgencyId = @NewAgencyId, " +
                "AgncyAddress = @NewAgncyAddress, " +
                "AgncyCity = @NewAgncyCity, " +
                "AgncyProv = @NewAgncyProv, " +
                "AgncyPostal = @NewAgncyPostal, " +
                "AgncyCountry = @NewAgncyCountry, " +
                "AgncyPhone = @NewAgncyPhone, " +
                "AgncyFax = @NewAgncyFax " +
                "WHERE AgencyId = @OldAgencyId " + // needed for identification of object
                "AND AgncyAddress = @OldAgncyAddress " + // the rest - for optimistic concurrency
                "AND AgncyCity = @OldAgncyCity " +
                "AND AgncyProv = @OldAgncyProv " +
                "AND AgncyPostal = @OldAgncyPostal " +
                "AND AgncyCountry = @OldAgncyCountry " +
                "AND AgncyPhone = @OldAgncyPhone " +
                "AND AgncyFax = @OldAgncyFax "; 
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewAgencyId", newObj.AgencyId);
            cmd.Parameters.AddWithValue("@NewAgncyAddress", newObj.AgncyAddress);
            cmd.Parameters.AddWithValue("@NewAgncyCity", newObj.AgncyCity);
            cmd.Parameters.AddWithValue("@NewAgncyProv", newObj.AgncyProv);
            cmd.Parameters.AddWithValue("@NewAgncyPostal", newObj.AgncyPostal);
            cmd.Parameters.AddWithValue("@NewAgncyCountry", newObj.AgncyCountry);
            cmd.Parameters.AddWithValue("@NewAgncyPhone", newObj.AgncyPhone);
            cmd.Parameters.AddWithValue("@NewAgncyFax", newObj.AgncyFax);
            // ID
            cmd.Parameters.AddWithValue("@OldAgencyId", oldObj.AgencyId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldAgncyAddress", oldObj.AgncyAddress);
            cmd.Parameters.AddWithValue("@OldAgncyCity", oldObj.AgncyCity);
            cmd.Parameters.AddWithValue("@OldAgncyProv", oldObj.AgncyProv);
            cmd.Parameters.AddWithValue("@OldAgncyPostal", oldObj.AgncyPostal);
            cmd.Parameters.AddWithValue("@OldAgncyCountry", oldObj.AgncyCountry);
            cmd.Parameters.AddWithValue("@OldAgncyPhone", oldObj.AgncyPhone);
            cmd.Parameters.AddWithValue("@OldAgncyFax", oldObj.AgncyFax);

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
