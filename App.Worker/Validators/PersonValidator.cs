using System.Text.RegularExpressions;
using App.Worker.Entities;
using FluentResults;

namespace App.Worker.Validators
{
    public static class PersonValidator
    {
        public static Result Validate(Person person)
        {
            return Result.Merge(
                   PersonValidator.ValidateFullName(person.FullName),
                   PersonValidator.ValidateEmail(person.Email));
        }

        private static Result ValidateFullName(string fullName)
        {
            if(string.IsNullOrWhiteSpace(fullName))
                return Result.Fail("Invalid string");

            return Result.Ok();
        }

        private static Result ValidateEmail(string email)
        {
            var InvalidEmailMessage = "Invalid Email";
            
             if(string.IsNullOrWhiteSpace(email))
                return Result.Fail(InvalidEmailMessage);
            
            var regexIsValid = Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);

            return Result.OkIf(regexIsValid, InvalidEmailMessage);
        }
    }
}