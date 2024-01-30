using System.Data.SqlClient;
using EyeHistoria.Models;

namespace EyeHistoria.DAL
{
    public class ProcessDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public ProcessDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "ProcessConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }
        public int Add(Demographic demographic)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Demographic (p_name, p_race, p_age, p_occupation, p_gender) 
                                    OUTPUT INSERTED.p_id
                                    VALUES(@p_name, @p_race, @p_age, @p_occupation, @p_gender)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@p_name", demographic.p_name);
            cmd.Parameters.AddWithValue("@p_race", demographic.p_race);
            cmd.Parameters.AddWithValue("@p_age", demographic.p_age);
            cmd.Parameters.AddWithValue("@p_occupation", demographic.p_occupation);
            cmd.Parameters.AddWithValue("@p_gender", demographic.p_gender);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            demographic.p_id = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return demographic.p_id;
        }
    }
}
