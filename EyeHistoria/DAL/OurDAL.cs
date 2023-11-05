﻿using System.Collections;
using System.Collections.Generic;
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

        public int Add(Symptoms symptoms)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Symptoms (SymptomName, AdminID, LastModifiedBy, 
                                Date) 
                                OUTPUT INSERTED.SymptomID 
                                VALUES(@symptomname, @adminid, @lastmodifiedby, @date)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@symptomname", symptoms.SymptomName);
            cmd.Parameters.AddWithValue("@adminid", symptoms.AdminID);
            cmd.Parameters.AddWithValue("@lastmodifiedby", symptoms.LastModifiedBy);
            cmd.Parameters.AddWithValue("@date", symptoms.Date);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            symptoms.SymptomID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return symptoms.SymptomID;
        }

        public List<Question> get_question_basedon_type(string question_type)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            // query
            cmd.CommandText = @"SELECT * FROM Questions WHERE Type= @question_type";
            
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
                        FollowUpID = (!reader.IsDBNull(9) ? reader.GetInt32(8) : null)
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

    }
}
