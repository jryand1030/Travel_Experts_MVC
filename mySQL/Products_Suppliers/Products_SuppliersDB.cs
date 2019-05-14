using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Products_Suppliers
{
    public class Products_SuppliersDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Products_Suppliers GetValue(int objID)
        {
            Products_Suppliers obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT ProductSupplierId, ProductId, SupplierId " +
                "FROM Products_Suppliers " +
                "WHERE ProductSupplierId = @ProductSupplierId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ProductSupplierId", objID);

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
                    obj = new Products_Suppliers();
                    obj.ProductSupplierId = Convert.ToInt32(reader["ProductSupplierId"]);
                    obj.ProductId = Convert.ToInt32(reader["ProductId"]);
                    obj.SupplierId = Convert.ToInt32(reader["SupplierId"]);
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
        public static List<Products_Suppliers> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT ProductSupplierId, ProductId, SupplierId " +
                "FROM Products_Suppliers ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Products_Suppliers> dataList = new List<Products_Suppliers>(); // epmty list
            Products_Suppliers data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Products_Suppliers();
                data.ProductSupplierId = Convert.ToInt32(reader["ProductSupplierId"]);
                data.ProductId = Convert.ToInt32(reader["ProductId"]);
                data.SupplierId = Convert.ToInt32(reader["SupplierId"]);
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Products_Suppliers obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Products_Suppliers(ProductSupplierId, ProductId, SupplierId) " +
                "OUTPUT inserted.[ProductSupplierId] " +
                "VALUES(@ProductSupplierId, @ProductId, @SupplierId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ProductSupplierId", obj.ProductSupplierId);
            cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
            cmd.Parameters.AddWithValue("@SupplierId", obj.SupplierId);

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
        public static bool Delete(Products_Suppliers obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Products_Suppliers " +
                "WHERE ProductSupplierId = @ProductSupplierId " + // needed for identification of object
                "AND ProductId = @ProductId " + // the rest - for optimistic concurrency
                "AND SupplierId = @SupplierId ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@ProductSupplierId", obj.ProductSupplierId);
            cmd.Parameters.AddWithValue("@ProductId", obj.ProductId);
            cmd.Parameters.AddWithValue("@SupplierId", obj.SupplierId);

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
        public static bool Update(Products_Suppliers oldObj, Products_Suppliers newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Products_Suppliers SET " +
                "ProductSupplierId = @NewProductSupplierId, " +
                "ProductId = @NewProductId, " +
                "SupplierId = @NewSupplierId " +
                "WHERE ProductSupplierId = @OldProductSupplierId " + // identifies
                "AND ProductId = @OldProductId " + // the rest - for optimistic concurrency
                "AND SupplierId = @OldSupplierId ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewProductSupplierId", newObj.ProductSupplierId);
            cmd.Parameters.AddWithValue("@NewProductId", newObj.ProductId);
            cmd.Parameters.AddWithValue("@NewSupplierId", newObj.SupplierId);
            // ID
            cmd.Parameters.AddWithValue("@OldProductSupplierId", oldObj.ProductSupplierId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldProductId", oldObj.ProductId);
            cmd.Parameters.AddWithValue("@OldSupplierId", oldObj.SupplierId);

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
