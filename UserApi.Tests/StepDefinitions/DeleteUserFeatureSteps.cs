using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using UserApi.Api.Controllers;
using UserApi.Data.Interfaces;

namespace UserApi.Tests
{
    [Binding]
    class DeleteUserFeatureSteps
    {
        ScenarioContext context = ScenarioContext.Current;

        [When(@"I request to delete the user by Id")]
        public void WhenIRequestToDeleteTheUserById()
        {
            UsersController controller = context.Get<UsersController>();
            var response = controller.DeleteUser(1);
            context.Set(response);
        }

        [Then(@"the user should be removed from the repository")]
        public void ThenTheUserShouldBeRemovedFromTheRepository()
        {
            var repository = context.Get<IUsersRepository>();
            Assert.IsTrue(repository.GetById(1) == null);
        }
    }
}
