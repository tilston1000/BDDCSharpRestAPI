using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TechTalk.SpecFlow;
using UserApi.Api.Controllers;
using UserApi.Data.Repositories;

namespace UserApi.Tests
{
    [Binding]
    public class GetUserFeatureSteps
    {
        ScenarioContext context = ScenarioContext.Current;

        [BeforeScenario, Scope(Scenario = "Get user by id")]
        public static void TestHook()
        {

        }

        [BeforeScenario, Scope(Scenario = "Get user by id")]
        public static void TestHook2()
        {

        }

        [Given(@"that a user exists in the system")]
        public void GivenThatAUserExistsInTheSystem()
        {
            InMemoryUsersRepository repository = new InMemoryUsersRepository();
            Entities.User user = new Entities.User() { Id = 1, Email = "test@test.com", Name = "TestName", Surname = "TestSurname" };
            repository.Add(user);
            UsersController controller = new UsersController(repository);
            context.Set(controller);
        }

        [When(@"I request to get the user by Id"), Scope(Tag = "tag1", Scenario = "Get user by id")]
        public void WhenIRequestToGetTheUserById()
        {
            var usersController = context.Get<UsersController>();
            context.Set(usersController.GetUser(1));
        }

        [Then(@"the user should be returned in the response")]
        public void ThenTheUserShouldBeReturnedInTheResponse()
        {
            var response = context.Get<IHttpActionResult>();
            var user = response as OkNegotiatedContentResult<Entities.User>;
            Assert.AreEqual(user.Content.Id, 1);
        }

        [Then(@"the response status code is '(.*)'")]
        public void ThenTheResponseStatusCodeIs(string p0)
        {
            var response = context.Get<IHttpActionResult>();
            if (p0 =="200 OK")
            {
                var resp = response as OkNegotiatedContentResult<Entities.User>;
                Assert.IsNotNull(resp);
            }
            else if(p0 == "404 Not Found")
            {
                var resp = response as NotFoundResult;
                var resp2 = response as OkResult;
                Assert.IsFalse(resp == null && resp2 == null);
            } else if(p0 == "400 Bad Request")
            {
                var resp = response as BadRequestResult;
                Assert.IsNotNull(resp);
            }
        }

        [Given(@"that a user does not exist in the system")]
        public void GivenThatAUserDoesNotExistInTheSystem()
        {
            InMemoryUsersRepository repository = new InMemoryUsersRepository();
            UsersController controller = new UsersController(repository);
            context.Set(controller);
        }

        [Then(@"no user should be returned in the response")]
        public void ThenNoUserShouldBeReturnedInTheResponse()
        {
            var response = context.Get<IHttpActionResult>();
            var resp = response as NotFoundResult;
            Assert.IsNotNull(resp);
        }


    }
}
