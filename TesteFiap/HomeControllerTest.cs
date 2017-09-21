using MeuSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TesteFiap
{
    public class HomeControllerTest
    {
        [Fact]
        public async System.Threading.Tasks.Task dado_um_repository_sem_receitas_deve_retornar_uma_view_chamada_norecipesAsync()
        {
            var repository = new Moq.Mock<IRepository>();

            repository.Setup(x => x.List()).Returns(new List<Recipe>());

            var controller = new HomeController(repository.Object);
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Equal("NoRecipes", viewResult.ViewName);
        }
    }
}
