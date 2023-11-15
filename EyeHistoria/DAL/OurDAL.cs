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

        public List<Question> Get_All_GeneralQuestions()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Questions WHERE Category = 'General'";

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

        public List<Question> Get_Follow_Qn_BasedOn_FollowUpId(int FollowUpId)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Questions WHERE FollowUpID = @followUp_Id";

            cmd.Parameters.AddWithValue("@followUp_Id", FollowUpId);

            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Question> list_of_followUp_questions = new List<Question>();
            while (reader.Read())
            {
                list_of_followUp_questions.Add(
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
                        FollowUpID = (!reader.IsDBNull(9) ? reader.GetInt32(9) : null),
                        Data_questionID = (!reader.IsDBNull(10) ? reader.GetInt32(10) : null)
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return list_of_followUp_questions;
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
                        LearnMore = reader.GetString(2),
                        List_of_diagnosis_symptoms = reader.GetString(3).Split(',').Select(symptom => symptom.Trim()).ToList(),
                        Tests = reader.GetString(4),
                        AdminID = reader.GetInt32(5),
                        LastModifiedBy = reader.GetString(6),
                        Date = reader.GetDateTime(7)
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return list_of_diagnostic;
        }


        public List<Diagnosis_symptoms> Get_List_Diagnostic_Symptom_BasedOn_DiagnosisID(int DiagnosisID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Diagnosis_symptoms WHERE DiagnosisID = @diagnosisID";

            cmd.Parameters.AddWithValue("@diagnosisID", DiagnosisID);
            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Diagnosis_symptoms> list_of_diagnostic = new List<Diagnosis_symptoms>();
            while (reader.Read())
            {
                list_of_diagnostic.Add(
                    new Diagnosis_symptoms()
                    {
                        Diagnosis_symptomsID = reader.GetInt32(0),
                        SymptomName = reader.GetString(1),
                        DiagnosisID = reader.GetInt32(2)
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return list_of_diagnostic;
        }

        public List<Data_question> Get_List_Data_question_BasedOn_Diagnosis_symptomsID(int Diagnosis_symptomsID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Data_question WHERE Diagnosis_symptomsID = @diagnosis_symptomsID";

            cmd.Parameters.AddWithValue("@diagnosis_symptomsID", Diagnosis_symptomsID);
            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<Data_question> list_of_Data_question = new List<Data_question>();
            while (reader.Read())
            {
                list_of_Data_question.Add(
                    new Data_question()
                    {
                        Data_questionId = reader.GetInt32(0),
                        DataType = reader.GetString(1),
                        DataValue = reader.GetString(2),
                        Type = reader.GetString(3)
                    }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return list_of_Data_question;
        }

        public Data_question Get_Data_question_BasedOn_Data_questionID(int? Data_questionID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Data_question WHERE Data_questionID = @data_questionID";

            cmd.Parameters.AddWithValue("@data_questionID", Data_questionID);
            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            Data_question data_question = null;
            if (reader.Read())
            {
                data_question = new Data_question
                {
                    Data_questionId = reader.GetInt32(0),
                    DataType = reader.GetString(1),
                    DataValue = reader.GetString(2),
                    Type = reader.GetString(3)
                };
                
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return data_question;
        }


        public int AddDiagnosis(Diagnosis diagnosis)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Diagnosis (DiagnosisName, LearnMore, Symptoms, Tests, AdminID, 
                                LastModifiedBy, Date) 
                                OUTPUT INSERTED.DiagnosisID 
                                VALUES(@diagnosisname, @learnmore, @symptoms, @tests, @adminid, @lastmodifiedby, @date)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@diagnosisname", diagnosis.DiagnosisName);
            cmd.Parameters.AddWithValue("@learnmore", diagnosis.LearnMore);
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
            cmd.Parameters.AddWithValue("@tests", diagnosis.Tests);
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

        public List<int> Get_list_of_data_questionID_for_each_symptom_from_Questions(string symptomName)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT Data_questionID FROM Questions WHERE SymptomName = @symptomName AND Category = 'Follow-up'";
            cmd.Parameters.AddWithValue("@symptomName", symptomName);
            //A connection to database must be opened before any operations made.
            conn.Open();

            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            List<int> list_of_data_questionID = new List<int>();
            while (reader.Read())
            {
                list_of_data_questionID.Add(
                    reader.GetInt32(0)
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return list_of_data_questionID;
        }
    }
}

