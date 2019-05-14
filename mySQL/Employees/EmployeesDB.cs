using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Employees
{
    public class EmployeesDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Employees GetValue(int objID)
        {
            Employees obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT EmpFirstName, EmpMiddleInitial, EmpLastName, EmpBusPhone, EmpEmail, EmpPosition " +
                "FROM Employees " +
                "WHERE EmpEmail = @EmpEmail ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@EmpEmail", objID);

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
                    obj = new Employees();
                    obj.EmpFirstName = reader["EmpFirstName"].ToString();
                    obj.EmpMiddleInitial = reader["EmpMiddleInitial"].ToString();
                    obj.EmpLastName = reader["EmpLastName"].ToString();
                    obj.EmpBusPhone = reader["EmpBusPhone"].ToString();
                    obj.EmpEmail = reader["EmpEmail"].ToString();
                    obj.EmpPosition = reader["EmpPosition"].ToString();
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
        public static List<Employees> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT EmpFirstName, EmpMiddleInitial, EmpLastName, EmpBusPhone, EmpEmail, EmpPosition " +
                "FROM Employees ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Employees> dataList = new List<Employees>(); // epmty list
            Employees data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Employees();
                data.EmpFirstName = reader["EmpFirstName"].ToString();
                data.EmpMiddleInitial = reader["EmpMiddleInitial"].ToString();
                data.EmpLastName = reader["EmpLastName"].ToString();
                data.EmpBusPhone = reader["EmpBusPhone"].ToString();
                data.EmpEmail = reader["EmpEmail"].ToString();
                data.EmpPosition = reader["EmpPosition"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Employees obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Employees(EmpFirstName, EmpMiddleInitial, EmpLastName, EmpBusPhone, EmpEmail, EmpPosition) " +
                "OUTPUT inserted.[EmpEmail] " +
                "VALUES(@EmpFirstName, @EmpMiddleInitial, @EmpLastName, @EmpBusPhone, @EmpEmail, @EmpPosition) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@EmpEmail", obj.EmpEmail);
            cmd.Parameters.AddWithValue("@EmpFirstName", obj.EmpFirstName);
            cmd.Parameters.AddWithValue("@EmpMiddleInitial", obj.EmpMiddleInitial);
            cmd.Parameters.AddWithValue("@EmpLastName", obj.EmpLastName);
            cmd.Parameters.AddWithValue("@EmpBusPhone", obj.EmpBusPhone);
            cmd.Parameters.AddWithValue("@EmpPosition", obj.EmpPosition);

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
        public static bool Delete(Employees obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Employees " +
                "WHERE EmpEmail = @EmpEmail " + // needed for identification of object
                "AND EmpFirstName = @EmpFirstName " + // the rest - for optimistic concurrency
                "AND EmpMiddleInitial = @EmpMiddleInitial " +
                "AND EmpLastName = @EmpLastName " +
                "AND EmpBusPhone = @EmpBusPhone " +
                "AND EmpPosition = @EmpPosition ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@EmpEmail", obj.EmpEmail);
            cmd.Parameters.AddWithValue("@EmpFirstName", obj.EmpFirstName);
            cmd.Parameters.AddWithValue("@EmpMiddleInitial", obj.EmpMiddleInitial);
            cmd.Parameters.AddWithValue("@EmpLastName", obj.EmpLastName);
            cmd.Parameters.AddWithValue("@EmpBusPhone", obj.EmpBusPhone);
            cmd.Parameters.AddWithValue("@EmpPosition", obj.EmpPosition);

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
        public static bool Update(Employees oldObj, Employees newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Employees SET " +
                "EmpEmail = @NewEmpEmail, " +
                "EmpFirstName = @NewEmpFirstName, " +
                "EmpMiddleInitial = @NewEmpMiddleInitial, " +
                "EmpLastName = @NewEmpLastName, " +
                "EmpBusPhone = @NewEmpBusPhone, " +
                "EmpEmail = @NewEmpEmail, " +
                "EmpPosition = @NewEmpPosition " +
                "WHERE EmpEmail = @OldEmpEmail " + // identifies
                "AND EmpFirstName = @OldEmpFirstName " + // the rest - for optimistic concurrency
                "AND EmpMiddleInitial = @OldEmpMiddleInitial " +
                "AND EmpLastName = @OldEmpLastName " +
                "AND EmpBusPhone = @OldEmpBusPhone " +
                "AND EmpEmail = @OldEmpEmail " +
                "AND EmpPosition = @OldEmpPosition ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewEmpEmail", newObj.EmpEmail);
            cmd.Parameters.AddWithValue("@NewEmpFirstName", newObj.EmpFirstName);
            cmd.Parameters.AddWithValue("@NewEmpMiddleInitial", newObj.EmpMiddleInitial);
            cmd.Parameters.AddWithValue("@NewEmpLastName", newObj.EmpLastName);
            cmd.Parameters.AddWithValue("@NewEmpBusPhone", newObj.EmpBusPhone);
            cmd.Parameters.AddWithValue("@NewEmpEmail", newObj.EmpEmail);
            cmd.Parameters.AddWithValue("@NewEmpPosition", newObj.EmpPosition);
            // ID
            cmd.Parameters.AddWithValue("@OldEmpEmail", oldObj.EmpEmail);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldEmpFirstName", oldObj.EmpFirstName);
            cmd.Parameters.AddWithValue("@OldEmpMiddleInitial", oldObj.EmpMiddleInitial);
            cmd.Parameters.AddWithValue("@OldEmpLastName", oldObj.EmpLastName);
            cmd.Parameters.AddWithValue("@OldEmpBusPhone", oldObj.EmpBusPhone);
            cmd.Parameters.AddWithValue("@OldEmpEmail", oldObj.EmpEmail);
            cmd.Parameters.AddWithValue("@OldEmpPosition", oldObj.EmpPosition);

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
