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

        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            // add the list of symptoms
            List<string> OLDCARTS = new List<string> { "Onset", "Location", "Duration" , "Characteristics" , "Aggravation", "Relief", "Timing", "Severity" };
            indexViewModel.List_of_symptoms = symptomDAL.GetAllSymptoms();
            indexViewModel.General_Questions = Get_generalQuestions();
            
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
            // get all the diagnosis from SQL

            List<Diagnosis> list_of_diagnosis_SQL = Get_List_of_Diagnosis();

            // put the symptoms to find diagnosis
            List<Submit_symptoms> list_of_submitted_symptoms = new List<Submit_symptoms>();

            // get all the symptoms from SQL
            List<Symptoms> list_of_SQL_symptoms = symptomDAL.GetAllSymptoms();
            
            // put the list of symptoms ticked from symptom checher
            foreach (Symptoms symptom in list_of_SQL_symptoms)
            {
                if(formData[symptom.SymptomName].ToString() != "")
                {
                    Submit_symptoms submit_Symptoms = new Submit_symptoms();
                    submit_Symptoms.SymptomName_ticked = formData[symptom.SymptomName].ToString();
                    submit_Symptoms.Symptom = symptom;
                    
                    if(formData[symptom.SymptomName + "_Yes_No"] != "")
                    {
                        submit_Symptoms.Yes_No_data = formData[symptom.SymptomName + "_Yes_No"].ToString();
                    }

                    if (formData[symptom.SymptomName + "_Option"] != "")
                    {
                        submit_Symptoms.Severity_level = Convert.ToInt32(formData[symptom.SymptomName + "_Option"]);
                    }


                    // add the object to list
                    list_of_submitted_symptoms.Add(submit_Symptoms);
                }
            }
            Console.WriteLine(list_of_submitted_symptoms);

            int num_of_matched_symptoms = 0;
            // goes through the list of diseases
            foreach (Diagnosis diagnosis in list_of_diagnosis_SQL)
            {
                List<string> matched_symptoms = new List<string>();
                List<string> unmatched_symptoms = new List<string>();
                // iterates through symptoms sent
                for (int i = 0; i < list_of_submitted_symptoms.Count(); i++)
                {
                    // iterates through a disease's symptoms
                    for (int j = 0;j < diagnosis.List_of_diagnosis_symptoms.Count(); j++)
                    {
                        // goes through the list symptoms of a disease and then true if the syptom matches list_of_symptoms
                        if (list_of_submitted_symptoms[i].SymptomName_ticked == diagnosis.List_of_diagnosis_symptoms[j])
                        {
                            matched_symptoms.Add(diagnosis.List_of_diagnosis_symptoms[j]);
                            num_of_matched_symptoms++;
                            break;
                        }
                    }
                    // if there are no matched symptoms, add it to unmatched_symptoms
                    if (num_of_matched_symptoms == 0)
                    {
                        unmatched_symptoms.Add(list_of_submitted_symptoms[i].SymptomName_ticked);
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

        // get list of diagnosis
        List<Diagnosis> Get_List_of_Diagnosis()
        {
            List<Diagnosis> list_of_Diagnosis = symptomDAL.Get_Diagnostics();
            foreach(Diagnosis diagnosis in list_of_Diagnosis)
            {
                // iterates through each diagnosis to fill List_diagnosis_symptoms in each diagnosis
                diagnosis.List_diagnosis_symptoms = symptomDAL.Get_List_Diagnostic_Symptom_BasedOn_DiagnosisID(diagnosis.DiagnosisID);

                // iterates through the List_diagnosis_symptoms in the current iteration's diagnosis to fill List_data_Questions in diagnosis_symptoms
                foreach (Diagnosis_symptoms diagnosis_symptoms in diagnosis.List_diagnosis_symptoms)
                {
                    diagnosis_symptoms.List_data_Questions = symptomDAL.Get_List_Data_question_BasedOn_Diagnosis_symptomsID(diagnosis_symptoms.Diagnosis_symptomsID);
                }
            }

            return list_of_Diagnosis;
        }

        // Get all the general questions with their follow up qns
        List<GeneralQuestion> Get_generalQuestions()
        {
            // add list of general questions
            List<GeneralQuestion> generalQuestions = new List<GeneralQuestion>();

            List<Question> GeneralquestionList = symptomDAL.Get_All_GeneralQuestions();
            foreach (Question question in GeneralquestionList)
            {
                GeneralQuestion generalQuestion = new GeneralQuestion();
                generalQuestion.General_Question = question;
                generalQuestion.Follow_Questions = symptomDAL.Get_Follow_Qn_BasedOn_FollowUpId(question.QuestionID);
                foreach (Question follow_qns in generalQuestion.Follow_Questions)
                {
                    follow_qns.Data_question = symptomDAL.Get_Data_question_BasedOn_Data_questionID(follow_qns.Data_questionID);

                }
                generalQuestions.Add(generalQuestion);
            }

            return generalQuestions;
        }
    }
}