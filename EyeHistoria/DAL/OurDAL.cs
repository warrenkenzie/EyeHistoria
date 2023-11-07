using System.Collections;
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
    public class OurDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public OurDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "EyeHistoriaConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }

        public List<Symptoms> GetAllSymptoms()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement 
            cmd.CommandText = @"SELECT * FROM Symptoms";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a symptoms list
            List<Symptoms> symptomsList = new List<Symptoms>();
            while (reader.Read())
            {
                symptomsList.Add(
                new Symptoms
                {
                    SymptomID = reader.GetInt32(0), //0: 1st column
                    SymptomName = reader.GetString(1), //1: 2nd column
                    AdminID = reader.GetInt32(2), //2: 3rd column
                    LastModifiedBy = reader.GetString(3), //3: 4th column
                    Date = reader.GetDateTime(4) //4: 5th column
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return symptomsList;
        }

        public List<Question> get_question_basedon_type(string question_type)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Questions WHERE Type= @question_type AND Category = 'General'";
            
            // parameteres
            cmd.Parameters.AddWithValue("@question_type", question_type);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Question> list_of_questions = new List<Question>();
            while (reader.Read())
            {
                list_of_questions.Add(
                    new Question()
                    {
                        QuestionID = reader.GetInt32(0),
                        QuestionText = reader.GetString(1),
                        Type = reader.GetString(2),
                        Category = reader.GetString(3),
                        SymptomID = reader.GetInt32(4),
                        SymptomName = reader.GetString(5),
                        AdminID = reader.GetInt32(6),
                        LastModifiedBy = reader.GetString(7),
                        Date = reader.GetDateTime(8),
                        FollowUpID = (!reader.IsDBNull(9) ? reader.GetInt32(9) : null)
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return list_of_questions;
        }

        public List<Diagnosis> Get_Diagnostics()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Diagnosis";

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Diagnosis> list_of_diagnostic = new List<Diagnosis>();
            while (reader.Read())
            {
                list_of_diagnostic.Add(
                    new Diagnosis()
                    {
                        DiagnosisID = reader.GetInt32(0),
                        DiagnosisName = reader.GetString(1),
                        List_of_diagnosis_symptoms = reader.GetString(2).Split(',').Select(symptom => symptom.Trim()).ToList(),
                        AdminID = reader.GetInt32(3),
                        LastModifiedBy = reader.GetString(4),
                        Date = reader.GetDateTime(5)
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return list_of_diagnostic;
        }
        public int AddDiagnosis(Diagnosis diagnosis)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Diagnosis (DiagnosisName, Symptoms, AdminID, 
                                LastModifiedBy, Date) 
                                OUTPUT INSERTED.DiagnosisID 
                                VALUES(@diagnosisname, @symptoms, @adminid, @lastmodifiedby, @date)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@diagnosisname", diagnosis.DiagnosisName);
            string str_List_of_diagnosis_symptoms = "";
            for (int i = 0; i < diagnosis.List_of_diagnosis_symptoms.Count(); i++)
            {
                if(i != (diagnosis.List_of_diagnosis_symptoms.Count() - 1))
                {
                    str_List_of_diagnosis_symptoms += diagnosis.List_of_diagnosis_symptoms[i] + ",";
                }
                else
                {
                    str_List_of_diagnosis_symptoms += diagnosis.List_of_diagnosis_symptoms[i];
                }
            }    
            cmd.Parameters.AddWithValue("@symptoms", str_List_of_diagnosis_symptoms);
            cmd.Parameters.AddWithValue("@adminid", diagnosis.AdminID);
            cmd.Parameters.AddWithValue("@lastmodifiedby", diagnosis.LastModifiedBy);
            cmd.Parameters.AddWithValue("@date", DateTime.Today);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            diagnosis.DiagnosisID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return diagnosis.DiagnosisID;
        }

        public bool IsSymptomExist(string symptomName, int symptomID)
        {
            bool symptomFound = false;
            //Create a SqlCommand object and specify the SQL statement 
            //to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT SymptomID FROM Symptoms 
                                WHERE SymptomName=@selectedSymptomName";
            cmd.Parameters.AddWithValue("@selectedSymptomName", symptomName);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != symptomID)
                        //The email address is used by another staff
                        symptomFound = true;
                    else
                        symptomFound = false;
                }
            }
            else
            { //No record
                symptomFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();
            return symptomFound;
        }

        public void AddSymptomsWithQuestionsTemplateOne(Symptoms symptoms)
        {
            using (SqlCommand command = new SqlCommand("AddSymptomsWithQuestionsTemplateOne", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                // Add parameters to the stored procedure
                command.Parameters.AddWithValue("@SymptomName", symptoms.SymptomName);
                command.Parameters.AddWithValue("@AdminID", symptoms.AdminID);
                command.Parameters.AddWithValue("@LastModifiedBy", symptoms.LastModifiedBy);
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Stored procedure executed successfully.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        // This Symptom already exists in the database.
                        Console.WriteLine("This Symptom already exists in the database.");
                    }
                    else
                    {
                        // Handle other SQL errors
                        Console.WriteLine("An error occurred while executing the stored procedure.");
                    }
                }
            }
        }

        public void AddSymptomsWithQuestionsTemplateTwo(Symptoms symptoms)
        {
            using (SqlCommand command = new SqlCommand("AddSymptomsWithQuestionsTemplateTwo", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                // Add parameters to the stored procedure
                command.Parameters.AddWithValue("@SymptomName", symptoms.SymptomName);
                command.Parameters.AddWithValue("@AdminID", symptoms.AdminID);
                command.Parameters.AddWithValue("@LastModifiedBy", symptoms.LastModifiedBy);
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Stored procedure executed successfully.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        // This Symptom already exists in the database.
                        Console.WriteLine("This Symptom already exists in the database.");
                    }
                    else
                    {
                        // Handle other SQL errors
                        Console.WriteLine("An error occurred while executing the stored procedure.");
                    }
                }
            }
        }

        public void AddSymptomsWithQuestionsTemplateThree(Symptoms symptoms)
        {
            using (SqlCommand command = new SqlCommand("AddSymptomsWithQuestionsTemplateThree", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                // Add parameters to the stored procedure
                command.Parameters.AddWithValue("@SymptomName", symptoms.SymptomName);
                command.Parameters.AddWithValue("@AdminID", symptoms.AdminID);
                command.Parameters.AddWithValue("@LastModifiedBy", symptoms.LastModifiedBy);
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Stored procedure executed successfully.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        // This Symptom already exists in the database.
                        Console.WriteLine("This Symptom already exists in the database.");
                    }
                    else
                    {
                        // Handle other SQL errors
                        Console.WriteLine("An error occurred while executing the stored procedure.");
                    }
                }
            }
        }

        public List<Question> GetQuestions()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Questions";

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Question> questionlist = new List<Question>();
            while (reader.Read())
            {
                questionlist.Add(
                    new Question()
                    {
                        QuestionID = reader.GetInt32(0),
                        QuestionText = reader.GetString(1),
                        Type = reader.GetString(2),
                        Category = reader.GetString(3),
                        SymptomID = reader.GetInt32(4),
                        SymptomName = reader.GetString(5),
                        AdminID = reader.GetInt32(6),
                        LastModifiedBy = reader.GetString(7),
                        Date = reader.GetDateTime(8),
                        FollowUpID = !reader.IsDBNull(9) ?
                                      reader.GetInt32(9) : (int?)null

                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return questionlist;
        }

        public int AddQuestion(Question question)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Questions (Question, Type, Category, SymptomID,
                                SymptomName, AdminID, LastModifiedBy, Date, FollowupID)
                                OUTPUT INSERTED.QuestionID
                                VALUES(@question, @type, @category, @symptomid, @symptomname, @adminid, @lastmodifiedby, @date, @followupid)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@question", question.QuestionText);
            cmd.Parameters.AddWithValue("@type", question.Type);
            cmd.Parameters.AddWithValue("@category", question.Category);
            cmd.Parameters.AddWithValue("@symptomid", question.SymptomID);
            cmd.Parameters.AddWithValue("@symptomname", question.SymptomName);
            cmd.Parameters.AddWithValue("@adminid", question.AdminID);
            cmd.Parameters.AddWithValue("@lastmodifiedby", question.LastModifiedBy);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);

            if (question.FollowUpID != 0)
            {
                cmd.Parameters.AddWithValue("@followupid", question.FollowUpID.Value);
            }

            else 
            {
                cmd.Parameters.AddWithValue("@followupid", DBNull.Value);
            }
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            question.QuestionID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return question.QuestionID;
        }

        public int UpdateQuestion(Question question)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Questions SET Question=@question, 
                                type=@type, catregory=@category 
                                WHERE QuestionID = @selectedQuestionID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@question",question.QuestionText);
            cmd.Parameters.AddWithValue("@type", question.Type);
            cmd.Parameters.AddWithValue("@category", question.Category);
            cmd.Parameters.AddWithValue("@selectedQuestionID", question);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }

        public int Delete(int questionid)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Questions
                                WHERE QuestionID = @selectQuestionID";
            cmd.Parameters.AddWithValue("@selectQuestionID", questionid);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();
            //Return number of row of staff record updated or deleted
            return rowAffected;
        }
    }
}

