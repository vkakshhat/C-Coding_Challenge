//using System;
//using System.Data.SqlClient;
//using Microsoft.Data.SqlClient;

//namespace UtilLibrary
//{
//    public class DBConnection
//    {
//        static DBConnection()
//        {
//            _cn = "Data Source=AK\\SQLEXPRESS;Initial Catalog=CareerHub;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
//        }
//        private static string _cn;

//        public static string ConnectionString
//        {
//            get { return _cn; }
//            private set
//            {
//                _cn = value;
//            }
//        }

//        public static SqlConnection ReturnConnection()
//        {
//            SqlConnection connection = new SqlConnection(ConnectionString);
//            return connection;
//        }
//    }
//}
