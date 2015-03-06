using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Helper
{
    /// <summary>
    /// Summary description for DatabaseHelper
    /// </summary>
    public static class DatabaseHelper
    {
        private static SqlConnection _cnn;

        /// <summary>
        /// Setup connection to database
        /// </summary>
        /// <returns>SqlConnection object</returns>
        public static SqlConnection Connect()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["HabecoConnectionString"].ConnectionString;
            _cnn = new SqlConnection(connectionString);
            try
            {
                _cnn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _cnn;
        }

        /// <summary>
        /// Disconnection to database
        /// </summary>
        /// <param name="cnn">SqlConnection object</param>
        public static void Disconnect(SqlConnection cnn)
        {
            try
            {
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Check data exist in database or not
        /// </summary>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="sqlQuery">SQL query</param>
        /// <param name="param">Param list</param>
        /// <returns>Bool type</returns>
        public static bool ExistDataFromDb(SqlConnection conn, string sqlQuery, Dictionary<string, string> param)
        {
            using (var command = new SqlCommand(sqlQuery, conn))
            {
                foreach (var pair in param)
                {
                    command.Parameters.AddWithValue(pair.Key, pair.Value);
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        return true;
                }
            }
            return false;
        }
    }
}
