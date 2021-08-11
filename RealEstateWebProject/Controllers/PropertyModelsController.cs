using Microsoft.AspNetCore.Mvc;
using RealEstateWebProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateWebProject.Controllers
{
    public class PropertyModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyModelsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
