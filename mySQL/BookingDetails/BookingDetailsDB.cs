using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySQL.BookingDetails
{
    public class BookingDetailsDB
    {
        #region GetValue
        // retrieve object with given ID
        public static BookingDetails GetValue(int objID)
        {
            BookingDetails obj = null;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create SELECT command
            string query =
                "SELECT BookingDetailId, ItineraryNo, TripStart, TripEnd, Description, Destination, BasePrice, AgencyCommission, BookingId, RegionId, ClassId, FeeId, ProductSupplierId " +
                "FROM BookingDetails " +
                "WHERE BookingDetailId = @BookingDetailId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@BookingDetailId", objID);

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
                    obj = new BookingDetails();
                    obj.BookingDetailId = Convert.ToInt32(reader["BookingDetailId"]);
                    obj.ItineraryNo = Convert.ToSingle(reader["ItineraryNo"]);
                    obj.TripStart = (DateTime)reader["TripStart"];
                    obj.TripEnd = (DateTime)reader["TripEnd"];
                    obj.Description = reader["Description"].ToString();
                    obj.Destination = reader["Destination"].ToString();
                    obj.BasePrice = Convert.ToDecimal(reader["BasePrice"]);
                    obj.AgencyCommission = Convert.ToDecimal(reader["AgencyCommission"]);
                    obj.BookingId = Convert.ToInt32(reader["BookingId"]);
                    obj.RegionId = reader["RegionId"].ToString();
                    obj.ClassId = reader["ClassId"].ToString();
                    obj.FeeId = reader["FeeId"].ToString();
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
        public static List<BookingDetails> GetAll()
        {
            // create SELECT command
            string query =
                "SELECT BookingDetailId, ItineraryNo, TripStart, TripEnd, Description, Destination, BasePrice, AgencyCommission, BookingId, RegionId, ClassId, FeeId, ProductSupplierId " +
                "FROM BookingDetails ";
            SqlConnection connection = TravelExperts.GetConection();
            List<BookingDetails> dataList = new List<BookingDetails>(); // epmty list
            BookingDetails data; // for reading
                            // create connection



            SqlCommand cmd = new SqlCommand(query, connection);
            // open the conection
            connection.Open();

            // run the command
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // build object list to return
            while (reader.Read()) // if there is a object with this ID
            {
                data = new BookingDetails();
                data.BookingDetailId = Convert.ToInt32(reader["BookingDetailId"]);
                data.ItineraryNo = Convert.ToSingle(reader["ItineraryNo"]);
                data.TripStart = (DateTime)reader["TripStart"];
                data.TripEnd = (DateTime)reader["TripEnd"];
                data.Description = reader["Description"].ToString();
                data.Destination = reader["Destination"].ToString();
                data.BasePrice = Convert.ToDecimal(reader["BasePrice"]);
                data.AgencyCommission = Convert.ToDecimal(reader["AgencyCommission"]);
                data.BookingId = Convert.ToInt32(reader["BookingId"]);
                data.RegionId = reader["RegionId"].ToString();
                data.ClassId = reader["ClassId"].ToString();
                data.FeeId = reader["FeeId"].ToString();
                data.ProductSupplierId = Convert.ToInt32(reader["ProductSupplierId"]);
                dataList.Add(data);
            }

            return dataList;
        }
        #endregion

        #region Add
        // insert new row to table
        // return new object
        public static int Add(BookingDetails obj)
        {
            int custID = 0;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create INSERT command
            // CustomerID is IDENTITY so no value provided
            string insertStatment =
                "INSERT INTO BookingDetails(BookingDetailId, ItineraryNo, TripStart, TripEnd, Description, Destination, BasePrice, AgencyCommission, BookingId, RegionId, ClassId, FeeId, ProductSupplierId) " +
                "OUTPUT inserted.[BookingDetailId] " +
                "VALUES(@BookingDetailId, @ItineraryNo, @TripStart, @TripEnd, @Description, @Destination, @BasePrice, @AgencyCommission, @BookingId, @RegionId, @ClassId, @FeeId, @ProductSupplierId) ";
            SqlCommand cmd = new SqlCommand(insertStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@BookingDetailId", obj.BookingDetailId);
            cmd.Parameters.AddWithValue("@ItineraryNo", obj.ItineraryNo);
            cmd.Parameters.AddWithValue("@TripStart", obj.TripStart);
            cmd.Parameters.AddWithValue("@TripEnd", obj.TripEnd);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@Destination", obj.Destination);
            cmd.Parameters.AddWithValue("@BasePrice", obj.BasePrice);
            cmd.Parameters.AddWithValue("@AgencyCommission", obj.AgencyCommission);
            cmd.Parameters.AddWithValue("@BookingId", obj.BookingId);
            cmd.Parameters.AddWithValue("@RegionId", obj.RegionId);
            cmd.Parameters.AddWithValue("@ClassId", obj.ClassId);
            cmd.Parameters.AddWithValue("@FeeId", obj.FeeId);
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
        public static bool Delete(BookingDetails obj)
        {
            bool success = false;

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create DELETE command
            string deleteStatment =
                "DELETE FROM BookingDetails " +
                "WHERE BookingDetailId = @BookingDetailId " + // needed for identification of object
                "AND BookingDetailId = @BookingDetailId " + // the rest - for optimistic concurrency
                "AND TripStart = @TripStart " +
                "AND ItineraryNo = @ItineraryNo " +
                "AND Description = @Description " +
                "AND Destination = @Destination " +
                "AND BasePrice = @BasePrice " +
                "AND AgencyCommission = @AgencyCommission " +
                "AND BookingId = @BookingId " +
                "AND RegionId = @RegionId " +
                "AND ClassId = @ClassId " +
                "AND FeeId = @FeeId " +
                "AND ProductSupplierId = @ProductSupplierId ";
            SqlCommand cmd = new SqlCommand(deleteStatment, connection);
            // suply perameter value
            cmd.Parameters.AddWithValue("@BookingDetailId", obj.BookingDetailId);
            cmd.Parameters.AddWithValue("@ItineraryNo", obj.ItineraryNo);
            cmd.Parameters.AddWithValue("@TripStart", obj.TripStart);
            cmd.Parameters.AddWithValue("@TripEnd", obj.TripEnd);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@Destination", obj.Destination);
            cmd.Parameters.AddWithValue("@BasePrice", obj.BasePrice);
            cmd.Parameters.AddWithValue("@AgencyCommission", obj.AgencyCommission);
            cmd.Parameters.AddWithValue("@BookingId", obj.BookingId);
            cmd.Parameters.AddWithValue("@RegionId", obj.RegionId);
            cmd.Parameters.AddWithValue("@ClassId", obj.ClassId);
            cmd.Parameters.AddWithValue("@FeeId", obj.FeeId);
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
        public static bool Update(BookingDetails oldObj, BookingDetails newObj)
        {
            bool success = false; // did not update

            // create connection
            SqlConnection connection = TravelExperts.GetConection();

            // create UPDATE command
            string updateStatment =
                "UPDATE BookingDetails SET " +
                "BookingDetailId = @NewBookingDetailId, " +
                "ItineraryNo = @NewItineraryNo, " +
                "TripStart = @NewTripStart, " +
                "TripEnd = @NewTripEnd, " +
                "Description = @NewDescription, " +
                "Destination = @NewDestination, " +
                "BasePrice = @NewBasePrice, " +
                "BookingId = @NewBookingId, " +
                "RegionId = @NewRegionId, " +
                "ClassId = @NewClassId, " +
                "FeeId = @NewFeeId, " +
                "ProductSupplierId = @NewProductSupplierId " +
                "WHERE BookingDetailId = @OldBookingDetailId " + // identifies
                "AND ItineraryNo = @OldItineraryNo " + // the rest - for optimistic concurrency
                "AND TripStart = @OldTripStart " +
                "AND TripEnd = @OldTripEnd " +
                "AND Description = @OldDescription " +
                "AND Destination = @OldDestination " +
                "AND BasePrice = @OldBasePrice " +
                "AND AgencyCommission = @OldAgencyCommission " +
                "AND BookingId = @OldBookingId " +
                "AND RegionId = @OldRegionId " +
                "AND ClassId = @OldClassId " +
                "AND FeeId = @OldFeeId " +
                "AND ProductSupplierId = @OldProductSupplierId ";
            SqlCommand cmd = new SqlCommand(updateStatment, connection);
            // suply perameter value

            // New object Values
            cmd.Parameters.AddWithValue("@NewBookingDetailId", newObj.BookingDetailId);
            cmd.Parameters.AddWithValue("@NewItineraryNo", newObj.ItineraryNo);
            cmd.Parameters.AddWithValue("@NewTripStart", newObj.TripStart);
            cmd.Parameters.AddWithValue("@NewTripEnd", newObj.TripEnd);
            cmd.Parameters.AddWithValue("@NewDescription", newObj.Description);
            cmd.Parameters.AddWithValue("@NewDestination", newObj.Destination);
            cmd.Parameters.AddWithValue("@NewBasePrice", newObj.BasePrice);
            cmd.Parameters.AddWithValue("@NewAgencyCommission", newObj.AgencyCommission);
            cmd.Parameters.AddWithValue("@NewBookingId", newObj.BookingId);
            cmd.Parameters.AddWithValue("@NewRegionId", newObj.RegionId);
            cmd.Parameters.AddWithValue("@NewClassId", newObj.ClassId);
            cmd.Parameters.AddWithValue("@NewFeeId", newObj.FeeId);
            cmd.Parameters.AddWithValue("@NewProductSupplierId", newObj.ProductSupplierId);
            // ID
            cmd.Parameters.AddWithValue("@OldBookingDetailId", oldObj.BookingDetailId);
            // Old object Values
            cmd.Parameters.AddWithValue("@OldItineraryNo", oldObj.ItineraryNo);
            cmd.Parameters.AddWithValue("@OldTripStart", oldObj.TripStart);
            cmd.Parameters.AddWithValue("@OldTripEnd", oldObj.TripEnd);
            cmd.Parameters.AddWithValue("@OldDescription", oldObj.Description);
            cmd.Parameters.AddWithValue("@OldDestination", oldObj.Destination);
            cmd.Parameters.AddWithValue("@OldBasePrice", oldObj.BasePrice);
            cmd.Parameters.AddWithValue("@OldAgencyCommission", oldObj.AgencyCommission);
            cmd.Parameters.AddWithValue("@OldBookingId", oldObj.BookingId);
            cmd.Parameters.AddWithValue("@OldRegionId", oldObj.RegionId);
            cmd.Parameters.AddWithValue("@OldClassId", oldObj.ClassId);
            cmd.Parameters.AddWithValue("@OldFeeId", oldObj.FeeId);
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
