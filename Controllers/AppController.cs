using DutchTreatEmpty.Data;
using DutchTreatEmpty.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreatEmpty.Controllers
{
    public class AppController : Controller
    {
        private readonly ILogger<AppController> logger;
        private readonly IDutchRepository repository;

        public AppController(ILogger<AppController> _logger,
            IDutchRepository repository)
        {
            logger = _logger;
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send email
            }
            else
            {
                //Send Error
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Shop()
        {
            var products = repository.GetAllProducts();
            return View(products);
        }
    }
}
