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

        // list of disease_obj
        List<Disease> list_diseases_obj = new List<Disease>();


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
            int num_of_matched_symptoms = 0;
            // goes through the list of diseases
            foreach (KeyValuePair<string, List<string>> disease in list_of_diseases)
            {
                List<string> matched_symptoms = new List<string>();
                List<string> unmatched_symptoms = new List<string>();
                // iterates through symptoms sent
                for (int i = 0; i < list_of_symptoms.Count(); i++)
                {
                    // iterates through a disease's symptoms
                    for (int j = 0;j < disease.Value.Count(); j++)
                    {
                        // goes through the list symptoms of a disease and then true if the syptom matches list_of_symptoms
                        if (list_of_symptoms[i] == disease.Value[j])
                        {
                            matched_symptoms.Add(disease.Value[j]);
                            num_of_matched_symptoms++;
                            break;
                        }
                    }
                    // if there are no matched symptoms, add it to unmatched_symptoms
                    if (num_of_matched_symptoms == 0)
                    {
                        unmatched_symptoms.Add(list_of_symptoms[i]);
                    }
                }

                // calculate the match of list of symptoms to disease and if more than 50%, add it to list_diseases_obj
                float matched = (float)num_of_matched_symptoms / 3 * 100;
                Disease disease_obj = new Disease(disease.Key, matched, matched_symptoms, unmatched_symptoms);
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