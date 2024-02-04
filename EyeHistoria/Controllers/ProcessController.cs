using EyeHistoria.DAL;
using EyeHistoria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeHistoria.Controllers
{
    public class ProcessController : Controller
    {

        private ProcessDAL processContext = new ProcessDAL();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Demographic demographic)
        {
            if (ModelState.IsValid)
            {
				// Add demographic record to the database
				demographic.p_id = processContext.Add(demographic);
                HttpContext.Session.SetInt32("demographic.p_id", demographic.p_id);
				// Redirect user to ChiefComplaintProcess view
				return RedirectToAction("ChiefComplaintProcess");
            }
            else
            {
                //Input validation fails, return to the View
                return View();
            }

        }

        public ActionResult ChiefComplaintProcess()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChiefComplaintProcess(ChiefComplaint chiefComplaint)
        {
            if (ModelState.IsValid)
            {
				// Add chiefComplaint record to the database
				int storedId = HttpContext.Session.GetInt32("demographic.p_id") ?? 0;
                chiefComplaint.p_id = storedId;
                processContext.Add2(chiefComplaint);
				// Redirect user to PersonalOcularHistoryProcess view
				return RedirectToAction("PersonalOcularHistoryProcess");
            }
            else
            {
				
				return View(chiefComplaint);
            }
        }

        // GET: ProcessController/Create
        public ActionResult PersonalOcularHistoryProcess()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalOcularHistoryProcess(PersonalOcularHistory personalOcularHistory)
        {
            if (ModelState.IsValid)
            {
                // Add personal ocular history record to the database
                int storedId = HttpContext.Session.GetInt32("demographic.p_id") ?? 0;
                personalOcularHistory.p_id = storedId;
                personalOcularHistory.p_id = processContext.Add3(personalOcularHistory);

				// Redirect user to FamilyOcularHistoryProcess view
				return RedirectToAction("FamilyOcularHistoryProcess");
            }
            else
            {
                // Input validation fails, return to the Create view
                // to display error message
                return View(personalOcularHistory);
            }
        }

        // GET: ProcessController/Create
        public ActionResult FamilyOcularHistoryProcess()
        {
            return View();
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FamilyOcularHistoryProcess(FamilyOcularHistory familyOcularHistory)
        {
            if (ModelState.IsValid)
            {
				// Add familyOcularHistory record to the database
				int storedId = HttpContext.Session.GetInt32("demographic.p_id") ?? 0;
                familyOcularHistory.p_id = storedId;
                familyOcularHistory.p_id = processContext.Add4(familyOcularHistory);
				// Redirect user to PersonalHealthHistoryProcess view
				return RedirectToAction("PersonalHealthHistoryProcess");
            }
            else
            {
                // Input validation fails, return to the Create view
                // to display error message
                return View(familyOcularHistory);
            }
        }

        public ActionResult PersonalHealthHistoryProcess()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonalHealthHistoryProcess(PersonalHealthHistory personalHealthHistory)
        {
			// Add personalHealthHistory record to the database
			int storedId = HttpContext.Session.GetInt32("demographic.p_id") ?? 0;
            personalHealthHistory.p_id = storedId;
            personalHealthHistory.p_id = processContext.Add5(personalHealthHistory);
			// Redirect user to FamilyHealthHistoryProcess view
			return RedirectToAction("FamilyHealthHistoryProcess");
        }

        public ActionResult FamilyHealthHistoryProcess()
        {
            return View();
        }

        // Family Health History
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FamilyHealthHistoryProcess(FamilyHealthHistory familyHealthHistory)
        {
			// Add familyHealthHistory record to the database
			int storedId = HttpContext.Session.GetInt32("demographic.p_id") ?? 0;
            familyHealthHistory.p_id = storedId;
            processContext.Add6(familyHealthHistory);
			// Redirect user to HabitsProcess view
			return RedirectToAction("HabitsProcess");
        }

        // GET: Habits/Create
        public ActionResult HabitsProcess()
        {
            return View();
        }

        // Family Health History
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HabitsProcess(Habits habits)
        {
            if (ModelState.IsValid)
            {
				// Add habits record to the database
				int storedId = HttpContext.Session.GetInt32("demographic.p_id") ?? 0;
                habits.p_id = storedId;
                habits.p_id = processContext.Add7(habits);
				// Redirect user to Summary view
				return RedirectToAction("Summary");
            }
            else
            {
                // Handle validation errors
                return View(habits);
            }
        }


        // GET: ProcessController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProcessController/Edit/5
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

        // GET: ProcessController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProcessController/Delete/5
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

        // GET: ProcessController/Create
        public ActionResult AddCombo()
        {
            OcularHistoryViewModel viewModel = new OcularHistoryViewModel
            {
                PersonalOcularHistory = new PersonalOcularHistory(),
                FamilyOcularHistory = new FamilyOcularHistory()
            };

            return View(viewModel);
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCombo(OcularHistoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.PersonalOcularHistory != null)
                {
                    viewModel.PersonalOcularHistory.p_id = processContext.Add3(viewModel.PersonalOcularHistory);
                    return RedirectToAction("PersonalOcularHistoryProcess");
                }
                else if (viewModel.FamilyOcularHistory != null)
                {
                    viewModel.FamilyOcularHistory.p_id = processContext.Add4(viewModel.FamilyOcularHistory);
                    return RedirectToAction("FamilyOcularHistoryProcess");
                }
            }

            return View(viewModel);
        }

        public ActionResult OcularHistoryProcess()
        {
            return View();
        }

        public ActionResult Summary()
        {

            int p_id = HttpContext.Session.GetInt32("demographic.p_id") ?? 0;

            // load process
            Demographic demographic = processContext.GetPatient_Demographic(p_id);
            ChiefComplaint chiefComplaint = processContext.GetPatient_ChiefComplaint(p_id);
            PersonalOcularHistory personalOcularHistory = processContext.GetPatient_PersonalOcularHistory(p_id);
            FamilyOcularHistory familyOcularHistory = processContext.GetPatient_FamilyOcularHistory(p_id);
            PersonalHealthHistory personalHealthHistory = processContext.GetPatient_PersonalHealthHistory(p_id);
            FamilyHealthHistory familyHealthHistory = processContext.GetPatient_FamilyHealthHistory(p_id);
            Habits habits = processContext.GetPatient_Habits(p_id);

            Process summary = new Process(demographic, chiefComplaint, personalOcularHistory, familyOcularHistory, personalHealthHistory, familyHealthHistory, habits);

            return View(summary);
        }

    }
}
