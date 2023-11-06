﻿using EyeHistoria.DAL;
using EyeHistoria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace EyeHistoria.Controllers
{
    public class HomeController : Controller
    {
        // list of disease_obj
        List<Disease> list_diseases_obj = new List<Disease>();

        // DAL
        private OurDAL symptomDAL = new OurDAL();


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            // add the list of symptoms
            indexViewModel.List_of_symptoms = symptomDAL.GetAllSymptoms();

            // add the questions
            indexViewModel.Onset_questions = symptomDAL.get_question_basedon_type("Onset");
            indexViewModel.Location_questions = symptomDAL.get_question_basedon_type("Location");
            indexViewModel.Duration_questions = symptomDAL.get_question_basedon_type("Duration");
            indexViewModel.Characteristics_questions = symptomDAL.get_question_basedon_type("Characteristics");
            indexViewModel.Aggravation_questions = symptomDAL.get_question_basedon_type("Aggravation");
            indexViewModel.Relief_questions = symptomDAL.get_question_basedon_type("Relief");
            indexViewModel.Timing_questions = symptomDAL.get_question_basedon_type("Timing");
            indexViewModel.Severity_questions = symptomDAL.get_question_basedon_type("Severity");

            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SubmitDiagnosis()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult SubmitDiagnosis(IFormCollection formData)
        {
            // put the symptoms to find diagnosis
            List<string> list_of_symptoms_ticked = new List<string>();

            // get all the symptoms from SQL
            List<Symptoms> list_of_SQL_symptoms = symptomDAL.GetAllSymptoms();
            
            // put the list of symptoms ticked from symptom checher
            foreach (Symptoms symptom in list_of_SQL_symptoms)
            {
                list_of_symptoms_ticked.Add(formData[symptom.SymptomName].ToString());

            }

            // get all the diagnosis from SQL
            // List<Diagnosis> list_of_diagnosis_SQL = symptomDAL.Get_Diagnostics();
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            // Add some key-value pairs to the dictionary.
            dictionary["Blurred Vision"] = 5;
            dictionary["Light Sensitivity"] = 2;
            dictionary["Double Vision"] = 3;
            dictionary["Night Blindness"] = 5;

            List<Diagnosis> list_of_diagnosis_SQL = new List<Diagnosis>();
            list_of_diagnosis_SQL.Add(
                new Diagnosis(1, "Cataracts", new List<string> { "Blurred Vision", "Light Sensitivity", "Double Vision", "Night Blindness" },
                dictionary
                , 1, "Jonathan Hong Yi Hao", new DateTime(2023, 11, 5, 14, 34, 26, 753))
                );

            int num_of_matched_symptoms = 0;
            float match = 0;
            // goes through the list of diseases
            foreach (Diagnosis diagnosis in list_of_diagnosis_SQL)
            {
                List<string> matched_symptoms = new List<string>();
                List<string> unmatched_symptoms = new List<string>();
                // iterates through symptoms sent
                for (int i = 0; i < list_of_symptoms_ticked.Count(); i++)
                {
                    // iterates through a disease's symptoms
                    for (int j = 0;j < diagnosis.List_of_diagnosis_symptoms.Count(); j++)
                    {
                        // goes through the list symptoms of a disease and then true if the syptom matches list_of_symptoms
                        if (list_of_symptoms_ticked[i] == diagnosis.List_of_diagnosis_symptoms[j])
                        {
                            // pain measuer code
                            // formData[list_of_symptoms_ticked[i] + "_Option"].ToString() 
                            int valuesss = Convert.ToInt32(formData[list_of_symptoms_ticked[i] + "_Option"]);
                            Console.WriteLine(valuesss);

                            // !   CALUCLATE PAIN MEASURE  ! //
                            if(Convert.ToInt32(formData[list_of_symptoms_ticked[i] + "_Option"]) > diagnosis.List_of_diagnosis_symptoms_with_their_pain_level[list_of_symptoms_ticked[i]])
                            {
                                match += (float)(Convert.ToInt32(formData[list_of_symptoms_ticked[i] + "_Option"]) - diagnosis.List_of_diagnosis_symptoms_with_their_pain_level[list_of_symptoms_ticked[i]]) * 25 / diagnosis.List_of_diagnosis_symptoms.Count();
                            }
                            else if(Convert.ToInt32(formData[list_of_symptoms_ticked[i] + "_Option"]) < diagnosis.List_of_diagnosis_symptoms_with_their_pain_level[list_of_symptoms_ticked[i]])
                            {
                                match += (float)(diagnosis.List_of_diagnosis_symptoms_with_their_pain_level[list_of_symptoms_ticked[i]] - Convert.ToInt32(formData[list_of_symptoms_ticked[i] + "_Option"])) * 25 / diagnosis.List_of_diagnosis_symptoms.Count();
                            }
                            else if(Convert.ToInt32(formData[list_of_symptoms_ticked[i] + "_Option"]) == diagnosis.List_of_diagnosis_symptoms_with_their_pain_level[list_of_symptoms_ticked[i]])
                            {
                                match += (float) 100 / (diagnosis.List_of_diagnosis_symptoms.Count());
                            }
                            matched_symptoms.Add(diagnosis.List_of_diagnosis_symptoms[j]);
                            num_of_matched_symptoms++;
                            break;
                        }
                    }
                    // if there are no matched symptoms, add it to unmatched_symptoms
                    if (num_of_matched_symptoms == 0)
                    {
                        unmatched_symptoms.Add(list_of_symptoms_ticked[i]);
                    }
                }

                //  !!!!!! CALCULATIONS !!!!!!

                // calculate the match of list of symptoms to disease and if more than 50%, add it to list_diseases_obj
                //float match = (float)num_of_matched_symptoms / list_of_SQL_symptoms.Count() * 100;

                Disease disease_obj = new Disease(diagnosis.DiagnosisName, match, matched_symptoms, unmatched_symptoms);
                list_diseases_obj.Add(disease_obj);

                // reset num_of_matched_symptoms to 0 for the next iteration
                num_of_matched_symptoms = 0;
                match = 0;
            }
           
            ViewData["diagnosis"] = num_of_matched_symptoms;
            return View(list_diseases_obj);
        }
    }
}