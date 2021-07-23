using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateWebProject.Data;
using RealEstateWebProject.Models;

namespace RealEstateWebProject.Controllers
{
    public class AgentModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AgentModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentModel.ToListAsync());
        }

        // GET: AgentModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentModel = await _context.AgentModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (agentModel == null)
            {
                return NotFound();
            }

            return View(agentModel);
        }

        // GET: AgentModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,email")] AgentModel agentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentModel);
        }

        // GET: AgentModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentModel = await _context.AgentModel.FindAsync(id);
            if (agentModel == null)
            {
                return NotFound();
            }
            return View(agentModel);
        }

        // POST: AgentModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,email")] AgentModel agentModel)
        {
            if (id != agentModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentModelExists(agentModel.id))
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
            return View(agentModel);
        }

        // GET: AgentModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentModel = await _context.AgentModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (agentModel == null)
            {
                return NotFound();
            }

            return View(agentModel);
        }

        // POST: AgentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentModel = await _context.AgentModel.FindAsync(id);
            _context.AgentModel.Remove(agentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentModelExists(int id)
        {
            return _context.AgentModel.Any(e => e.id == id);
        }
    }
}
