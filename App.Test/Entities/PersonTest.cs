using System;
using App.Worker.Entities;
using FluentResults;
using Xunit;

namespace App.Test.Entities
{
    public class PersonTest
    {
        [Theory]
        [InlineData("", "email@email.com")]
        [InlineData(" ", "email@email.com")]
        [InlineData(null, "email@email.com")]
        [InlineData("fullName", "")]
        [InlineData("fullName", " ")]
        [InlineData("fullName", null)]
        public void Create_InvalidParams_FailedResult( string fullName, string email)
        {
            // Arrange
            Result<Person> newPersonResult;

            // Act
            newPersonResult = Person.Create(Guid.NewGuid(), fullName, email);

            // Assert
            Assert.True(newPersonResult.IsFailed);
        }

        [Fact]
        public void Create_ValidParams_SuccessfulResult()
        {
            // Arrange
            Result<Person> newPersonResult;
            var fullName = "fullName";
            var email = "email@email.com";
            var id = Guid.NewGuid();

            // Act
            newPersonResult = Person.Create(id, fullName, email);

            // Assert
            Assert.True(newPersonResult.IsSuccess);
            Assert.True(newPersonResult.Value.Id.Equals(id));
            Assert.True(newPersonResult.Value.Email.Equals(email));
            Assert.True(newPersonResult.Value.FullName.Equals(fullName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email")]
        [InlineData("email@")]
        [InlineData("email@email")]
        [InlineData("@email")]
        [InlineData("@email.com")]
        [InlineData("email.com")]
        public void ChangeEmail_InvalidParams_FailedResult(string newEmail)
        {
            // Arrange
            var fullName = "fullName";
            var email = "email@email.com";
            var id = Guid.NewGuid();
            
            var newPersonResult = Person.Create(id, fullName, email);
            var person = newPersonResult.Value;
            
            // Act
            var changeEmailResult = person.ChangeEmail(newEmail);

            // Assert
            Assert.True(changeEmailResult.IsFailed);
        }

        [Fact]
        public void ChangeEmail_ValidParams_SucessfulResult()
        {
            // Arrange
            var fullName = "fullName";
            var email = "email@email.com";
            var id = Guid.NewGuid();
            
            var newPersonResult = Person.Create(id, fullName, email);
            var person = newPersonResult.Value;

            var newEmail = "newEmail@email.com";
            
            // Act
            var changeEmailResult = person.ChangeEmail(newEmail);

            // Assert
            Assert.True(changeEmailResult.IsSuccess);
            Assert.True(person.Email.Equals(newEmail));
        }
    }
}