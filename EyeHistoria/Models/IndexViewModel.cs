namespace EyeHistoria.Models
{
    public class IndexViewModel
    {
        public List<Symptoms> list_of_symptoms  { get; set; }
        public List<Question> onset_questions { get; set; }
        public List<Question> location_questions { get; set; }
        public List<Question> duration_questions { get; set; }
        public List<Question> characteristics_questions { get; set; }
        public List<Question> aggravation_questions { get; set; }
        public List<Question> relief_questions { get; set; }
        public List<Question> timing_questions { get; set; }
        public List<Question> severity_questions { get; set; }

        public IndexViewModel()
        {

        }

        public IndexViewModel(List<Symptoms> list_of_symptoms, List<Question> onset_questions, List<Question> location_questions, List<Question> duration_questions, List<Question> characteristics_questions, List<Question> aggravation_questions, List<Question> relief_questions, List<Question> timing_questions, List<Question> severity_questions)
        {
            this.list_of_symptoms = list_of_symptoms;
            this.onset_questions = onset_questions;
            this.location_questions = location_questions;
            this.duration_questions = duration_questions;
            this.characteristics_questions = characteristics_questions;
            this.aggravation_questions = aggravation_questions;
            this.relief_questions = relief_questions;
            this.timing_questions = timing_questions;
            this.severity_questions = severity_questions;
        }
    }
}
