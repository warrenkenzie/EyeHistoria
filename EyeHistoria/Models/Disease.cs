using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace EyeHistoria.Models
{
    public class Disease
    {
        public string Disease_name { get; set; }

        public float Match { get; set; }

        public Dictionary<string,List<Data_question>> Matched_symptoms { get; set; }

        public Dictionary<string, List<Data_question>> Unmatched_symptoms { get; set; }

        public string LearnMore { get; set; }

        public string Tests { get; set; }


        // default constructor
        public Disease()
        {
            
        }

        // constructor
        public Disease(string disease, float match, Dictionary<string, List<Data_question>> matched_symptoms, Dictionary<string, List<Data_question>> unmatched_symptoms,string learnMore, string tests)
        {
            Disease_name = disease;
            Match = match;
            Matched_symptoms = matched_symptoms;
            Unmatched_symptoms = unmatched_symptoms;
            LearnMore = learnMore;
            Tests = tests;

        }
    }
}
