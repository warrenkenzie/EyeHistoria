namespace EyeHistoria.Models
{
    public class Diagnosis_symptoms
    {
        public int Diagnosis_symptomsID { get; set; }  

        public string SymptomName { get; set; }

        public int DiagnosisID { get; set; }

        public List<Data_question> List_data_Questions { get; set; }

       
    }
}
