﻿using AJAX.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AJAX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View();  
        }

        public IActionResult PM25()
        {
            return View();
        }

        public IActionResult ExchangeRate()
        {
            return View();
        }

        public IActionResult VUEClient()
        {
            return View();
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