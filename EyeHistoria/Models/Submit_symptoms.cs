namespace EyeHistoria.Models
{
    public class Submit_symptoms
    {
        public string SymptomName_ticked { get; set; }
        public Symptoms Symptom { get; set; }
        public string? Yes_No_data { get; set;}
        public int? Severity_level { get; set; }

    }
}
