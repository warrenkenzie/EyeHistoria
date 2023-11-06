using System.ComponentModel.DataAnnotations;
using EyeHistoria.DAL;

namespace EyeHistoria.Models
{
    public class ValidateSymptomExists : ValidationAttribute
    {
        private OurDAL context = new OurDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            // Get the symptom name value to validate
            string symptomName = Convert.ToString(value);


            // Casting the validation context to the "Symptoms" model class
            Symptoms symptoms = (Symptoms)validationContext.ObjectInstance;
            // Get the Symptom Id from the symptom instance
            int symptomID = symptoms.SymptomID;
            if (context.IsSymptomExist(symptomName, symptomID))
                // validation failed
                return new ValidationResult
                ("Oops! This symptom already exists!");
            else
                // validation passed 
                return ValidationResult.Success;
        }
    }
 }
