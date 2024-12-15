using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;
        private readonly IGenreService genreService;
        public BookController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService, IGenreService genreService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.publisherService = publisherService;
            this.genreService = genreService;
        }
        public async Task<IActionResult> Add()
        {
            var model = new Book();
            model.PublisherList = (await publisherService.GetAll())
                .Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() })
                .ToList();
            model.AuthorList = (await authorService.GetAll())
                .Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList();
            model.GenreList = (await genreService.GetAll())
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book model)
        {
            model.PublisherList = (await publisherService.GetAll())
                .Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId})
                .ToList();
            model.AuthorList = (await authorService.GetAll())
                .Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId}).ToList();
            model.GenreList = (await genreService.GetAll())
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId}).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await bookService.Add(model);
            if (result)
            {
                TempData["msg"] = "Add Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await bookService.GetById(id);
            model.PublisherList = (await publisherService.GetAll())
               .Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId })
               .ToList();
            model.AuthorList = (await authorService.GetAll())
                .Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.GenreList = (await genreService.GetAll())
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Book model)
        {
            model.PublisherList = (await publisherService.GetAll())
               .Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId })
               .ToList();
            model.AuthorList = (await authorService.GetAll())
                .Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorId }).ToList();
            model.GenreList = (await genreService.GetAll())
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreId }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await bookService.Update(model);
            if (result)
            {
                return RedirectToAction(nameof(GetAll));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var record = await bookService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> GetAll()
        {
            var data = await bookService.GetAll();
            return View(data);
        }
    }
}
