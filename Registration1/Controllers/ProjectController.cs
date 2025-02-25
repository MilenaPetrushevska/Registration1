using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Registration1.Models;


namespace Registration1.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Project
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            Console.WriteLine($"UserId from session: {userId}");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var userProjects = _context.Projects.Where(p => p.UserId == userId.Value).ToList();
            Console.WriteLine($"Found {userProjects.Count} projects for user {userId}");
            foreach (var project in userProjects)
            {
                Console.WriteLine($"Project ID: {project.Id}, UserId: {project.UserId}");
            }

            return View(userProjects);
        }


        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null || project.UserId != HttpContext.Session.GetInt32("UserId"))
            {
                return NotFound();
            }

            return View(project);
        }
        // GET: Project/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Project/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }
            project.UserId = userId.Value;

            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }

            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("ID is null.");
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                Console.WriteLine($"Project not found with ID: {id}");
                return NotFound();
            }

            Console.WriteLine($"Project found: {project.Name} with ID: {project.Id}");

            if (project.UserId != HttpContext.Session.GetInt32("UserId"))
            {
                Console.WriteLine("User does not match.");
                return NotFound();
            }

            return View(project);
        }


        // POST: Project/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            Console.WriteLine($"Editing project with ID: {id}.");
            Console.WriteLine($"Project received for editing: Name - {project.Name}, ID - {project.Id}");

            if (id != project.Id)
            {
                Console.WriteLine($"ID mismatch: expected {id}, received {project.Id}");

                return NotFound();
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                project.UserId = userId.Value; 
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("Model is valid, updating project.");
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Project updated successfully.");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        Console.WriteLine($"Project with ID {project.Id} does not exist anymore.");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Model state is invalid.");
            }
            return View(project);
        }


        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null || project.UserId != HttpContext.Session.GetInt32("UserId"))
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == HttpContext.Session.GetInt32("UserId"));


            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index)); 
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

    }

    
}