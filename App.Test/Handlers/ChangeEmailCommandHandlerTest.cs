using System;
using System.Threading;
using App.Worker.Commands;
using App.Worker.Entities;
using App.Worker.Handlers;
using App.Worker.Repositories;
using Moq;
using Xunit;

namespace App.Test.Handlers
{
    public class ChangeEmailCommandHandlerTest
    {
        [Fact]
        public async void Handle_ValidCommand_SucessfulResult()
        {
            // Arrange
            var fullName = "fullName";
            var email = "email@email.com";
            var newEmail = "newEmail@email.com";
            var id = Guid.NewGuid();
            
            var newPersonResult = Person.Create(id, fullName, email);
            var person = newPersonResult.Value;

            var command = new ChangeEmailCommand
            {
                newEmail = newEmail,
                UserId = id
            };

            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            mock.Setup(x => x.GetByIdAsync(It.Is<Guid>(o => o.Equals(id)))).ReturnsAsync(person);
            mock.Setup(x => x.UpdateAsync(It.IsAny<Person>()));
            
            var cancellationToken = new CancellationToken();
            var handler = new ChangeEmailCommandHandler(mock.Object);

            // Act
            var handlerResult = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(handlerResult.IsSuccess);
        }

        [Fact]
        public async void Handle_PersonDoesNotExist_FailedResult()
        {
            // Arrange
            var fullName = "fullName";
            var email = "email@email.com";
            var newEmail = "newEmail@email.com";
            var id = Guid.NewGuid();
            
            var newPersonResult = Person.Create(id, fullName, email);
            var person = newPersonResult.Value;

            var command = new ChangeEmailCommand
            {
                newEmail = newEmail,
                UserId = id
            };

            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            mock.Setup(x => x.GetByIdAsync(It.Is<Guid>(o => o.Equals(id)))).ReturnsAsync(default(Person));
            mock.Setup(x => x.UpdateAsync(It.IsAny<Person>()));
            
            var cancellationToken = new CancellationToken();
            var handler = new ChangeEmailCommandHandler(mock.Object);

            // Act
            var handlerResult = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(handlerResult.IsFailed);
        }

        [Fact]
        public async void Handle_InvalidNewEmail_FailedResult()
        {
            // Arrange
            var fullName = "fullName";
            var email = "email@email.com";
            var newEmail = "@email.com";
            var id = Guid.NewGuid();
            
            var newPersonResult = Person.Create(id, fullName, email);
            var person = newPersonResult.Value;

            var command = new ChangeEmailCommand
            {
                newEmail = newEmail,
                UserId = id
            };

            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            mock.Setup(x => x.GetByIdAsync(It.Is<Guid>(o => o.Equals(id)))).ReturnsAsync(person);
            mock.Setup(x => x.UpdateAsync(It.IsAny<Person>()));
            
            var cancellationToken = new CancellationToken();
            var handler = new ChangeEmailCommandHandler(mock.Object);

            // Act
            var handlerResult = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(handlerResult.IsFailed);
        }

        [Fact]
        public async void Handle_UnexpectedError_FailedResult()
        {
            // Arrange
            var fullName = "fullName";
            var email = "email@email.com";
            var newEmail = "newEmail@email.com";
            var id = Guid.NewGuid();
            
            var newPersonResult = Person.Create(id, fullName, email);
            var person = newPersonResult.Value;

            var command = new ChangeEmailCommand
            {
                newEmail = newEmail,
                UserId = id
            };

            Mock<IPersonRepository> mock = new Mock<IPersonRepository>();
            mock.Setup(x => x.GetByIdAsync(It.Is<Guid>(o => o.Equals(id)))).ReturnsAsync(person);
            mock.Setup(x => x.UpdateAsync(It.IsAny<Person>())).Throws<Exception>();
            
            var cancellationToken = new CancellationToken();
            var handler = new ChangeEmailCommandHandler(mock.Object);

            // Act
            var handlerResult = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(handlerResult.IsFailed);
        }
    }
}