using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // GET: PropertyModels
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var props = from c in _context.PropertyModel select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                props = props.Where(x => x.name.Contains(searchString) || x.address.Contains(searchString));
                ViewBag.visible = true;
            }
            else
            {
                ViewBag.visible = false;
            }
            return View(await props.AsNoTracking().ToListAsync());
        }
    }


}
