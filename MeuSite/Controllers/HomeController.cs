using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MeuSite.Models;

namespace MeuSite.Controllers
{
    public class HomeController : Controller
    {
        private IRepository @object;

        public HomeController(IRepository @object)
        {
            this.@object = @object;
        }

        public async Task<IActionResult> Index()
        {
            var list = @object.List();

            if (!list.Any())
                return View("NoRecipes");

            return View(list);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Recipe
    {
        public int Id { get; set; }
    }

    public class Repository : IRepository
    {
        public List<Recipe> List()
        {
            return new List<Recipe>();
        }
    }

    public interface IRepository
    {
        List<Recipe> List();
    }
}
