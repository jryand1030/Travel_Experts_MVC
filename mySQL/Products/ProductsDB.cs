using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL
{
    public class ProductsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Products GetValue(int objID)
        {
            Products obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT ProductID, ProdName " +
                "FROM Products " +
                "WHERE ProductID = @ProductID ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ProductID", objID);

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
                    obj = new Products();
                    obj.ProductID = Convert.ToInt32(reader["ProductID"]);
                    obj.ProdName = reader["ProdName"].ToString();
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
        public static List<Products> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT ProductID, ProdName " +
                "FROM Products ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Products> dataList = new List<Products>(); // epmty list
            Products data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Products();
                data.ProductID = Convert.ToInt32(reader["ProductID"]);
                data.ProdName = reader["ProdName"].ToString();
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Products obj)
        {
            int objID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Products(ProductID, ProdName) " +
                "OUTPUT inserted.[ProductID] " +
                "VALUES(@ProductID, @ProdName) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
            cmd.Parameters.AddWithValue("@ProdName", obj.ProdName);

            // execute the INSERT command
            try
            {
                // open the conection
                connection.Open();

                // execute insert command
                objID = (int)cmd.ExecuteScalar();

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
            return objID;
        }
        #endregion

        #region Delete
        // Delete object
        // return indicator of success
        public static bool Delete(Products obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Products " +
                "WHERE ProductID = @ProductID " + // needed for identification of object
                "AND ProdName = @ProdName "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ProductID", obj.ProductID);
            cmd.Parameters.AddWithValue("@ProdName", obj.ProdName);

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
        public static bool Update(Products oldObj, Products newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Products SET " +
                "ProductID = @NewProductID, " +
                "ProdName = @NewProdName " +
                "WHERE ProductID = @OldProductID " + // identifies
                "AND ProdName = @OldProdName "; // the rest - for optimistic concurrency;
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewProductID", newObj.ProductID);
            cmd.Parameters.AddWithValue("@NewProdName", newObj.ProdName);
            // ID
            cmd.Parameters.AddWithValue("@OldProductID", oldObj.ProductID);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldProdName", oldObj.ProdName);

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
