using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.Bookings
{
    public class BookingsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static Bookings GetValue(int objID)
        {
            Bookings obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT BookingId, BookingDate, BookingNo, TravelerCount, CustomerId, TripTypeId, PackageId " +
                "FROM Bookings " +
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
                    obj = new Bookings();
                    obj.BookingId = Convert.ToInt32(reader["BookingId"]);
                    obj.BookingDate = (DateTime)reader["BookingDate"];
                    obj.BookingNo = reader["BookingNo"].ToString();
                    obj.TravelerCount = Convert.ToSingle(reader["TravelerCount"]);
                    obj.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                    obj.TripTypeId = reader["TripTypeId"].ToString();
                    if (reader["PackageId"].ToString() == "")
                    {
                        obj.PackageId = null;
                    }
                    else
                    {
                        obj.PackageId = Convert.ToInt32(reader["PackageId"].ToString());
                    }
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
        public static List<Bookings> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT BookingId, BookingDate, BookingNo, TravelerCount, CustomerId, TripTypeId, PackageId " +
                "FROM Bookings ";
            SqlConnection connection = TravelExperts.GetConection();
            List<Bookings> dataList = new List<Bookings>(); // epmty list
            Bookings data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new Bookings();
                data.BookingId = Convert.ToInt32(reader["BookingId"]);
                data.BookingDate = (DateTime)reader["BookingDate"];
                data.BookingNo = reader["BookingNo"].ToString();
                data.TravelerCount = Convert.ToSingle(reader["TravelerCount"]);
                data.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                data.TripTypeId = reader["TripTypeId"].ToString();
                if (reader["PackageId"].ToString() == "")
                {
                    data.PackageId = null;
                }
                else
                {
                    data.PackageId = Convert.ToInt32(reader["PackageId"].ToString());
                }
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(Bookings obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO Bookings(BookingId, BookingDate, BookingNo, TravelerCount, CustomerId, TripTypeId, PackageId) " +
                "OUTPUT inserted.[TripTypeId] " +
                "VALUES(@BookingId, @BookingDate, @BookingNo, @TravelerCount, @CustomerId, @TripTypeId, @PackageId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@BookingId", obj.BookingId);
            cmd.Parameters.AddWithValue("@BookingDate", obj.BookingDate);
            cmd.Parameters.AddWithValue("@BookingNo", obj.BookingNo);
            cmd.Parameters.AddWithValue("@TravelerCount", obj.TravelerCount);
            cmd.Parameters.AddWithValue("@TripTypeId", obj.TripTypeId);
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
            cmd.Parameters.AddWithValue("@TripTypeId", obj.TripTypeId);
            cmd.Parameters.AddWithValue("@PackageId", obj.PackageId);

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
        public static bool Delete(Bookings obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM Bookings " +
                "WHERE TripTypeId = @TripTypeId " + // needed for identification of object
                "AND TTName = @TTName "; // the rest - for optimistic concurrency
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@BookingId", obj.BookingId);
            cmd.Parameters.AddWithValue("@BookingDate", obj.BookingDate);
            cmd.Parameters.AddWithValue("@BookingNo", obj.BookingNo);
            cmd.Parameters.AddWithValue("@TravelerCount", obj.TravelerCount);
            cmd.Parameters.AddWithValue("@TripTypeId", obj.TripTypeId);
            cmd.Parameters.AddWithValue("@CustomerId", obj.CustomerId);
            cmd.Parameters.AddWithValue("@TripTypeId", obj.TripTypeId);
            cmd.Parameters.AddWithValue("@PackageId", obj.PackageId);

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
        public static bool Update(Bookings oldObj, Bookings newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE Bookings SET " +
                "BookingId = @NewNewBookingId, " +
                "BookingDate = @NewNewBookingDate, " +
                "BookingNo = @NewNewBookingNo, " +
                "TravelerCount = @NewNewTravelerCount, " +
                "TripTypeId = @NewNewTripTypeId, " +
                "CustomerId = @NewNewCustomerId, " +
                "TripTypeId = @NewTripTypeId, " +
                "PackageId = @NewPackageId " +
                "WHERE BookingId = @OldBookingId " + // identifies
                "AND BookingDate = @OldBookingDate " + // the rest - for optimistic concurrency
                "AND BookingNo = @OldBookingNo " +
                "AND TravelerCount = @OldTravelerCount " +
                "AND TripTypeId = @OldTripTypeId " +
                "AND CustomerId = @OldCustomerId " +
                "AND TripTypeId = @OldTripTypeId " +
                "AND PackageId = @OldPackageId ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewBookingId", newObj.BookingId);
            cmd.Parameters.AddWithValue("@NewBookingDate", newObj.BookingDate);
            cmd.Parameters.AddWithValue("@NewBookingNo", newObj.BookingNo);
            cmd.Parameters.AddWithValue("@NewTravelerCount", newObj.TravelerCount);
            cmd.Parameters.AddWithValue("@NewTripTypeId", newObj.TripTypeId);
            cmd.Parameters.AddWithValue("@NewCustomerId", newObj.CustomerId);
            cmd.Parameters.AddWithValue("@NewTripTypeId", newObj.TripTypeId);
            cmd.Parameters.AddWithValue("@NewPackageId", newObj.PackageId);
            // ID
            cmd.Parameters.AddWithValue("@OldBookingId", oldObj.BookingId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldBookingDate", oldObj.BookingDate);
            cmd.Parameters.AddWithValue("@OldBookingNo", oldObj.BookingNo);
            cmd.Parameters.AddWithValue("@OldTravelerCount", oldObj.TravelerCount);
            cmd.Parameters.AddWithValue("@OldTripTypeId", oldObj.TripTypeId);
            cmd.Parameters.AddWithValue("@OldCustomerId", oldObj.CustomerId);
            cmd.Parameters.AddWithValue("@OldTripTypeId", oldObj.TripTypeId);
            cmd.Parameters.AddWithValue("@OldPackageId", oldObj.PackageId);

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
