using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateWebProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateWebProject.Controllers
{
    public class SellPropertyModelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SellPropertyModelController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: SellPropertyModelController
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            var models = await _context.SellPropertyModel.Where(x => x.owner == name).ToListAsync();
            if (models.Count > 0)
            {
                ViewBag.empty = false;
            }
            else
            {
                ViewBag.empty = true;
            }
            return View(models);
        }

        // GET: SellPropertyModelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SellPropertyModelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellPropertyModelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SellPropertyModelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SellPropertyModelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SellPropertyModelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellPropertyModelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
