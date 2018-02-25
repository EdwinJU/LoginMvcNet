using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaLogin.Models;

using Microsoft.AspNetCore.Http;  
using Newtonsoft.Json;

namespace PruebaLogin.Controllers
{
    public class UsersController : Controller
    {
        private readonly MvcUserContext _context;

        public UsersController(MvcUserContext context)
        {
            _context = context;
        

        }


        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }

        public IActionResult Login()
        {
            //List<Make> makes = new List<Make>();
            //makes= db.MvcPrueba.ToList();
            //ViewBag.listaCursos = makes;
            return View();
        }   

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username,Password")] User users)
        {

            var user = await _context.Movie
               .SingleOrDefaultAsync(m => m.Username == users.Username && m.Password == users.Password);

            if (user != null)
            {

              HttpContext.Session.SetString("User",user.ToString());
  

              ViewBag.message="Bien lo logro";
                    
              return RedirectToAction("Create");

            }
            else
            {
                ModelState.AddModelError("", "El Usuario o la contraseña son erroneas");
                  return RedirectToAction("Index");
            }
            return View();
          

        }

        //public ActionResult LoggedIn(){

          //  if(_session.GetString(("User")!=null){

            //    return View();

            //}else{
              //  return RedirectToAction("Login");
            //}
        //}



        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Movie
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Lastname,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Lastname,Username,Password")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Movie
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            _context.Movie.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
