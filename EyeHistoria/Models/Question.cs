using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class Question
    {
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        [Display(Name = "Question")]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Question type is required.")]
        [Display(Name = "Question Type")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Question category is required.")]
        [Display(Name = "Question Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Symptom ID is required.")]
        [Display(Name = "Symptom ID")]
        public int SymptomID { get; set; }

        [Required(ErrorMessage = "Symptom name is required.")]
        [Display(Name = "Symptom Name")]
        public string SymptomName { get; set; }

        [Required(ErrorMessage = "Admin ID is required.")]
        [Display(Name = "Admin ID")]
        public int AdminID { get; set; }

        [Required(ErrorMessage = "Last modified by is required.")]
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        public int? FollowUpID { get; set; }

        public Question()
        {

        }

        public Question(int questionID, string questionText, string type, string category, int symptomID, string symptomName, int adminID, string lastModifiedBy, DateTime date, int? followUpID)
        {
            QuestionID = questionID;
            QuestionText = questionText;
            Type = type;
            Category = category;
            SymptomID = symptomID;
            SymptomName = symptomName;
            AdminID = adminID;
            LastModifiedBy = lastModifiedBy;
            Date = date;
            FollowUpID = followUpID;
        }

        public List<string> List_of_diagnosis_symptoms { get; set; }
    }
}
