using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UserApi.Api.Controllers;

namespace UserApi.Tests.StepDefinitions
{
    [Binding]
    class UpdateUserFeatureSteps
    {
        ScenarioContext context = ScenarioContext.Current;

        [When(@"I request to update the user by Id with details")]
        public void WhenIRequestToUpdateTheUserByIdWithDetails(Table table)
        {
            var updateData = table.CreateInstance<UpdateUserDataTable>();
            var usersController = context.Get<UsersController>();
            var response = usersController.UpdateUser(1, updateData.Name, updateData.Surname, updateData.Email);
            context.Set(response);
        }

        [Then(@"the user should be updated")]
        public void ThenTheUserShouldBeUpdated()
        {
            var response = context.Get<IHttpActionResult>();
            var resp = response as OkResult;
            Assert.IsNotNull(resp);
        }

        [When(@"I request to update the user by Id with details Name: '(.*)' Surname: '(.*)' and Email: '(.*)'")]
        public void WhenIRequestToUpdateTheUserByIdWithDetailsNameSurnameAndEmail(string p0, string p1, string p2)
        {
            var usersController = context.Get<UsersController>();
            var response = usersController.UpdateUser(1, p0, p1, p2);
            context.Set(response);
        }

        class UpdateUserDataTable
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
        }

    }
}
