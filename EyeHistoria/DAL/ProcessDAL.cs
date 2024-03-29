﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Diagnostics.SymbolStore;
using EyeHistoria.Models;
using Humanizer;
using MessagePack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;

namespace EyeHistoria.DAL
{
    public class ProcessDAL
    {
        private IConfiguration Configuration { get; set; }
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

            conn.Open();
            

            cmd.CommandText = @"INSERT INTO Demographic (p_name, p_race, p_age, p_occupation, p_gender)
                                    OUTPUT INSERTED.p_id
                                    VALUES(@p_name, @p_race, @p_age, @p_occupation, @p_gender)";
      
            cmd.Parameters.AddWithValue("@p_name", demographic.p_name);
            cmd.Parameters.AddWithValue("@p_race", demographic.p_race);
            cmd.Parameters.AddWithValue("@p_age", demographic.p_age);
            cmd.Parameters.AddWithValue("@p_occupation", demographic.p_occupation);
            cmd.Parameters.AddWithValue("@p_gender", demographic.p_gender);

            
            demographic.p_id = (int)cmd.ExecuteScalar();

            conn.Close();

            return demographic.p_id;
        }

        public int Add2(ChiefComplaint chiefcomplaint)
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT ChiefComplaint ON";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"INSERT INTO ChiefComplaint (p_id,p_chiefcomplaint) 
                                VALUES(@p_id,@p_chiefcomplaint)";
            cmd.Parameters.AddWithValue("@p_id", chiefcomplaint.p_id);
            cmd.Parameters.AddWithValue("@p_chiefcomplaint", chiefcomplaint.p_chiefcomplaint);

            cmd.ExecuteNonQuery();

            // Disable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT Demographic OFF";
            cmd.ExecuteNonQuery();
            conn.Close();

