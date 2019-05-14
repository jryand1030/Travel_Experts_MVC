using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Agents
{
    public class AgentsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Agents GetValue(int objID)
        {
            Agents obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT AgentId, AgtFirstName, AgtMiddleInitial, AgtLastName, AgtBusPhone, AgtEmail, AgtPosition, AgencyId " +
                "FROM Agents " +
                "WHERE AgentId = @AgentId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AgentId", objID);

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
                    obj = new Agents();
                    obj.AgentId = Convert.ToInt32(reader["AgentId"]);
                    obj.AgtFirstName = reader["AgtFirstName"].ToString();
                    obj.AgtMiddleInitial = reader["AgtMiddleInitial"].ToString();
                    obj.AgtLastName = reader["AgtLastName"].ToString();
                    obj.AgtBusPhone = reader["AgtBusPhone"].ToString();
                    obj.AgtEmail = reader["AgtEmail"].ToString();
                    obj.AgtPosition = reader["AgtPosition"].ToString();
                    obj.AgencyId = Convert.ToInt32(reader["AgencyId"]);
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
        public static List<Agents> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT AgentId, AgtFirstName, AgtMiddleInitial, AgtLastName, AgtBusPhone, AgtEmail, AgtPosition, AgencyId " +
                "FROM Agents ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Agents> dataList = new List<Agents>(); // epmty list
            Agents data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Agents();
                data.AgentId = Convert.ToInt32(reader["AgentId"]);
                data.AgtFirstName = reader["AgtFirstName"].ToString();
                data.AgtMiddleInitial = reader["AgtMiddleInitial"].ToString();
                data.AgtLastName = reader["AgtLastName"].ToString();
                data.AgtBusPhone = reader["AgtBusPhone"].ToString();
                data.AgtEmail = reader["AgtEmail"].ToString();
                data.AgtPosition = reader["AgtPosition"].ToString();
                data.AgencyId = Convert.ToInt32(reader["AgencyId"]);
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Agents obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Agents(AgentId, AgtFirstName, AgtMiddleInitial, AgtLastName, AgtBusPhone, AgtEmail, AgtPosition, AgencyId) " +
                "OUTPUT inserted.[AgentId] " +
                "VALUES(@AgentId, @AgtFirstName, @AgtMiddleInitial, @AgtLastName, @AgtBusPhone, @AgtEmail, @AgtPosition, @AgencyId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AgentId", obj.AgentId);
            cmd.Parameters.AddWithValue("@AgtFirstName", obj.AgtFirstName);
            cmd.Parameters.AddWithValue("@AgtMiddleInitial", obj.AgtMiddleInitial);
            cmd.Parameters.AddWithValue("@AgtLastName", obj.AgtLastName);
            cmd.Parameters.AddWithValue("@AgtBusPhone", obj.AgtBusPhone);
            cmd.Parameters.AddWithValue("@AgtEmail", obj.AgtEmail);
            cmd.Parameters.AddWithValue("@AgtPosition", obj.AgtPosition);
            cmd.Parameters.AddWithValue("@AgencyId", obj.AgencyId);

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
        public static bool Delete(Agents obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Agents " +
                "WHERE AgentId = @AgentId " + // needed for identification of object
                "AND AgtFirstName = @AgtFirstName " + // the rest - for optimistic concurrency
                "AND AgtMiddleInitial = @AgtMiddleInitial " +
                "AND AgtLastName = @AgtLastName " +
                "AND AgtBusPhone = @AgtBusPhone " +
                "AND AgtEmail = @AgtEmail " +
                "AND AgtPosition = @AgtPosition " +
                "AND AgencyId = @AgencyId ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@AgentId", obj.AgentId);
            cmd.Parameters.AddWithValue("@AgtFirstName", obj.AgtFirstName);
            cmd.Parameters.AddWithValue("@AgtMiddleInitial", obj.AgtMiddleInitial);
            cmd.Parameters.AddWithValue("@AgtLastName", obj.AgtLastName);
            cmd.Parameters.AddWithValue("@AgtBusPhone", obj.AgtBusPhone);
            cmd.Parameters.AddWithValue("@AgtEmail", obj.AgtEmail);
            cmd.Parameters.AddWithValue("@AgtPosition", obj.AgtPosition);
            cmd.Parameters.AddWithValue("@AgencyId", obj.AgencyId);

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
        public static bool Update(Agents oldObj, Agents newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Agents SET " +
                "AgentId = @NewAgentId, " +
                "AgtFirstName = @NewAgtFirstName, " +
                "AgtMiddleInitial = @NewAgtMiddleInitial, " +
                "AgtLastName = @NewAgtLastName, " +
                "AgtBusPhone = @NewAgtBusPhone, " +
                "AgtEmail = @NewAgtEmail, " +
                "AgtPosition = @NewAgtPosition, " +
                "AgencyId = @NewAgencyId " +
                "WHERE AgentId = @OldAgentId " + // identifies
                "AND AgtFirstName = @OldAgtFirstName " + // the rest - for optimistic concurrency
                "AND AgtFirstName = @OldAgtFirstName " +
                "AND AgtMiddleInitial = @OldAgtMiddleInitial " +
                "AND AgtLastName = @OldAgtLastName " +
                "AND AgtBusPhone = @OldAgtBusPhone " +
                "AND AgtEmail = @OldAgtEmail " +
                "AND AgtPosition = @OldAgtPosition " +
                "AND AgencyId = @OldAgencyId ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewAgentId", newObj.AgentId);
            cmd.Parameters.AddWithValue("@NewAgtFirstName", newObj.AgtFirstName);
            cmd.Parameters.AddWithValue("@NewAgtMiddleInitial", newObj.AgtMiddleInitial);
            cmd.Parameters.AddWithValue("@NewAgtLastName", newObj.AgtLastName);
            cmd.Parameters.AddWithValue("@NewAgtBusPhone", newObj.AgtBusPhone);
            cmd.Parameters.AddWithValue("@NewAgtEmail", newObj.AgtEmail);
            cmd.Parameters.AddWithValue("@NewAgtPosition", newObj.AgtPosition);
            cmd.Parameters.AddWithValue("@NewAgencyId", newObj.AgencyId);
            // ID
            cmd.Parameters.AddWithValue("@OldAgentId", oldObj.AgentId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldAgtFirstName", oldObj.AgtFirstName);
            cmd.Parameters.AddWithValue("@OldAgtMiddleInitial", oldObj.AgtMiddleInitial);
            cmd.Parameters.AddWithValue("@OldAgtLastName", oldObj.AgtLastName);
            cmd.Parameters.AddWithValue("@OldAgtBusPhone", oldObj.AgtBusPhone);
            cmd.Parameters.AddWithValue("@OldAgtEmail", oldObj.AgtEmail);
            cmd.Parameters.AddWithValue("@OldAgtPosition", oldObj.AgtPosition);
            cmd.Parameters.AddWithValue("@OldAgencyId", oldObj.AgencyId);

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
