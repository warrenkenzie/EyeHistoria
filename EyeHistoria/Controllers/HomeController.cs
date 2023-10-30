using EyeHistoria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace EyeHistoria.Controllers
{
    public class HomeController : Controller
    {
        // put the symptoms to find diagnosis
        List<string> list_of_symptoms = new List<string>();

        // dummy diseases for diagnosis
        List<string> cataract = new List<string> { "eyeRedness", "eyeSwelling" };
        

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
            if(!list_of_diseases.Any()) //  checks if list_of_diseases is empty, if not generate
            {
                Generate_list_of_diseases();
            }
            List<int> checks = new List<int>();

            // put the list of symptoms in a list
            string eyeRedness = formData["eyeRedness"].ToString();
            string eyeItch = formData["eyeItch"].ToString();
            string eyeSwelling = formData["eyeSwelling"].ToString();

            list_of_symptoms.Add(eyeRedness);
            list_of_symptoms.Add(eyeItch);
            list_of_symptoms.Add(eyeSwelling);

            int index = list_of_symptoms.Count();
            int num_of_true = 0;
            // goes through the list of diseases
            foreach (KeyValuePair<string, List<string>> disease in list_of_diseases)
            {
                // iterates through symptoms sent
                for (int i = 0; i < list_of_symptoms.Count(); i++)
                {
                    // iterates through a disease's symptoms
                    for (int j = 0;j < disease.Value.Count(); j++)
                    {
                        // goes through the list symptoms of a disease and then true if the syptom matches list_of_symptoms
                        if (list_of_symptoms[i] == disease.Value[j])
                        {
                            num_of_true++;
                            break;
                        }
                    }
                   
                }
                checks.Add(num_of_true);
                num_of_true = 0;
            }
            Console.WriteLine(checks.ToString());
            ViewData["diagnosis"] = num_of_true;
            return View();
        }

        // generates a dict of diseases
        Dictionary<string, List<string>> Generate_list_of_diseases()
        {
            Dictionary<string, List<string>> list_of_diseases = new Dictionary<string, List<string>>();
            List<string> cataract = new List<string> { "eyeRedness", "eyeItch", "eyeSwelling" };
            List<string> blindness = new List<string> { "eyeSwelling" };
            List<string> swollenEyes = new List<string> { "eyeSwelling", "eyeItch" };  
            list_of_diseases.Add("cataract", cataract);
            list_of_diseases.Add("blindness", blindness);
            list_of_diseases.Add("swollenEyes", swollenEyes);

            return list_of_diseases;
        }
    }
}