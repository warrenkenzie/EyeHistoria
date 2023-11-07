﻿using EyeHistoria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EyeHistoria.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlTypes;

namespace EyeHistoria.Controllers
{
    public class AdminController : Controller
    {
        private OurDAL context = new OurDAL();

        // GET: AdminController
        public ActionResult Index()
        {
            List<Symptoms> symptomsList = context.GetAllSymptoms();
            return View(symptomsList);
        }

        // GET: AdminController
        public ActionResult ViewDiagnosis()
        {
            List<Diagnosis> diagnosisList = context.Get_Diagnostics();
            return View(diagnosisList);
        }

        public ActionResult ViewQuestions()
        {
            List<Question> questionlist = context.GetQuestions();
            return View(questionlist);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            Symptoms symptoms = new Symptoms();
            symptoms.AdminID = 1;
            symptoms.LastModifiedBy = "Jonathan Hong Yi Hao";
            return View(symptoms);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Symptoms symptoms)
        {
            if (ModelState.IsValid)
            {
                //Add staff record to database
                Random random = new Random();
                int randomNumber = random.Next(1, 4);
                if (randomNumber == 1)
                {
                    context.AddSymptomsWithQuestionsTemplateOne(symptoms);
                }
                else if (randomNumber == 2)
                {
                    context.AddSymptomsWithQuestionsTemplateTwo(symptoms);
                }
                else
                {
                    context.AddSymptomsWithQuestionsTemplateThree(symptoms);
                }
                //Redirect user to Staff/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(symptoms);
            }
        }

        // GET: AdminController/Create
        public ActionResult AddDiagnosis()
        {
            AddDiagnosisViewModel diagnosis = new AddDiagnosisViewModel();
            diagnosis.AdminID = 1;
            diagnosis.LastModifiedBy = "Jonathan Hong Yi Hao";
            ViewData["list_of_all_symptoms_from_SQL"] = context.GetAllSymptoms();
            return View(diagnosis); 
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDiagnosis(AddDiagnosisViewModel diagnosis)
        {
            if (ModelState.IsValid)
            {
                List<Symptoms> list_of_all_symptoms = context.GetAllSymptoms();

                // convert AddDiagnosisViewModel to Diagnosis
                Diagnosis diagnosis_processed = new Diagnosis();
                diagnosis_processed.DiagnosisID = diagnosis.DiagnosisID;
                diagnosis_processed.DiagnosisName = diagnosis.DiagnosisName;
                diagnosis_processed.AdminID = diagnosis.AdminID;
                diagnosis_processed.LastModifiedBy = diagnosis.LastModifiedBy;
                diagnosis_processed.Date = diagnosis.Date;
                diagnosis_processed.List_of_diagnosis_symptoms = new List<string>();
                for (int i = 0;i < diagnosis.List_of_diagnosis_symptoms_checkbox.Count(); i++)
                {
                    if (diagnosis.List_of_diagnosis_symptoms_checkbox[i] == true)
                    {
                        diagnosis_processed.List_of_diagnosis_symptoms.Add(list_of_all_symptoms[i].SymptomName);
                    }
                }

                //Add staff record to database
                diagnosis_processed.DiagnosisID = context.AddDiagnosis(diagnosis_processed);
                //Redirect user to Staff/Index view
                return RedirectToAction("ViewDiagnositics");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(diagnosis);
            }
        }

        public ActionResult AddQuestions()
        {
            Question question = new Question();
            question.AdminID = 1;
            question.LastModifiedBy = "Jonathan Hong Yi Hao";
            return View(question);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestions(Question question)
        {
            if (ModelState.IsValid)
            {
                //Add staff record to database
                question.QuestionID = context.AddQuestion(question);
                //Redirect user to Staff/Index view
                return RedirectToAction("ViewQuestions");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(question);
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult UpdateQuestion(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                //Update staff record to database
                context.UpdateQuestion(question);
                return RedirectToAction("ViewQuestions");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(question);
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult DeleteQuestions(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestions(Question question)
        {
            // Delete the staff record from database
            context.Delete(question.QuestionID);
                return RedirectToAction("Index");
        }
    }
}
