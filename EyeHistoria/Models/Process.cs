namespace EyeHistoria.Models
{
    public class Process
    {
        public Demographic demographic { get; set; }

        public ChiefComplaint chiefComplaint { get; set; }

        public PersonalOcularHistory personalOcularHistory { get; set; }

        public FamilyOcularHistory familyOcularHistory { get; set; }

        public PersonalHealthHistory personalHealthHistory { get; set; }

        public FamilyHealthHistory familyHealthHistory { get; set; }

        public Habits habits { get; set; }

        public Process(Demographic demographic, ChiefComplaint chiefComplaint, PersonalOcularHistory personalOcularHistory, FamilyOcularHistory familyOcularHistory, PersonalHealthHistory personalHealthHistory, FamilyHealthHistory familyHealthHistory, Habits habits)
        {
            this.demographic = demographic;
            this.chiefComplaint = chiefComplaint;
            this.personalOcularHistory = personalOcularHistory;
            this.familyOcularHistory = familyOcularHistory;
            this.personalHealthHistory = personalHealthHistory;
            this.familyHealthHistory = familyHealthHistory;
            this.habits = habits;
        }
    }
}
