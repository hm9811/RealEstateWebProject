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
    public class BidModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BidModelsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var bids = await _context.BidModel.ToListAsync();
            List<BidModel> bidss = new List<BidModel>();
            List<bool> isRent = new List<bool>();
            foreach (var bid in bids)
            {
                var property = _context.PropertyModel.FirstOrDefault(x => x.name == bid.propname);
                if (property.propertyMode == PropertyMode.Rent)
                {
                    isRent.Add(true);
                }
                else
                {
                    isRent.Add(false);
                }
            }
            ViewBag.isRent = isRent;
            return View(bids);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidModel = await _context.BidModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (bidModel == null)
            {
                return NotFound();
            }
            var property = _context.PropertyModel.FirstOrDefault(x => x.name == bidModel.propname);
            ViewBag.isRent = (property.propertyMode == PropertyMode.Rent) ? true : false;
            return View(bidModel);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidModel = await _context.BidModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (bidModel == null)
            {
                return NotFound();
            }
            var property = _context.PropertyModel.FirstOrDefault(x => x.name == bidModel.propname);
            ViewBag.propname = property.name;
            ViewBag.isRent = (property.propertyMode == PropertyMode.Rent) ? true : false;
            return View(bidModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bidModel = await _context.BidModel.FindAsync(id);
            _context.BidModel.Remove(bidModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidModelExists(int id)
        {
            return _context.BidModel.Any(e => e.id == id);
        }
    }
}
