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
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Demographic (p_name, p_race, p_age, p_occupation, p_gender) 
                                OUTPUT INSERTED.p_id
                                VALUES(@p_name, @p_race, @p_age, @p_occupation, @p_gender)";
            cmd.Parameters.AddWithValue("@p_name", demographic.p_name);
            cmd.Parameters.AddWithValue("@p_race", demographic.p_race);
            cmd.Parameters.AddWithValue("@p_age", demographic.p_age);
            cmd.Parameters.AddWithValue("@p_occupation", demographic.p_occupation);
            cmd.Parameters.AddWithValue("@p_gender", demographic.p_gender);

            conn.Open();
            demographic.p_id = (int)cmd.ExecuteScalar();
            conn.Close();

            return demographic.p_id;
        }

        public int Add2(ChiefComplaint chiefcomplaint)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO ChiefComplaint (p_chief_complaint) 
                                OUTPUT INSERTED.p_id
                                VALUES(@p_chiefcomplaint)";
            cmd.Parameters.AddWithValue("@p_chiefcomplaint", chiefcomplaint.p_chiefcomplaint);

            conn.Open();
            chiefcomplaint.p_id = (int)cmd.ExecuteScalar();
            conn.Close();

            return chiefcomplaint.p_id;
        }

        public int Add3(PersonalOcularHistory personalOcularHistory)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO PersonalOcularHistory (p_id, p_prescription, p_procedure, p_condition, pre_type, pro_type, con_type, pre_sdate, pro_sdate, con_sdate, pre_edate, pro_edate, con_edate) 
                        OUTPUT INSERTED.p_id
                        VALUES(@p_id, @p_prescription, @p_procedure, @p_condition, @pre_type, @pro_type, @con_type, @pre_sdate, @pro_sdate, @con_sdate, @pre_edate, @pro_edate, @con_edate)";
            cmd.Parameters.AddWithValue("@p_id", personalOcularHistory.p_id);
            cmd.Parameters.AddWithValue("@p_prescription", personalOcularHistory.p_prescription);
            cmd.Parameters.AddWithValue("@p_procedure", personalOcularHistory.p_procedure);
            cmd.Parameters.AddWithValue("@p_condition", personalOcularHistory.p_condition);
            cmd.Parameters.AddWithValue("@pre_type", personalOcularHistory.pre_type);
            cmd.Parameters.AddWithValue("@pro_type", personalOcularHistory.pro_type);
            cmd.Parameters.AddWithValue("@con_type", personalOcularHistory.con_type);
            cmd.Parameters.AddWithValue("@pre_sdate", personalOcularHistory.pre_sdate);
            cmd.Parameters.AddWithValue("@pro_sdate", personalOcularHistory.pro_sdate);
            cmd.Parameters.AddWithValue("@con_sdate", personalOcularHistory.con_sdate);
            cmd.Parameters.AddWithValue("@pre_edate", personalOcularHistory.pre_edate);
            cmd.Parameters.AddWithValue("@pro_edate", personalOcularHistory.pro_edate);
            cmd.Parameters.AddWithValue("@con_edate", personalOcularHistory.con_edate);

            conn.Open();
            personalOcularHistory.p_id = (int)cmd.ExecuteScalar();
            conn.Close();

            return personalOcularHistory.p_id;
        }

        public int Add4(FamilyOcularHistory familyOcularHistory)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO FamilyOcularHistory (p_id, pf_diseases, dis_type, dis_member) 
                                OUTPUT INSERTED.p_id
                                VALUES(@p_id, @pf_diseases, @dis_type, @dis_member)";
            cmd.Parameters.AddWithValue("@p_id", familyOcularHistory.p_id);
            cmd.Parameters.AddWithValue("@pf_diseases", familyOcularHistory.pf_diseases);
            cmd.Parameters.AddWithValue("@dis_type", familyOcularHistory.dis_type);
            cmd.Parameters.AddWithValue("@dis_member", familyOcularHistory.dis_member);

            conn.Open();
            familyOcularHistory.p_id = (int)cmd.ExecuteScalar();
            conn.Close();

            return familyOcularHistory.p_id;
        }
    }
}
