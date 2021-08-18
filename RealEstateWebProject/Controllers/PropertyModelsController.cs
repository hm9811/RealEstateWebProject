using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateWebProject.Data;
using RealEstateWebProject.Models;
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyModel = await _context.PropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (propertyModel == null)
            {
                return NotFound();
            }

            return View(propertyModel);
        }
        public async Task<IActionResult> Verify(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyModel = await _context.PropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (propertyModel == null)
            {
                return NotFound();
            }
            propertyModel.propertyStatus = PropertyStatus.Verified;
            _context.Update(propertyModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Unfit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyModel = await _context.PropertyModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (propertyModel == null)
            {
                return NotFound();
            }
            propertyModel.propertyStatus = PropertyStatus.Unfit;
            _context.Update(propertyModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        private bool PropertyModelExists(int id)
        {
            return _context.PropertyModel.Any(e => e.id == id);
        }
    }


}
