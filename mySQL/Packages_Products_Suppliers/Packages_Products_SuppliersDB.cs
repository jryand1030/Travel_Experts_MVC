using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Packages_Products_Suppliers
{
    public class Packages_Products_SuppliersDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Packages_Products_Suppliers GetValue(int objID)
        {
            Packages_Products_Suppliers obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT PackageId, ProductSupplierId " +
                "FROM Packages_Products_Suppliers " +
                "WHERE PackageId = @PackageId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@PackageId", objID);

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
                    obj = new Packages_Products_Suppliers();
                    obj.PackageId = Convert.ToInt32(reader["PackageId"]);
                    obj.ProductSupplierId = Convert.ToInt32(reader["ProductSupplierId"]);
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
        public static List<Packages_Products_Suppliers> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT PackageId, ProductSupplierId " +
                "FROM Packages_Products_Suppliers ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Packages_Products_Suppliers> dataList = new List<Packages_Products_Suppliers>(); // epmty list
            Packages_Products_Suppliers data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Packages_Products_Suppliers();
                data.PackageId = Convert.ToInt32(reader["PackageId"]);
                data.ProductSupplierId = Convert.ToInt32(reader["ProductSupplierId"]);
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Packages_Products_Suppliers obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Packages_Products_Suppliers(PackageId, ProductSupplierId) " +
                "OUTPUT inserted.[PackageId] " +
                "VALUES(@PackageId, @ProductSupplierId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@PackageId", obj.PackageId);
            cmd.Parameters.AddWithValue("@ProductSupplierId", obj.ProductSupplierId);

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
        public static bool Delete(Packages_Products_Suppliers obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Packages_Products_Suppliers " +
                "WHERE PackageId = @PackageId " + // needed for identification of object
                "AND ProductSupplierId = @ProductSupplierId "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@PackageId", obj.PackageId);
            cmd.Parameters.AddWithValue("@ProductSupplierId", obj.ProductSupplierId);

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
        public static bool Update(Packages_Products_Suppliers oldObj, Packages_Products_Suppliers newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Packages_Products_Suppliers SET " +
                "PackageId = @NewPackageId, " +
                "ProductSupplierId = @NewProductSupplierId " +
                "WHERE PackageId = @OldPackageId " + // identifies
                "AND ProductSupplierId = @OldProductSupplierId "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewPackageId", newObj.PackageId);
            cmd.Parameters.AddWithValue("@NewProductSupplierId", newObj.ProductSupplierId);
            // ID
            cmd.Parameters.AddWithValue("@OldPackageId", oldObj.PackageId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldProductSupplierId", oldObj.ProductSupplierId);

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
