using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateWebProject.Controllers
{
    public class SellPropertyModelController : Controller
    {
        // GET: SellPropertyModelController
        public ActionResult Index()
        {
            return View();
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
