using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;

namespace EyeHistoria.Models
{
    public class Diagnosis
    {
        public int DiagnosisID { get; set; }
        public string DiagnosisName { get; set; }
        public List<string> List_of_diagnosis_symptoms { get; set;}
        public Dictionary<string, int> List_of_diagnosis_symptoms_with_their_pain_level { get; set; }
        public int AdminID { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime Date { get; set;}

        public Diagnosis()
        {

        }

        public Diagnosis(int diagnosisID, string diagnosisName, List<string> list_of_diagnosis_symptoms, Dictionary<string, int> list_of_diagnosis_symptoms_with_their_pain_level, int adminID, string lastModifiedBy, DateTime date)
        {
            DiagnosisID = diagnosisID;
            DiagnosisName = diagnosisName;
            List_of_diagnosis_symptoms = list_of_diagnosis_symptoms;
            List_of_diagnosis_symptoms_with_their_pain_level = list_of_diagnosis_symptoms_with_their_pain_level;
            AdminID = adminID;
            LastModifiedBy = lastModifiedBy;
            Date = date;
        }
    }
}
