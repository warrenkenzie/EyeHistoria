using EyeHistoria.DAL;
using EyeHistoria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
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

        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            // add the list of symptoms
            indexViewModel.List_of_symptoms = symptomDAL.GetAllSymptoms();

            List<string> OLDCARTS = new List<string> { "Onset", "Location", "Duration" , "Characteristics" , "Aggravation", "Relief", "Timing", "Severity" };
            // add list of general questions
            List<GeneralQuestion> generalQuestions = new List<GeneralQuestion>();

            List<Question> GeneralquestionList = symptomDAL.Get_All_GeneralQuestions();
            foreach(Question question in GeneralquestionList)
            {
                generalQuestions.Add(
                    new GeneralQuestion
                    {
                        General_Question = question,
                        Follow_Questions = symptomDAL.Get_Follow_Qn_BasedOn_FollowUpId(question.QuestionID)
                    }
                );
            }
            indexViewModel.General_Questions = generalQuestions;
            /*indexViewModel.Onset_questions = symptomDAL.get_question_basedon_type("Onset");
            indexViewModel.Location_questions = symptomDAL.get_question_basedon_type("Location");
            indexViewModel.Duration_questions = symptomDAL.get_question_basedon_type("Duration");
            indexViewModel.Characteristics_questions = symptomDAL.get_question_basedon_type("Characteristics");
            indexViewModel.Aggravation_questions = symptomDAL.get_question_basedon_type("Aggravation");
            indexViewModel.Relief_questions = symptomDAL.get_question_basedon_type("Relief");
            indexViewModel.Timing_questions = symptomDAL.get_question_basedon_type("Timing");
            indexViewModel.Severity_questions = symptomDAL.get_question_basedon_type("Severity");*/

            return View(indexViewModel);
        }

        // passes symptomDAL.GetAllSymptoms() to Index.js
        public JsonResult GetSymptomsList()
        {
            // Assuming Model.List_of_symptoms is your list of symptoms
            return Json(symptomDAL.GetAllSymptoms());
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
            // initialise list of diseases
            Dictionary<string, List<string>> list_of_diseases = Generate_list_of_diseases();

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
            List<Diagnosis> list_of_diagnosis_SQL = symptomDAL.Get_Diagnostics();

            int num_of_matched_symptoms = 0;
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
                float matched = (float)num_of_matched_symptoms / diagnosis.List_of_diagnosis_symptoms.Count() * 100;

                Disease disease_obj = new Disease(diagnosis.DiagnosisName, matched, matched_symptoms, unmatched_symptoms);
                list_diseases_obj.Add(disease_obj);

                // reset num_of_matched_symptoms to 0 for the next iteration
                num_of_matched_symptoms = 0;

            }
           
            ViewData["diagnosis"] = num_of_matched_symptoms;
            return View(list_diseases_obj);
        }

        // generates a dict of diseases
        Dictionary<string, List<string>> Generate_list_of_diseases()
        {
            Dictionary<string, List<string>> list_of_diseases = new Dictionary<string, List<string>>();
            List<string> cataract = new List<string> { "Eye Redness", "Eye Irritation", "Eye Pain" };
            List<string> blindness = new List<string> { "Eye Irritation" };
            List<string> swollenEyes = new List<string> { "Eye Irritation", "Eye Irritation" };  
            list_of_diseases.Add("cataract", cataract);
            list_of_diseases.Add("blindness", blindness);
            list_of_diseases.Add("swollenEyes", swollenEyes);

            return list_of_diseases;
        }
    }
}