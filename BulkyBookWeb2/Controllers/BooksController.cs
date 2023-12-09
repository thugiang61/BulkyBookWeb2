using BulkyBookWeb2.Data;
using BulkyBookWeb2.Models;
using BulkyBookWeb2.Models.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BulkyBookWeb2.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly BulkyBookWeb2Context _context;

        public BooksController(BulkyBookWeb2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int selectedYear = (int)FilteredByYearCriteria.ShowAll)// 2 tham so nay o 2 view khac nhau nen luc goi thi cx chi goi dc theo 1 trg 2 tiu chi th
        {
            // dung IQuery thay vi IEnum vi IQuery phu hop dung cho out-memory nhu sql, con IEnum la cho in-memory
            IQueryable<Book> books = from b in _context.Book select b;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(currentUserId))
            {
                // lay books cua rieng user htai trc r ms lay years theo cai books do ms dung
                books = books.Where(b => b.UserId == currentUserId);
            }

            IQueryable<int> years = books.Select(b => b.FinishDate.HasValue ? b.FinishDate.Value.Year : (int)FilteredByYearCriteria.NotFinishedYet).Distinct();

            if (selectedYear != (int)FilteredByYearCriteria.ShowAll)
            {
                if (selectedYear == (int)FilteredByYearCriteria.NotFinishedYet)
                {
                    books = books.Where(b => !b.FinishDate.HasValue);
                }
                else
                {
                    books = books.Where(b => b.FinishDate.HasValue && b.FinishDate.Value.Year == selectedYear);
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Name.Contains(searchString) || b.Author.Contains(searchString));
            }

            var booksFilteredByYear = new BooksFilteredByYearViewModel
            {
                Books = await books.ToListAsync(),
                Years = await years.ToListAsync(),
                SelectedYear = selectedYear,
            };

            return View(booksFilteredByYear);
        }


        ////index, details, create, update, delete
        //[HttpGet]
        //public async Task<IActionResult> Index(int searchString)
        //{
        //    var books = from b in _context.Book select b;

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        books = books.Where(b => b.Name.Contains(searchString) || b.Author.Contains(searchString));
        //    }

        //    return View(await books.ToListAsync());
        //}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }
 
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = await _context.Book.FirstAsync(b => b.Id == id && b.UserId == currentUserId);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public IActionResult Create() { return View(); }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Author,Genre,StartDate,FinishDate,Status,Review,OtherNote,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(book);
                await _context.SaveChangesAsync();

                TempData["success"] = "The book is created successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = await _context.Book.FirstAsync(b => b.Id == id && b.UserId == currentUserId);
            if (book == null || book.UserId != currentUserId)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Name,Author,Genre,StartDate,FinishDate,Status,Review,OtherNote,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }


            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (book == null || book.UserId != currentUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["success"] = "The book is edited successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        //GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }
                  
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = await _context.Book.FirstAsync(b => b.Id == id && b.UserId == currentUserId);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/DeleteConfirmed/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var book = await _context.Book.FirstAsync(b => b.Id == id && b.UserId == currentUserId);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
                TempData["success"] = "The book is deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
        }

        public bool BookExists(int id)
        {
            return _context.Book.Any(x => x.Id == id);
        }
    }
}
