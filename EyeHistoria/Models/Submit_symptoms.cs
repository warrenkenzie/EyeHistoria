namespace EyeHistoria.Models
{
    public class Submit_symptoms
    {
        public string SymptomName_ticked { get; set; }
        public Symptoms Symptom { get; set; }
        public Dictionary<int, string> Yes_No_data { get; set;}
        public Dictionary<int, int> Severity_level { get; set; }

        public Submit_symptoms()
        {
            Yes_No_data = new Dictionary<int, string>();
            Severity_level = new Dictionary<int, int>();
        }

    }
}
