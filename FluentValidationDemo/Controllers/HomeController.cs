using FluentValidationDemo.Models;
using FluentValidationDemo.Validator;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace FluentValidationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IList<PersonModel> _persons = new List<PersonModel>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult CreatePerson()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreatePerson(PersonModel person)
        {
            PersonValidator validator = new PersonValidator();

            var results = validator.Validate(person);

            if (!results.IsValid)
            {
                foreach (var fail in results.Errors)
                {
                    ModelState.AddModelError(fail.PropertyName, fail.ErrorMessage);
                }
            }

            _persons.Add(person);

            return View();
        }

        public IActionResult ListPerson()
        {
           

            return View(_persons);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}