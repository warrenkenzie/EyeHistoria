namespace EyeHistoria.Models
{
    public class Symptoms
    {
        public int SymptomID { get; set; }

        public string SymptomName { get; set; }

        public int AdminID { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime Date { get; set; }
    }
}
