﻿using System.ComponentModel.DataAnnotations;

namespace EyeHistoria.Models
{
    public class Diagnosis
    {
        [Display(Name = "ID")]
        public int DiagnosisID { get; set; }

        [Display(Name = "Diagnosis")]
        public string DiagnosisName { get; set; }

        [Display(Name = "Symptoms")]
        public List<string> List_of_diagnosis_symptoms { get; set;}

        [Display(Name = "Admin ID")]
        public int AdminID { get; set; }

        [Display(Name = "Created By")]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set;}
    }
}
