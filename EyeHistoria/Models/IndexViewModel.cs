namespace EyeHistoria.Models
{
    public class IndexViewModel
    {
        public List<Symptoms> List_of_symptoms  { get; set; }
        public List<Question> Onset_questions { get; set; }
        public List<Question> Location_questions { get; set; }
        public List<Question> Duration_questions { get; set; }
        public List<Question> Characteristics_questions { get; set; }
        public List<Question> Aggravation_questions { get; set; }
        public List<Question> Relief_questions { get; set; }
        public List<Question> Timing_questions { get; set; }
        public List<Question> Severity_questions { get; set; }

        public IndexViewModel()
        {

        }

        public IndexViewModel(List<Symptoms> list_of_symptoms, List<Question> onset_questions, List<Question> location_questions, List<Question> duration_questions, List<Question> characteristics_questions, List<Question> aggravation_questions, List<Question> relief_questions, List<Question> timing_questions, List<Question> severity_questions)
        {
            List_of_symptoms = list_of_symptoms;
            Onset_questions = onset_questions;
            Location_questions = location_questions;
            Duration_questions = duration_questions;
            Characteristics_questions = characteristics_questions;
            Aggravation_questions = aggravation_questions;
            Relief_questions = relief_questions;
            Timing_questions = timing_questions;
            Severity_questions = severity_questions;
        }
    }
}
