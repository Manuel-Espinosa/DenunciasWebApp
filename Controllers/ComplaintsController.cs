using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DenunciasWebApp.Data;
using DenunciasWebApp.Models;

namespace DenunciasWebApp.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ComplaintsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Complaints
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var complaints = _context.Complaints
                .Include(c => c.Status)
                .Include(c => c.Crime)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt);
            return View(await complaints.ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var complaint = await _context.Complaints
                .Include(c => c.Status)
                .Include(c => c.Crime)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            ViewBag.Crimes = new SelectList(_context.Crimes, "Id", "Name");
            return View();
        }

        // POST: Complaints/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CrimeId")] Complaint complaint)
        {
            var userId = _userManager.GetUserId(User);
            complaint.UserId = userId!;
            complaint.CreatedAt = DateTime.UtcNow;

            // Set initial status to "Activa" (Id = 1)
            var activeStatus = await _context.ComplaintStatuses.FirstOrDefaultAsync(s => s.Name == "Activa");
            complaint.StatusId = activeStatus?.Id ?? 1;

            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Crimes = new SelectList(_context.Crimes, "Id", "Name", complaint.CrimeId);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (complaint == null)
            {
                return NotFound();
            }

            ViewBag.Crimes = new SelectList(_context.Crimes, "Id", "Name", complaint.CrimeId);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CrimeId")] Complaint complaint)
        {
            if (id != complaint.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var existingComplaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (existingComplaint == null)
            {
                return NotFound();
            }

            existingComplaint.Title = complaint.Title;
            existingComplaint.Description = complaint.Description;
            existingComplaint.CrimeId = complaint.CrimeId;

            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.Id))
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

            ViewBag.Crimes = new SelectList(_context.Crimes, "Id", "Name", complaint.CrimeId);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var complaint = await _context.Complaints
                .Include(c => c.Status)
                .Include(c => c.Crime)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var complaint = await _context.Complaints
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (complaint == null)
            {
                return NotFound();
            }

            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.Id == id);
        }
    }
}
