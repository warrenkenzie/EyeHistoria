using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace EyeHistoria.Models
{
    public class Disease
    {
        public string disease_name { get; set; }

        public float match { get; set; }

        public List<string> matched_symptoms { get; set; }

        public List<string> unmatched_symptoms { get; set; }

        // default constructor
        public Disease()
        {
            
        }

        // constructor
        public Disease(string Disease, float Match, List<string> Matched_symptoms, List<string> Unmatched_symptoms)
        {
            disease_name = Disease;
            match = Match;
            matched_symptoms = Matched_symptoms;
            unmatched_symptoms = Unmatched_symptoms;
        }
    }
}
