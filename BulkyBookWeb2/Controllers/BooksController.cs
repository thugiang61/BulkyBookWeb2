using BulkyBookWeb2.Data;
using BulkyBookWeb2.Models;
using BulkyBookWeb2.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb2.Controllers
{
    public class BooksController : Controller
    {
        private readonly BulkyBookWeb2Context _context;

        public BooksController(BulkyBookWeb2Context context)
        {
            _context = context;
        }

        //index, details, create, update, delete
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int selectedYear = (int)FilteredByYearCriteria.ShowAll)// 2 tham so nay o 2 view khac nhau nen luc goi thi cx chi goi dc theo 1 trg 2 tiu chi th
        {
            // dung IQuery thay vi IEnum vi IQuery phu hop dung cho out-memory nhu sql, con IEnum la cho in-memory
            IQueryable<Book> books = from b in _context.Book select b;
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
                Books = await books.ToListAsync(),// List la con cua IEnum nen ko can cast tu tk con cu the len tk cha tong quat
                Years = await years.ToListAsync(),
                SelectedYear = selectedYear,
            };

            return View(booksFilteredByYear);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);

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

            // vì sao theo scraffolding code thì chỉ mình chỗ này vs chỗ deleteconfirmed ms xài find th chư còn lại đều xài firstordefault hết nhỉ 
            var book = await _context.Book.FindAsync(id);
            if (book == null)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,Genre,StartDate,FinishDate,Status,Review,OtherNote,Price")] Book book)
        {
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

            // mình nghĩ là nếu chỉ cần dữ liệu để hiện thị lên view th thì xài find để tìm trog dbcontext trc (r nếu ko có thì sẽ vô db) ổn hơn là xài firstordefault để mà vô db tìm lun 
            var book = await _context.Book.FindAsync(id);
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
            // vì sao theo scraffolding code thì chỉ mình chỗ này vs chỗ view edit ms xài find th chư còn lại đều xài firstordefault hết nhỉ 
            var book = await _context.Book.FindAsync(id);
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

            //vi ko co check model isValid hay ko nhu khi edit hay create nen la ko can co dong nay de load lai trang edit hay create nx
            //return View(book);
        }

        public bool BookExists(int id)
        {
            return _context.Book.Any(x => x.Id == id);
        }
    }
}
