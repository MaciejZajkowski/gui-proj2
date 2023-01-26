using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library.Data;
using library.Models;
using Microsoft.AspNetCore.Authorization;


namespace library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly MvcWebAppDbContext _context;

        public BooksController(MvcWebAppDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return _context.Books != null ? 
                          View(await _context.Books.ToListAsync()) :
                          Problem("Entity set 'MvcWebAppDbContext.Books'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.book_id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

         public async Task<IActionResult> Revoke(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'MvcWebAppDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null & book.user == User.Identity.Name)
            {
                book.user = "-";
                book.status = false;
                _context.Books.Update(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("book_id,author,user,title,publisher,year,status")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        public async Task<IActionResult> Edit2(int id)
        {
            ///Console.WriteLine("tutaj");
            if (_context.Books == null)
            {
                return Problem("Entity set 'MvcWebAppDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            // var new_book = new Book(){book_id = id, user = "lol",author ="lol",title ="lol",publisher="lol",year=2020,status =false};
            // _context.Books.Update(new_book);
            if (book != null & book.user == "-")
            {
                book.user = User.Identity.Name;
                //book.user.set("lol");
                //var new_book = new Book(){user = "lol",author ="lol",title ="lol",publisher="lol",year=2020,status =false};
                _context.Books.Update(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.book_id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


        public async Task<IActionResult> Conf(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'MvcWebAppDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null & book.user != "-")
            {

                book.status = true;
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
            
            
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DConfirm(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'MvcWebAppDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                book.user = "-";
                book.status = false;
                _context.Books.Update(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            
          return (_context.Books?.Any(e => e.book_id == id)).GetValueOrDefault();
        }
    }
}