            return chiefcomplaint.p_id;
        }

        public int Add3(PersonalOcularHistory personalOcularHistory)
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT PersonalOcularHistory ON";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"INSERT INTO PersonalOcularHistory (p_id, p_prescription, p_procedure, p_condition, pre_type, pro_type, con_type, pre_sdate, pro_sdate, con_sdate, pre_edate, pro_edate, con_edate) 
                    VALUES(@p_id, @p_prescription, @p_procedure, @p_condition, @pre_type, @pro_type, @con_type, @pre_sdate, @pro_sdate, @con_sdate, @pre_edate, @pro_edate, @con_edate)";
            cmd.Parameters.AddWithValue("@p_id", personalOcularHistory.p_id);
            cmd.Parameters.AddWithValue("@p_prescription", personalOcularHistory.p_prescription);
            cmd.Parameters.AddWithValue("@p_procedure", personalOcularHistory.p_procedure);
            cmd.Parameters.AddWithValue("@p_condition", personalOcularHistory.p_condition);
            cmd.Parameters.AddWithValue("@pre_type", personalOcularHistory.pre_type != null ? personalOcularHistory.pre_type : DBNull.Value);
            cmd.Parameters.AddWithValue("@pro_type", personalOcularHistory.pro_type != null ? personalOcularHistory.pro_type : DBNull.Value);
            cmd.Parameters.AddWithValue("@con_type", personalOcularHistory.con_type != null ? personalOcularHistory.con_type : DBNull.Value);
            cmd.Parameters.AddWithValue("@pre_sdate", personalOcularHistory.pre_sdate != null ? personalOcularHistory.pre_sdate : DBNull.Value);
            cmd.Parameters.AddWithValue("@pro_sdate", personalOcularHistory.pro_sdate != null ? personalOcularHistory.pro_sdate : DBNull.Value);
            cmd.Parameters.AddWithValue("@con_sdate", personalOcularHistory.con_sdate != null ? personalOcularHistory.con_sdate : DBNull.Value);
            cmd.Parameters.AddWithValue("@pre_edate", personalOcularHistory.pre_edate != null ? personalOcularHistory.pre_edate : DBNull.Value);
            cmd.Parameters.AddWithValue("@pro_edate", personalOcularHistory.pro_edate != null ? personalOcularHistory.pro_edate : DBNull.Value);
            cmd.Parameters.AddWithValue("@con_edate", personalOcularHistory.con_edate != null ? personalOcularHistory.con_edate : DBNull.Value);

            cmd.ExecuteNonQuery();

            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT PersonalOcularHistory OFF";
            cmd.ExecuteNonQuery();
            conn.Close();

            return personalOcularHistory.p_id;
        }

        public int Add4(FamilyOcularHistory familyOcularHistory)
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();

            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT FamilyOcularHistory ON";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"INSERT INTO FamilyOcularHistory (p_id, pf_diseases, dis_type, dis_member) 
                                VALUES(@p_id, @pf_diseases, @dis_type, @dis_member)";
            cmd.Parameters.AddWithValue("@p_id", familyOcularHistory.p_id);
            cmd.Parameters.AddWithValue("@pf_diseases", familyOcularHistory.pf_diseases);
            cmd.Parameters.AddWithValue("@dis_type", familyOcularHistory.dis_type != null ? familyOcularHistory.dis_type : DBNull.Value);
            cmd.Parameters.AddWithValue("@dis_member", familyOcularHistory.dis_member != null ? familyOcularHistory.dis_member : DBNull.Value);

            cmd.ExecuteNonQuery();

            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT FamilyOcularHistory OFF";
            cmd.ExecuteNonQuery();
            conn.Close();

            return familyOcularHistory.p_id;
        }

        public int Add5(PersonalHealthHistory personalHealthHistory)
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();

            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT PersonalHealthHistory ON";
            cmd.ExecuteNonQuery();


            cmd.CommandText = @"INSERT INTO PersonalHealthHistory (p_id, p_allergy, p_medication, p_hcondition, all_type, med_type, hcon_type, med_sdate, hcon_sdate, med_edate, hcon_edate) 
                        VALUES(@p_id, @p_allergy, @p_medication, @p_hcondition, @all_type, @med_type, @hcon_type, @med_sdate, @hcon_sdate, @med_edate, @hcon_edate)";
            cmd.Parameters.AddWithValue("@p_id", personalHealthHistory.p_id);
            cmd.Parameters.AddWithValue("@p_allergy", personalHealthHistory.p_allergy != null ? personalHealthHistory.p_allergy : DBNull.Value);
            cmd.Parameters.AddWithValue("@p_medication", personalHealthHistory.p_medication != null ? personalHealthHistory.p_medication : DBNull.Value);
            cmd.Parameters.AddWithValue("@p_hcondition", personalHealthHistory.p_hcondition != null ? personalHealthHistory.p_hcondition : DBNull.Value);
            cmd.Parameters.AddWithValue("@all_type", personalHealthHistory.all_type != null ? personalHealthHistory.all_type : DBNull.Value);
            cmd.Parameters.AddWithValue("@med_type", personalHealthHistory.med_type != null ? personalHealthHistory.med_type : DBNull.Value);
            cmd.Parameters.AddWithValue("@hcon_type", personalHealthHistory.hcon_type != null ? personalHealthHistory.hcon_type : DBNull.Value);
            cmd.Parameters.AddWithValue("@med_sdate", personalHealthHistory.med_sdate != null ? personalHealthHistory.med_sdate : DBNull.Value);
            cmd.Parameters.AddWithValue("@hcon_sdate", personalHealthHistory.hcon_sdate != null ? personalHealthHistory.hcon_sdate : DBNull.Value);
            cmd.Parameters.AddWithValue("@med_edate", personalHealthHistory.med_edate != null ? personalHealthHistory.med_edate : DBNull.Value);
            cmd.Parameters.AddWithValue("@hcon_edate", personalHealthHistory.hcon_edate != null ? personalHealthHistory.hcon_edate : DBNull.Value );


            cmd.ExecuteNonQuery();

            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT PersonalHealthHistory OFF";
            cmd.ExecuteNonQuery();

            conn.Close();

            return personalHealthHistory.p_id;
        }

        public int Add6(FamilyHealthHistory familyHealthHistory)
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT FamilyHealthHistory ON";
            cmd.ExecuteNonQuery();


            cmd.CommandText = @"INSERT INTO FamilyHealthHistory (p_id, pf_hdiseases, hdis_type, hdis_member) 
                        VALUES(@p_id, @pf_hdiseases, @hdis_type, @hdis_member)";
            cmd.Parameters.AddWithValue("@p_id", familyHealthHistory.p_id);
            cmd.Parameters.AddWithValue("@pf_hdiseases", familyHealthHistory.pf_hdiseases != null ? familyHealthHistory.pf_hdiseases : DBNull.Value);
            cmd.Parameters.AddWithValue("@hdis_type", familyHealthHistory.hdis_type != null ? familyHealthHistory.hdis_type : DBNull.Value);
            cmd.Parameters.AddWithValue("@hdis_member", familyHealthHistory.hdis_member != null ? familyHealthHistory.hdis_member : DBNull.Value);

            cmd.ExecuteNonQuery();

            // Enable identity insert
            cmd.CommandText = "SET IDENTITY_INSERT FamilyHealthHistory OFF";
            cmd.ExecuteNonQuery();
            conn.Close();

            return familyHealthHistory.p_id;
        }

        public int Add7(Habits habits)
        {
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = "SET IDENTITY_INSERT Habits ON";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"INSERT INTO Habits (p_id, p_smoke, p_drink, s_freq, d_freq, p_game, p_read, g_freq, r_freq) 
                        VALUES(@p_id, @p_smoke, @p_drink, @s_freq, @d_freq, @p_game, @p_read, @g_freq, @r_freq)";
            cmd.Parameters.AddWithValue("@p_id", habits.p_id);
            cmd.Parameters.AddWithValue("@p_smoke", habits.p_smoke);
            cmd.Parameters.AddWithValue("@p_drink", habits.p_drink);
            cmd.Parameters.AddWithValue("@s_freq", habits.s_freq != null ? habits.s_freq : DBNull.Value);
            cmd.Parameters.AddWithValue("@d_freq", habits.d_freq != null ? habits.d_freq : DBNull.Value);
            cmd.Parameters.AddWithValue("@p_game", habits.p_game != null ? habits.p_game : DBNull.Value);
            cmd.Parameters.AddWithValue("@p_read", habits.p_read != null ? habits.p_read : DBNull.Value);
            cmd.Parameters.AddWithValue("@g_freq", habits.g_freq != null ? habits.g_freq : DBNull.Value);
            cmd.Parameters.AddWithValue("@r_freq", habits.r_freq != null ? habits.r_freq : DBNull.Value);

            cmd.ExecuteNonQuery();

            cmd.CommandText = "SET IDENTITY_INSERT Habits OFF";
            cmd.ExecuteNonQuery();
            conn.Close();

            return habits.p_id;
        }


        // Retrieving based on p_id (patient id)
        public Demographic GetPatient_Demographic(int p_id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Demographic WHERE p_id = @p_id";

            cmd.Parameters.AddWithValue("@p_id", p_id);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            Demographic demographic = null;
            if (reader.Read())
            {
                demographic = new Demographic
                {
                    p_id = reader.GetInt32(0),
                    p_name = reader.GetString(1),
                    p_race = reader.GetString(2),
                    p_age = reader.GetInt32(3),
                    p_occupation = reader.GetString(4),
                    p_gender = reader.GetString(5)
                };

            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return demographic;
        }

        // retrieves chiefcomplaint based on patient id
        public ChiefComplaint GetPatient_ChiefComplaint(int p_id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM ChiefComplaint WHERE p_id = @p_id";

            cmd.Parameters.AddWithValue("@p_id", p_id);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            ChiefComplaint chiefComplaint = null;
            if (reader.Read())
            {
                chiefComplaint = new ChiefComplaint
                {
                    p_id = reader.GetInt32(0),
                    p_chiefcomplaint = reader.GetString(1)
                };

            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return chiefComplaint;
        }

        // retrieves PersonalOcularHistory based on p_id
        // NOTE: I am only getting the first 4 columns from the table
        public PersonalOcularHistory GetPatient_PersonalOcularHistory(int p_id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM PersonalOcularHistory WHERE p_id = @p_id";

            cmd.Parameters.AddWithValue("@p_id", p_id);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            PersonalOcularHistory personalOcularHistory = null;
            if (reader.Read())
            {
                personalOcularHistory = new PersonalOcularHistory
                {
                    p_id = reader.GetInt32(0),
                    p_prescription = reader.GetString(1),
                    p_procedure = reader.GetString(2),
                    p_condition = reader.GetString(3),
                    pre_type = (!reader.IsDBNull(4) ? reader.GetString(4) : null),
                    pro_type = (!reader.IsDBNull(5) ? reader.GetString(5) : null),
                    con_type = (!reader.IsDBNull(6) ? reader.GetString(6) : null),
                    pre_sdate = (!reader.IsDBNull(7) ? reader.GetDateTime(7) : null),
                    pro_sdate = (!reader.IsDBNull(8) ? reader.GetDateTime(8) : null),
                    con_sdate = (!reader.IsDBNull(9) ? reader.GetDateTime(9) : null),
                    pre_edate = (!reader.IsDBNull(10) ? reader.GetDateTime(10) : null),
                    pro_edate = (!reader.IsDBNull(11) ? reader.GetDateTime(11) : null),
                    con_edate = (!reader.IsDBNull(12) ? reader.GetDateTime(12) : null)

                };

            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return personalOcularHistory;
        }

        // gets family ocular history based on p_id
        public FamilyOcularHistory GetPatient_FamilyOcularHistory(int p_id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM FamilyOcularHistory WHERE p_id = @p_id";

            cmd.Parameters.AddWithValue("@p_id", p_id);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            FamilyOcularHistory familyOcularHistory = null;
            if (reader.Read())
            {
                familyOcularHistory = new FamilyOcularHistory
                {
                    p_id = reader.GetInt32(0),
                    pf_diseases = reader.GetString(1),
                    dis_type = (!reader.IsDBNull(2) ? reader.GetString(2) : null),
                    dis_member = (!reader.IsDBNull(3) ? reader.GetString(3) : null)
                };

            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return familyOcularHistory;
        }

        // gets the personalhealthhistory based on p_id only take first 7 columns
        public PersonalHealthHistory GetPatient_PersonalHealthHistory(int p_id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM PersonalHealthHistory WHERE p_id = @p_id";

            cmd.Parameters.AddWithValue("@p_id", p_id);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            PersonalHealthHistory personalHealthHistory = null;
            if (reader.Read())
            {
                personalHealthHistory = new PersonalHealthHistory
                {
                    p_id = reader.GetInt32(0),
                    p_allergy = reader.GetString(1),
                    p_medication = reader.GetString(2),
                    p_hcondition = reader.GetString(3),
                    all_type = (!reader.IsDBNull(4) ? reader.GetString(4) : null),
                    med_type = (!reader.IsDBNull(5) ? reader.GetString(5) : null),
                    hcon_type = (!reader.IsDBNull(6) ? reader.GetString(6) : null),
                    med_sdate = (!reader.IsDBNull(7) ? reader.GetDateTime(7) : null),
                    hcon_sdate = (!reader.IsDBNull(8) ? reader.GetDateTime(8) : null),
                    med_edate = (!reader.IsDBNull(9) ? reader.GetDateTime(9) : null),
                    hcon_edate = (!reader.IsDBNull(10) ? reader.GetDateTime(10) : null)
                };

            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return personalHealthHistory;
        }
        public FamilyHealthHistory GetPatient_FamilyHealthHistory(int p_id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM FamilyHealthHistory WHERE p_id = @p_id";

            cmd.Parameters.AddWithValue("@p_id", p_id);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            FamilyHealthHistory familyHealthHistory = null;
            if (reader.Read())
            {
                familyHealthHistory = new FamilyHealthHistory
                {
                    p_id = reader.GetInt32(0),
                    pf_hdiseases = reader.GetString(1),
                    hdis_type = (!reader.IsDBNull(2) ? reader.GetString(2) : null),
                    hdis_member = (!reader.IsDBNull(3) ? reader.GetString(3) : null)
                };

            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return familyHealthHistory;
        }

        // get habits based on p_id
        // NOTE: only get first 5 columns
        public Habits GetPatient_Habits(int p_id)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Habits WHERE p_id = @p_id";

            cmd.Parameters.AddWithValue("@p_id", p_id);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            Habits habits = null;
            if (reader.Read())
            {
                habits = new Habits
                {
                    p_id = reader.GetInt32(0),
                    p_smoke = reader.GetString(1),
                    p_drink = reader.GetString(2),
                    s_freq = (!reader.IsDBNull(3) ? reader.GetInt32(3) : null),
                    d_freq = (!reader.IsDBNull(4) ? reader.GetInt32(4) : null),
                    p_game = reader.GetString(5),
                    p_read = reader.GetString(6),
                    g_freq = (!reader.IsDBNull(7) ? reader.GetInt32(7) : null),
                    r_freq = (!reader.IsDBNull(8) ? reader.GetInt32(8) : null)
                };

            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return habits;
        }

    }
}
