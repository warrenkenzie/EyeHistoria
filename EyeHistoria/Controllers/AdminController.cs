using EyeHistoria.Models;
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
                context.ExecuteYourStoredProcedure(symptoms);
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
            Diagnosis diagnosis = new Diagnosis();
            diagnosis.AdminID = 1;
            diagnosis.LastModifiedBy = "Jonathan Hong Yi Hao";
            return View(diagnosis);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDiagnosis(Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                //Add staff record to database
                diagnosis.DiagnosisID = context.AddDiagnosis(diagnosis);
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

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
