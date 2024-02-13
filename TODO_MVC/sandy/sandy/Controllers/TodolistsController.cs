using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sandy.Models;

namespace sandy.Controllers
{
    public class TodolistsController : Controller
    {
        private readonly TodoContext _context;

        public TodolistsController(TodoContext context)
        {
            _context = context;
        }

        // GET: Todolists
        public async Task<IActionResult> Index()
        {
            var todoContext = _context.Todolist.Include(t => t.User);
            return View(await todoContext.ToListAsync());
        }

        // GET: Todolists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Todolist == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TodoID == id);
            if (todolist == null)
            {
                return NotFound();
            }

            return View(todolist);
        }

        // GET: Todolists/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Webuser, "UserID", "Email");
            return View();
        }

        // POST: Todolists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodoID,UserID,Category,Remark,CreateDate,ModifyDate,ScaheduledDate,IsCompleted")] Todolist todolist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todolist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Webuser, "UserID", "Email", todolist.UserID);
            return View(todolist);
        }

        // GET: Todolists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Todolist == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Webuser, "UserID", "Email", todolist.UserID);
            return View(todolist);
        }

        // POST: Todolists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TodoID,UserID,Category,Remark,CreateDate,ModifyDate,ScaheduledDate,IsCompleted")] Todolist todolist)
        {
            if (id != todolist.TodoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todolist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodolistExists(todolist.TodoID))
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
            ViewData["UserID"] = new SelectList(_context.Webuser, "UserID", "Email", todolist.UserID);
            return View(todolist);
        }

        // GET: Todolists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Todolist == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TodoID == id);
            if (todolist == null)
            {
                return NotFound();
            }

            return View(todolist);
        }

        // POST: Todolists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Todolist == null)
            {
                return Problem("Entity set 'TodoContext.Todolist'  is null.");
            }
            var todolist = await _context.Todolist.FindAsync(id);
            if (todolist != null)
            {
                _context.Todolist.Remove(todolist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodolistExists(int id)
        {
          return (_context.Todolist?.Any(e => e.TodoID == id)).GetValueOrDefault();
        }
    }
}
