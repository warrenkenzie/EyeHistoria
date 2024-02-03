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
            return View();
        }

        // POST: ProcessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Demographic demographic)
        {
            if (ModelState.IsValid)
            {
                //Add staff record to database
                demographic.p_id = processContext.Add(demographic);
                //Redirect user to Staff/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(demographic);
            }
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

    }
}
