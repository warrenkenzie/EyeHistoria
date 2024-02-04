using EyeHistoria.DAL;
using EyeHistoria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EyeHistoria.Controllers
{
    public class ProcessController : Controller
    {

        private ProcessDAL processContext = new ProcessDAL();

        // GET: ProcessController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProcessController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProcessController/Create
        public ActionResult Create()
        {
            Demographic demographic = new Demographic();    
            return View(demographic);
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Demographic demographic)
        {

                //Add staff record to database
                demographic.p_id = processContext.Add(demographic);
                //Redirect user to Staff/Index view
                return RedirectToAction("Index");
            
        }

        // GET: ProcessController/Create
        public ActionResult Add2()
        {
            return View();
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add2(ChiefComplaint chiefComplaint)
        {
            if (ModelState.IsValid)
            {
                //Add staff record to database
                chiefComplaint.p_id = processContext.Add2(chiefComplaint);
                //Redirect user to Staff/Index view
                return RedirectToAction("ChiefComplaintProcess");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(chiefComplaint);
            }
        }

        public ActionResult ChiefComplaintProcess()
        {
            return View();
        }

        // GET: ProcessController/Create
        public ActionResult Add3()
        {
            return View();
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add3(PersonalOcularHistory personalOcularHistory)
        {
            if (ModelState.IsValid)
            {
                // Add personal ocular history record to the database
                personalOcularHistory.p_id = processContext.Add3(personalOcularHistory);
                // Redirect user to PersonalOcularHistoryProcess view
                return RedirectToAction("PersonalOcularHistoryProcess");
            }
            else
            {
                // Input validation fails, return to the Create view
                // to display error message
                return View(personalOcularHistory);
            }
        }

        public ActionResult PersonalOcularHistoryProcess()
        {
            return View();
        }

        // GET: ProcessController/Create
        public ActionResult Add4()
        {
            return View();
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add4(FamilyOcularHistory familyOcularHistory)
        {
            if (ModelState.IsValid)
            {
                // Add family ocular history record to the database
                familyOcularHistory.p_id = processContext.Add4(familyOcularHistory);
                // Redirect user to FamilyOcularHistoryProcess view
                return RedirectToAction("FamilyOcularHistoryProcess");
            }
            else
            {
                // Input validation fails, return to the Create view
                // to display error message
                return View(familyOcularHistory);
            }
        }

        public ActionResult FamilyOcularHistoryProcess()
        {
            return View();
        }

        // GET: ProcessController/Create
        public ActionResult Add5()
        {
            return View();
        }

        // Personal Health History
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add5(PersonalHealthHistory personalHealthHistory)
        {
            if (ModelState.IsValid)
            {
                personalHealthHistory.p_id = processContext.Add5(personalHealthHistory);
                // Redirect or return a response based on your requirements
                return RedirectToAction("PersonalHealthHistoryProcess");
            }
            else
            {
                // Handle validation errors
                return View(personalHealthHistory);
            }
        }

        public ActionResult PersonalHealthHistoryProcess()
        {
            return View();
        }

        // GET: ProcessController/Create
        public ActionResult Add6()
        {
            return View();
        }

        // Family Health History
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add6(FamilyHealthHistory familyHealthHistory)
        {
            if (ModelState.IsValid)
            {
                familyHealthHistory.p_id = processContext.Add6(familyHealthHistory);
                // Redirect or return a response based on your requirements
                return RedirectToAction("FamilyHealthHistoryProcess");
            }
            else
            {
                // Handle validation errors
                return View(familyHealthHistory);
            }
        }

        public ActionResult FamilyHealthHistoryProcess()
        {
            return View();
        }

        // GET: Habits/Create
        public ActionResult Add7()
        {
            return View();
        }

        // Family Health History
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add7(Habits habits)
        {
            if (ModelState.IsValid)
            {
                habits.p_id = processContext.Add7(habits);
                // Redirect or return a response based on your requirements
                return RedirectToAction("HabitsProcess");
            }
            else
            {
                // Handle validation errors
                return View(habits);
            }
        }

        public ActionResult HabitsProcess()
        {
            return View();
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

        public ActionResult ChatBot()
        {
            // VARIABLE, p_id_test, IS FOR TEST PUROSES THIS IS TO FIX p_id FOR NOW

            int p_id = 1;

            // load process
            Demographic demographic = processContext.GetPatient_Demographic(p_id);
            ChiefComplaint chiefComplaint = processContext.GetPatient_ChiefComplaint(p_id);
            PersonalOcularHistory personalOcularHistory = processContext.GetPatient_PersonalOcularHistory(p_id);
            FamilyOcularHistory familyOcularHistory = processContext.GetPatient_FamilyOcularHistory(p_id);
            PersonalHealthHistory personalHealthHistory = processContext.GetPatient_PersonalHealthHistory(p_id);
            FamilyHealthHistory familyHealthHistory = processContext.GetPatient_FamilyHealthHistory(p_id);
            Habits habits = processContext.GetPatient_Habits(p_id);

            Process process = new Process(demographic, chiefComplaint, personalOcularHistory, familyOcularHistory, personalHealthHistory, familyHealthHistory, habits);

            return View(process);
        }

    }
}
