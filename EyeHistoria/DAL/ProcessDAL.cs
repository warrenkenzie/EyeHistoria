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

        public int Add5(PersonalHealthHistory personalHealthHistory)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO PersonalHealthHistory (p_id, p_allergy, p_medication, p_hcondition, all_type, med_type, hcon_type, med_sdate, hcon_sdate, med_edate, hcon_edate) 
                        OUTPUT INSERTED.p_id
                        VALUES(@p_id, @p_allergy, @p_medication, @p_hcondition, @all_type, @med_type, @hcon_type, @med_sdate, @hcon_sdate, @med_edate, @hcon_edate)";
            cmd.Parameters.AddWithValue("@p_id", personalHealthHistory.p_id);
            cmd.Parameters.AddWithValue("@p_allergy", personalHealthHistory.p_allergy);
            cmd.Parameters.AddWithValue("@p_medication", personalHealthHistory.p_medication);
            cmd.Parameters.AddWithValue("@p_hcondition", personalHealthHistory.p_hcondition);
            cmd.Parameters.AddWithValue("@all_type", personalHealthHistory.all_type);
            cmd.Parameters.AddWithValue("@med_type", personalHealthHistory.med_type);
            cmd.Parameters.AddWithValue("@hcon_type", personalHealthHistory.hcon_type);
            cmd.Parameters.AddWithValue("@med_sdate", personalHealthHistory.med_sdate);
            cmd.Parameters.AddWithValue("@hcon_sdate", personalHealthHistory.hcon_sdate);
            cmd.Parameters.AddWithValue("@med_edate", personalHealthHistory.med_edate);
            cmd.Parameters.AddWithValue("@hcon_edate", personalHealthHistory.hcon_edate);

            conn.Open();
            personalHealthHistory.p_id = (int)cmd.ExecuteScalar();
            conn.Close();

            return personalHealthHistory.p_id;
        }

        public int Add6(FamilyHealthHistory familyHealthHistory)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO FamilyHealthHistory (p_id, pf_hdiseases, hdis_type, hdis_member) 
                        OUTPUT INSERTED.p_id
                        VALUES(@p_id, @pf_hdiseases, @hdis_type, @hdis_member)";
            cmd.Parameters.AddWithValue("@p_id", familyHealthHistory.p_id);
            cmd.Parameters.AddWithValue("@pf_hdiseases", familyHealthHistory.pf_hdiseases);
            cmd.Parameters.AddWithValue("@hdis_type", familyHealthHistory.hdis_type);
            cmd.Parameters.AddWithValue("@hdis_member", familyHealthHistory.hdis_member);

            conn.Open();
            familyHealthHistory.p_id = (int)cmd.ExecuteScalar();
            conn.Close();

            return familyHealthHistory.p_id;
        }

        public int Add7(Habits habits)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Habits (p_id, p_smoke, p_drink, s_freq, d_freq, p_game, p_read, g_freq, r_freq) 
                        OUTPUT INSERTED.p_id
                        VALUES(@p_id, @p_smoke, @p_drink, @s_freq, @d_freq, @p_game, @p_read, @g_freq, @r_freq)";
            cmd.Parameters.AddWithValue("@p_id", habits.p_id);
            cmd.Parameters.AddWithValue("@p_smoke", habits.p_smoke);
            cmd.Parameters.AddWithValue("@p_drink", habits.p_drink);
            cmd.Parameters.AddWithValue("@s_freq", habits.s_freq);
            cmd.Parameters.AddWithValue("@d_freq", habits.d_freq);
            cmd.Parameters.AddWithValue("@p_game", habits.p_game);
            cmd.Parameters.AddWithValue("@p_read", habits.p_read);
            cmd.Parameters.AddWithValue("@g_freq", habits.g_freq);
            cmd.Parameters.AddWithValue("@r_freq", habits.r_freq);

            conn.Open();
            habits.p_id = (int)cmd.ExecuteScalar();
            conn.Close();

            return habits.p_id;
        }
    }
}
