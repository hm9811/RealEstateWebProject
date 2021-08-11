using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateWebProject.Data;
using RealEstateWebProject.Models;
using RealEstateWebProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateWebProject.Controllers
{
    public class RentPropertyModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RentPropertyModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string name = await _userManager.GetUserNameAsync(user);
            var models = await _context.RentPropertyModel.Where(x => x.owner == name).ToListAsync();
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentPropertyViewModel rentPropertyViewModel)
        {
            string pathForPic = String.Empty;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                string name = await _userManager.GetUserNameAsync(user);
                if (rentPropertyViewModel.image != null)
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                    pathForPic = Guid.NewGuid().ToString() + "_" + rentPropertyViewModel.image.FileName;
                    string filePath = Path.Combine(uploadFolder, pathForPic);
                    rentPropertyViewModel.image.CopyTo(new FileStream(filePath, FileMode.Create));
                    RentPropertyModel r = new RentPropertyModel
                    {
                        name = rentPropertyViewModel.name,
                        image = pathForPic,
                        address = rentPropertyViewModel.address,
                        description = rentPropertyViewModel.description,
                        owner = name,
                        price = rentPropertyViewModel.price,
                        propertyType = rentPropertyViewModel.propertyType
                    };
                    _context.Add(r);

                    PropertyModel p = new PropertyModel
                    {
                        address = r.address,
                        description = r.description,
                        image = r.image,
                        name = r.name,
                        owner = r.owner,
                        price = r.price,
                        propertyDetail = PropertyDetail.Unsold,
                        propertyMode = PropertyMode.Rent,
                        propertyStatus = PropertyStatus.Pending,
                        propertyType = r.propertyType
                    };
                    _context.PropertyModel.Add(p);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rentPropertyViewModel);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentPropertyModel = await _context.RentPropertyModel.FindAsync(id);
            if (rentPropertyModel == null)
            {
                return NotFound();
            }
            ViewBag.path = rentPropertyModel.image;
            RentPropertyViewModel rentPropertyViewModel = new RentPropertyViewModel
            {
                address = rentPropertyModel.address,
                description = rentPropertyModel.description,
                name = rentPropertyModel.name,
                price = rentPropertyModel.price,
                propertyType = rentPropertyModel.propertyType,
                image = null
            };
            return View(rentPropertyViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RentPropertyViewModel rentPropertyViewModel)
        {
            if (id != rentPropertyViewModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    string name = await _userManager.GetUserNameAsync(user);
                    string pathForPic = Guid.NewGuid().ToString() + "_" + rentPropertyViewModel.image.FileName;
                    string filePath = Path.Combine(uploadFolder, pathForPic);
                    rentPropertyViewModel.image.CopyTo(new FileStream(filePath, FileMode.Create));

                    RentPropertyModel rentPropertyModel = _context.RentPropertyModel.FirstOrDefault(x => x.id == id);
                    rentPropertyModel.address = rentPropertyViewModel.address;
                    rentPropertyModel.description = rentPropertyViewModel.description;
                    rentPropertyModel.image = pathForPic;
                    rentPropertyModel.name = rentPropertyViewModel.name;
                    rentPropertyModel.owner = name;
                    rentPropertyModel.price = rentPropertyViewModel.price;
                    rentPropertyModel.propertyType = rentPropertyViewModel.propertyType;

                    PropertyModel p = _context.PropertyModel.FirstOrDefault(x => x.name == rentPropertyModel.name);
                    p.address = rentPropertyViewModel.address;
                    p.description = rentPropertyViewModel.description;
                    p.image = pathForPic;
                    p.name = rentPropertyViewModel.name;
                    p.owner = name;
                    p.price = rentPropertyViewModel.price;
                    p.propertyType = rentPropertyViewModel.propertyType;

                    _context.RentPropertyModel.Update(rentPropertyModel);
                    _context.PropertyModel.Update(p);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentPropertyModelExists(rentPropertyViewModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rentPropertyViewModel);
        }
        private bool RentPropertyModelExists(int id)
        {
            return _context.RentPropertyModel.Any(e => e.id == id);
        }

    }
}
