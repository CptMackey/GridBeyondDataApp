using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace GridBeyondDataApp
{
    public class DatabaseAccess
    {
        public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GridBeyondDB.mdf;Integrated Security=True";

        public static bool InsertValues(string[] fields)
        {
            try
            {
                var insertRowQuery = "INSERT INTO dbo.MarketPrice(Date, Market_Price_EX1) VALUES(@date, @marketPrice)";
                // Don't insert if Date is invalid
                if (DateTime.TryParse(fields[0], out DateTime dateTime))
                {
                    decimal.TryParse(fields[1], out decimal marketPrice);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(insertRowQuery, connection))
                        {
                            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = dateTime;
                            cmd.Parameters.Add("@marketPrice", SqlDbType.BigInt).Value = marketPrice;
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static DataTable GetData(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    DataTable dataTable = new DataTable();
                    sda.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }
}
