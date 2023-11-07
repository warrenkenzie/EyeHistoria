namespace EyeHistoria.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public int SymptomID { get; set; }
        public string SymptomName { get; set; }
        public int AdminID { get; set; }
        public string LastModifiedBy { get; set; }
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
