using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DenunciasWebApp.Data;
using DenunciasWebApp.Models;

namespace DenunciasWebApp.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var complaints = _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Status)
                .Include(c => c.Crime)
                .OrderByDescending(c => c.CreatedAt);
            return View(await complaints.ToListAsync());
        }

        // GET: Admin/ChangeStatus/5
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.User)
                .Include(c => c.Status)
                .Include(c => c.Crime)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (complaint == null)
            {
                return NotFound();
            }

            ViewBag.Statuses = new SelectList(_context.ComplaintStatuses, "Id", "Name", complaint.StatusId);
            return View(complaint);
        }

        // POST: Admin/ChangeStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, int statusId)
        {
            var complaint = await _context.Complaints.FindAsync(id);

            if (complaint == null)
            {
                return NotFound();
            }

            complaint.StatusId = statusId;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
