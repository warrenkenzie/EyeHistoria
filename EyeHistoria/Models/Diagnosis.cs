﻿using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class Diagnosis
    {
        public int DiagnosisID { get; set; }
        public string DiagnosisName { get; set; }
        public List<string> List_of_diagnosis_symptoms { get; set;}
        public int AdminID { get; set; }
        public string LastModifiedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set;}
    }
}
