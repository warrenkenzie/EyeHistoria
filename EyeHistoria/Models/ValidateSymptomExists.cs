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
            // Get the email value to validate
            string symptomName = Convert.ToString(value);


            // Casting the validation context to the "Staff" model class
            Symptoms symptoms = (Symptoms)validationContext.ObjectInstance;
            // Get the Staff Id from the staff instance
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
