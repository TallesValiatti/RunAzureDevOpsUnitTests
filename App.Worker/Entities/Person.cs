using App.Worker.Validators;
using FluentResults;

namespace App.Worker.Entities
{
    public class Person
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }

        private Person(Guid id, string fullName, string email)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Email = email;
        }

        public static Result<Person> Create(Guid Id, string fullName, string email)
        {
            var person = new Person(Id, fullName, email);

            var validation = PersonValidator.Validate(person);

            return validation.IsFailed ? validation : Result.Ok(person);
        }

        public Result ChangeEmail(string newEmail)
        {
            this.Email = newEmail;

            var validation = PersonValidator.Validate(this);

            return validation.IsFailed ? validation : Result.Ok();
        }
    }
}