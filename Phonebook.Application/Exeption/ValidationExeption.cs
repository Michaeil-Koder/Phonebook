using FluentValidation.Results;

namespace Phonebook.Application.Exeption
{
    public class ValidationExeption : ApplicationException
    {
        public List<string> Errors { get; set; }= new List<string>();

        public ValidationExeption(ValidationResult validationResult)
        {
            foreach(var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}
